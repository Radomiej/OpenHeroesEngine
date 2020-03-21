using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class IsFreeAreaEvent
    {
        public readonly Point Position, Size;
        public bool IsFree = false;

        public IsFreeAreaEvent(Point position, Point size)
        {
            Position = position;
            Size = size;
        }

        public override string ToString()
        {
            return $"{nameof(Position)}: {Position}, {nameof(Size)}: {Size}, {nameof(IsFree)}: {IsFree}";
        }
    }
}