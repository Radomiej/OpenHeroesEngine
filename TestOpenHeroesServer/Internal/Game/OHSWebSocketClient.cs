using System;
using System.Diagnostics;
using Newtonsoft.Json;
using OpenHeroesEngine.Utils;
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

        private CountdownLatch connectWatcher = new CountdownLatch(1);
        private CountdownLatch signupWatcher = new CountdownLatch(1);

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
                _playerInfo.ConnectionId = yourConnectionId.ConnectionId;
                _connected = true;
                connectWatcher.Signal();
            }
            else if (incomingRealObject is YourPlayerEvent yourPlayerEvent)
            {
                _playerInfo.Id = yourPlayerEvent.PlayerId;
                _playerInfo.Name = yourPlayerEvent.PlayerName;
                _signup = true;
                Debug.WriteLine("Zwalniam SIGNUP");
                signupWatcher.Signal();
            }
            else return false;

            return true;
        }

        public void WaitForConnectionId()
        {
            connectWatcher.Wait();
        }
        public void PlayerLogin(string name, string playerId)
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
            loginEvent.ConnectionId = _playerInfo.ConnectionId;
            SendMessage(loginEvent);
            // Debug.WriteLine("Czekam na SIGNUP");
            // signupWatcher.Wait();
        }

        public void SendMessage(object createPlayerEvent, string channel = "public")
        {
            string message = WsMessageBuilder.CreateWsText(channel, createPlayerEvent);
            _webSocket.Send(message);
        }
    }
}