using OpenHeroesEngine.AStar;
using OpenHeroesEngine.Game.Models;

namespace OpenHeroesEngine.Game.Events
{
    public class AddResourceEvent
    {
        public readonly Point Position;
        public readonly Resource Resource;

        public AddResourceEvent(Resource resource, Point position)
        {
            Resource = resource;
            Position = position;
        }
    }
}