using System;
using OpenHeroesServer.WebSocket.Models;

namespace OpenHeroesServer.WebSocket
{
    [Serializable]
    public class WsMessage
    {
        public string channel;
        public string type;
        public string message;
        public NetworkPersistenceType persistenceType = NetworkPersistenceType.None;
    }
}