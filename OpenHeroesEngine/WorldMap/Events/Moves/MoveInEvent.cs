using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events.Moves
{
    public class MoveInEvent : IStatusEvent
    {
        public readonly Point Current, Previous;
        public readonly MoveToNextEvent MoveToNextEvent;

        public MoveInEvent(Point current, Point previous, MoveToNextEvent moveToNextEvent)
        {
            this.Current = current;
            this.Previous = previous;
            MoveToNextEvent = moveToNextEvent;
        }
    }
}