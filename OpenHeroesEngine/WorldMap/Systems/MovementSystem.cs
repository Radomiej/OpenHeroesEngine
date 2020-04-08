using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.Utils;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class MovementSystem : EventBasedSystem
    {
        [Subscribe]
        public void MoveToNextListener(MoveToNextEvent moveToNextEvent)
        {
            Point current = moveToNextEvent.CalculatedPath[moveToNextEvent.CurrentIndex];
            Point next = moveToNextEvent.CalculatedPath[moveToNextEvent.CurrentIndex + 1];

            MoveOutEvent moveOutEvent = new MoveOutEvent(current, next, moveToNextEvent);
            JEventBus.GetDefault().Post(moveOutEvent);
        }

        [Subscribe]
        public void MoveOutListener(MoveOutEvent moveOutEvent)
        {
            //Simulate drawing move army animations - in this case just skip
            MoveInEvent moveInEvent =
                new MoveInEvent(moveOutEvent.Next, moveOutEvent.Current, moveOutEvent.MoveToNextEvent);
            JEventBus.GetDefault().Post(moveInEvent);
        }

        [Subscribe]
        public void MoveInListener(MoveInEvent moveInEvent)
        {
            moveInEvent.MoveToNextEvent.Owner.GetComponent<GeoEntity>().Position = moveInEvent.Current;
            moveInEvent.MoveToNextEvent.Owner.GetComponent<Army>().MovementPoints -=
                DistanceHelper.EuclideanDistance(moveInEvent.Current, moveInEvent.Previous);
        }
    }
}