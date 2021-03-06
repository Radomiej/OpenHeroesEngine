﻿using Artemis;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events.Moves
{
    public class PlaceObjectOnMapEvent : IHardEvent
    {
        public readonly Entity Entity;
        public readonly Point Position, Size;

        public PlaceObjectOnMapEvent(Entity entity, Point position, Point size)
        {
            Entity = entity;
            Position = position;
            Size = size;
        }

        public override string ToString()
        {
            return $"{nameof(Entity)}: {Entity}, {nameof(Position)}: {Position}, {nameof(Size)}: {Size}";
        }
    }
}