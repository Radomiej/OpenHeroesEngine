using Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
namespace OpenHeroesEngine.WorldMap.Events
{
    public class RemoveResourceOnWorldMapEvent
    {
        public readonly Entity Resource;

        public RemoveResourceOnWorldMapEvent(Entity resource)
        {
            Resource = resource;
        }
    }
}