using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class AddResourceOnWorldMapEvent
    {
        public readonly Point Position;
        public readonly Resource Resource;

        public AddResourceOnWorldMapEvent(Resource resource, Point position)
        {
            Resource = resource;
            Position = position;
        }
    }
}