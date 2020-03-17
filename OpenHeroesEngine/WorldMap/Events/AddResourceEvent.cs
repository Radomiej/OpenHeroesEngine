using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events
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