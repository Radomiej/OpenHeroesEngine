using System;
using Newtonsoft.Json;
using OpenHeroesServer.WebSocket.Models;

namespace OpenHeroesServer.WebSocket
{
    [Serializable]
    public class WsMessage
    {
        public string channel;
        public string type;
        public string message;
        [JsonIgnore]
        public NetworkPersistenceType persistenceType = NetworkPersistenceType.None;
    }
}