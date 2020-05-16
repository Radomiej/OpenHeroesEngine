using System;
using OpenHeroesServer.Server.Models;

namespace OpenHeroesServer.Server.Events
{
    [Serializable]
    public class LoginPlayerEvent
    {
        public string connectionId;
        public string name;

        public LoginPlayerEvent()
        {
        }

        public LoginPlayerEvent(JPlayer player)
        {
            connectionId = player.ConnectionId;
            name = player.Name;
        }

        public LoginPlayerEvent(string name, string connectionId)
        {
            this.connectionId = connectionId;
            this.name = name;
        }
    }
}