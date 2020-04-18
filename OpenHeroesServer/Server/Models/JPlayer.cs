using OpenHeroesServer.WebSocket;

namespace OpenHeroesServer.Server.Models
{
    public class JPlayer
    {
        public bool Connected { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public PlayerWsService PlayerWsService;
    }
}