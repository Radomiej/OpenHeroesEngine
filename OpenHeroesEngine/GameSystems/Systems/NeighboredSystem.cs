using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.GameSystems.Events;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.GameSystems.Systems
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