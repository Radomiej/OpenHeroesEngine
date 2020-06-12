using Artemis;

namespace OpenHeroesEngine.WorldMap.Events.Resources
{
    public class RemoveResourceOnWorldMapEvent : IHardEvent
    {
        public readonly Entity Resource;

        public RemoveResourceOnWorldMapEvent(Entity resource)
        {
            Resource = resource;
        }
    }
}