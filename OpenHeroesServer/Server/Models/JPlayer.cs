using System;
using OpenHeroesServer.WebSocket;

namespace OpenHeroesServer.Server.Models
{
    public class JPlayer
    {
        public bool Connected { get; set; }
        public string ConnectionId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public PlayerWsService PlayerWsService;

        protected bool Equals(JPlayer other)
        {
            return ConnectionId == other.ConnectionId && Id == other.Id && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((JPlayer) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ConnectionId, Id, Name);
        }
    }
}