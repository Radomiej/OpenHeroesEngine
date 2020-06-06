using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.WorldMap.Events.World;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems.World
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class NeighboredSystem : EventBasedSystem
    {

        [Subscribe]
        public void FindNeighboredListener(FindNeighboredEvent findNeighbored)
        {
        }
    }
}