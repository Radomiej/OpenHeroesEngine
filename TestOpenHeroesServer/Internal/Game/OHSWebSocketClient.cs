using System;
using System.Diagnostics;
using Newtonsoft.Json;
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
        private WebSocket _webSocket;
        private JPlayer _playerInfo;

        public OHSWebSocketClient(string host, int serverPort)
        {
            _host = host;
            _serverPort = serverPort;
        }

        public void Connect()
        {
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
            WsMessage receivedMessage = JsonConvert.DeserializeObject<WsMessage>(e.Data);
            object incomingRealObject = WsMessageBuilder.ReadWsMessage(receivedMessage);
            if(!ProcessInternal(incomingRealObject)) QueueEvents.Instance.Add(incomingRealObject);
        }

        private bool ProcessInternal(object incomingRealObject)
        {
            if (incomingRealObject is YourConnectionIdEvent yourConnectionId)
            {
                _playerInfo = new JPlayer();
                _playerInfo.ConnectionId = yourConnectionId.ConnectionId;
            }
            else return false;

            return true;
        }

        public void PlayerLogin(string name, string token)
        {
            SendMessage(new LoginPlayerEvent(name, token));
        }

        public void SendMessage(object createPlayerEvent, string channel = "public")
        {
            string message = WsMessageBuilder.CreateWsText(channel, createPlayerEvent);
            _webSocket.Send(message);
        }
    }
}