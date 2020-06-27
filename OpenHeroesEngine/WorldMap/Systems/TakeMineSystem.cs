using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Api;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Events.Moves;
using OpenHeroesEngine.WorldMap.Events.Structures;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class TakeMineSystem : EventBasedSystem
    {
        [Subscribe]
        public void CheckIfResourceIsToTake(MoveInEvent moveInEvent)
        {
            Point middle = moveInEvent.Current;
            FindNearestStructure findNearestStructure = new FindNearestStructure(middle, 0);
            _eventBus.Post(findNearestStructure);
            if (findNearestStructure.Nearest == null) return;
            if (moveInEvent.MoveToNextEvent.Owner.HasComponent<Army>())
            {
                Army army = moveInEvent.MoveToNextEvent.Owner.GetComponent<Army>();
                MinesApi.AddMineToFraction(findNearestStructure.Nearest, army.Fraction);
            }
        }
    }
}