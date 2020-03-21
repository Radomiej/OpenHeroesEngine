using Artemis;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class PlaceObjectOnMapEvent
    {
        public readonly Entity Entity;
        public readonly Point Position, Size;
        public bool IsPlaced = false;

        public PlaceObjectOnMapEvent(Entity entity, Point position, Point size)
        {
            Entity = entity;
            Position = position;
            Size = size;
        }

        public override string ToString()
        {
            return $"{nameof(Entity)}: {Entity}, {nameof(Position)}: {Position}, {nameof(Size)}: {Size}, {nameof(IsPlaced)}: {IsPlaced}";
        }
    }
}