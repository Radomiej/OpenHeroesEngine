using System;
using OpenHeroesServer.Server.Models;

namespace OpenHeroesServer.Server.Events
{
    [Serializable]
    public class CreatePlayerEvent
    {
        public string hid;
        public string name;

        public CreatePlayerEvent()
        {
        }

        public CreatePlayerEvent(JPlayer player)
        {
            hid = player.Id;
            name = player.Name;
        }

        public CreatePlayerEvent(string name, string hid)
        {
            this.hid = hid;
            this.name = name;
        }
    }
}