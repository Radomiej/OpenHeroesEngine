using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events.Resources
{
    public class AddResourceOnWorldMapEvent : IHardEvent
    {
        public readonly Point Position;
        public readonly Components.Resource Resource;

        public AddResourceOnWorldMapEvent(Components.Resource resource, Point position)
        {
            Resource = resource;
            Position = position;
        }
    }
}