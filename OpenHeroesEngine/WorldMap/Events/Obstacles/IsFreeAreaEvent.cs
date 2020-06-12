using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events.Obstacles
{
    public class IsFreeAreaEvent : IFindEvent
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