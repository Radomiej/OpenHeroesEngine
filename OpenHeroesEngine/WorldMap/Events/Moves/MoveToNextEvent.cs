using System.Collections.Generic;
using Artemis;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events.Moves
{
    public class MoveToNextEvent : IStatusEvent
    {
        public readonly int CurrentIndex;
        public readonly List<Point> CalculatedPath;
        public readonly Entity Owner;
        public float MovementCost;

        public MoveToNextEvent(List<Point> calculatedPath, Entity owner, int currentIndex = 0)
        {
            CurrentIndex = currentIndex;
            CalculatedPath = calculatedPath;
            Owner = owner;
        }
    }
}