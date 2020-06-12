using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using OpenHeroesEngine.Utils;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesServer.Server;
using OpenHeroesServer.Server.Events;
using OpenHeroesServer.Server.Models;
using OpenHeroesServer.WebSocket;
using WebSocketSharp;

namespace TestOpenHeroesServer.Internal
{
    public class OHSWebSocketClient
    {
        private readonly string _host;
        private readonly int _serverPort;
        private bool _connected;
        private bool _signup;
        private WebSocket _webSocket;
        private volatile JPlayer _playerInfo;
        private QueueEvents _queueEvents;
        public QueueEvents Events => _queueEvents;

        public JPlayer PlayerInfo => _playerInfo;

        private CountdownLatch connectWatcher = new CountdownLatch(1);
        private CountdownLatch signupWatcher = new CountdownLatch(1);
        
        private ArrayList _typeWaiters = ArrayList.Synchronized(new ArrayList());

        public OHSWebSocketClient(string host, int serverPort)
        {
            _host = host;
            _serverPort = serverPort;
        }

        public void Connect()
        {
            _queueEvents = new QueueEvents();
            _webSocket = new WebSocket($"ws://{_host}:{_serverPort}/Javity");
            _webSocket.OnMessage += OnMessage;
            _webSocket.OnClose += OnClose;
            _webSocket.Connect();
        }

        private void OnClose(object? sender, CloseEventArgs e)
        {
            Debug.WriteLine("OnClose: " + String.Format("{0}", e.Code));
        }

        private void OnMessage(object? sender, MessageEventArgs e)
        {
            Debug.WriteLine("Message: " + String.Format("{0}", e.Data));
            object incomingRealObject = JsonDeserializer.DeserializeMessage(e);
            if(!ProcessInternal(incomingRealObject)) Events.Add(incomingRealObject);
        }

        private bool ProcessInternal(object incomingRealObject)
        {
            if (incomingRealObject is YourConnectionIdEvent yourConnectionId)
            {
                _playerInfo = new JPlayer();
                PlayerInfo.ConnectionId = yourConnectionId.ConnectionId;
                _connected = true;
                connectWatcher.Signal();
            }
            else if (incomingRealObject is YourPlayerEvent yourPlayerEvent)
            {
                PlayerInfo.Id = yourPlayerEvent.PlayerId;
                PlayerInfo.Name = yourPlayerEvent.PlayerName;
                _signup = true;
                Debug.WriteLine("Zwalniam SIGNUP");
                signupWatcher.Signal();
            }
            else
            {
                ProcessWaiters(incomingRealObject);
                return false;
            }
          
            return true;
        }

        private void ProcessWaiters(object incomingRealObject)
        {
            foreach (var obj in _typeWaiters.ToArray())
            {
                TypeWaiter waiter = obj as TypeWaiter;
                if(waiter == null) throw new NotSupportedException("waiter must be TypeWaiter");
                
                if(waiter.Type1 != incomingRealObject.GetType()) continue;
                waiter.Value = incomingRealObject;
                waiter.Signal();
                _typeWaiters.Remove(obj);
            }
        }

        public void WaitForConnectionId()
        {
            connectWatcher.Wait();
        }
        public void PlayerLogin(string name, string playerId = "")
        {
            if (_signup)
            {
                throw new NotSupportedException("ReLogin is not supported");
            }
            if (!_connected && !connectWatcher.TryWait(2000) && !_connected)
            {
                throw new NotSupportedException("Client should be connected to server");
            }
            
            var loginEvent = new LoginPlayerEvent(name, playerId);
            loginEvent.ConnectionId = PlayerInfo.ConnectionId;
            SendMessage(loginEvent);
            // Debug.WriteLine("Czekam na SIGNUP");
            // signupWatcher.Wait();
        }

        public void SendMessage(object createPlayerEvent, string channel = "public")
        {
            string message = WsMessageBuilder.CreateWsText(channel, createPlayerEvent);
            _webSocket.Send(message);
        }
        
        public void Disconnect()
        {
            _webSocket.Close(CloseStatusCode.Normal);
        }

        public T WaitForEvent<T>()
        {
            var countdownLatch = AddWaiter<T>(out var typeWaiter);
            countdownLatch.Wait();
            return typeWaiter.Value is T ? (T) typeWaiter.Value : default;
        }

        private CountdownLatch AddWaiter<T>(out TypeWaiter typeWaiter)
        {
            Type typeParameterType = typeof(T);
            CountdownLatch countdownLatch = new CountdownLatch(1);
            typeWaiter = new TypeWaiter(typeParameterType, countdownLatch);
            _typeWaiters.Add(typeWaiter);
            return countdownLatch;
        }

        public T SendAndWaitForEvent<T>(object eventToSend)
        {
            var countdownLatch = AddWaiter<T>(out var typeWaiter);
            SendMessage(eventToSend);
            countdownLatch.Wait();
            return typeWaiter.Value is T ? (T) typeWaiter.Value : default;
        }
    }
}