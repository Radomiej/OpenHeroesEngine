using OpenHeroesServer.Server.Models;

namespace OpenHeroesServer.Server.Events
{
    public class YourConnectionIdEvent
    {
        public string ConnectionId;

        public YourConnectionIdEvent()
        {
            
        }
        
        public YourConnectionIdEvent(JPlayer player)
        {
            this.ConnectionId = player.ConnectionId;
        }
    }
}