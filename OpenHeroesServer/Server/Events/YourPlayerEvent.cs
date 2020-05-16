using OpenHeroesServer.Server.Models;

namespace OpenHeroesServer.Server.Events
{
    public class YourPlayerEvent
    {
        public string hid;

        public YourPlayerEvent(JPlayer player)
        {
            this.hid = player.ConnectionId;
        }
    }
}