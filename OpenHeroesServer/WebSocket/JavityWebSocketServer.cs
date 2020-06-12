using System;
using System.Collections.Generic;
using OpenHeroesServer.WebSocket.Models;
using Radomiej.JavityBus;
using WebSocketSharp.Server;

namespace OpenHeroesServer.WebSocket
{
    public class JavityWebSocketServer
    {
        private static JavityWebSocketServer _instance;
        public static JavityWebSocketServer GetInstance()
        {
            if(_instance == null) _instance = new JavityWebSocketServer();
            return _instance;
        }

        private WebSocketServer _webSocketServer;
        private readonly List<WsMessage> _persistentMessages = new List<WsMessage>(1000);
        
        public void AddWsService<T> (string path) where T : WebSocketBehavior, new()
        {
            _webSocketServer.AddWebSocketService<T>(path);
        }

        public void AddWsService<T>(string path, Func<T> initializer)
            where T : WebSocketBehavior
        {
            _webSocketServer.AddWebSocketService<T>(path, initializer);
        }

        public void Create(int port = 4649)
        {
            _webSocketServer = new WebSocketServer(port);
        }

        public void Start()
        {
            _webSocketServer.Start();
        }

        public void Stop()
        {
            _webSocketServer.Stop();
        }

        [Subscribe]
        public void SendMessageListener(WsMessage wsMessageEvent)
        {
            if (wsMessageEvent.persistenceType == NetworkPersistenceType.None) return;
            _persistentMessages.Add(wsMessageEvent);
        }
        
        public List<WsMessage> GetPersistentMessages()
        {
            return _persistentMessages;
        }
    }
}