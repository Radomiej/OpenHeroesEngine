using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class MoveOutEvent
    {
        public readonly Point Current, Next;
        public readonly MoveToNextEvent MoveToNextEvent;

        public MoveOutEvent(Point current, Point next, MoveToNextEvent moveToNextEvent)
        {
            this.Current = current;
            this.Next = next;
            MoveToNextEvent = moveToNextEvent;
        }
    }
}