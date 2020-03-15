using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.Game.Events;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.Game.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class MovementSystem : EventBasedSystem
    {
        [Subscribe]
        public void MoveToNextListener(MoveToNextEvent moveToNextEvent)
        {
            Point current = moveToNextEvent.CalculatedPath[moveToNextEvent.CurrentIndex];
            Point next = moveToNextEvent.CalculatedPath[moveToNextEvent.CurrentIndex + 1];
            
            
        }
    }
}