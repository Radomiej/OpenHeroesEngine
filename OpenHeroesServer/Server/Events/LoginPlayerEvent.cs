using System;
using System.Diagnostics.CodeAnalysis;
using OpenHeroesServer.Server.Models;

namespace OpenHeroesServer.Server.Events
{
    [Serializable]
    public class LoginPlayerEvent
    {
        public string PlayerId;
        [NotNull]
        public string ConnectionId;
        [NotNull]
        public string Name;

        public LoginPlayerEvent()
        {
        }

        public LoginPlayerEvent(JPlayer player)
        {
            PlayerId = player.Id;
            ConnectionId = player.ConnectionId;
            Name = player.Name;
        }

        public LoginPlayerEvent(string name, string playerId)
        {
            this.PlayerId = playerId;
            this.Name = name;
        }
    }
}