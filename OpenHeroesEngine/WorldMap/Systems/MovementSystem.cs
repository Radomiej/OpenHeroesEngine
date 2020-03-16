using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
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
            MoveInEvent moveInEvent = new MoveInEvent(moveOutEvent.Next, moveOutEvent.Current, moveOutEvent.MoveToNextEvent);
            JEventBus.GetDefault().Post(moveInEvent);
        }

        [Subscribe]
        public void MoveInListener(MoveInEvent moveOutEvent)
        {
            //Simulate re-thinking movement(for AI or Human to cancel movement action) - in this case just go to next
            MoveToNextEvent moveToNextEvent = new MoveToNextEvent(moveOutEvent.MoveToNextEvent.CalculatedPath, moveOutEvent.MoveToNextEvent.CurrentIndex + 1);
            JEventBus.GetDefault().Post(moveToNextEvent);
        }
    }
}