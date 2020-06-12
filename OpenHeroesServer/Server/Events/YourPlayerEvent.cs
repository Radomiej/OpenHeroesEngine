using OpenHeroesServer.Server.Models;

namespace OpenHeroesServer.Server.Events
{
    public class YourPlayerEvent
    {
        public string ConnectionId;
        public string PlayerId;
        public string PlayerName;

        public YourPlayerEvent()
        {
        }

        public YourPlayerEvent(JPlayer player)
        {
            ConnectionId = player.ConnectionId;
            PlayerId = player.Id;
            PlayerName = player.Name;
        }
    }
}