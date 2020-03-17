using System.Collections.Generic;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class MoveToNextEvent
    {
        public readonly int CurrentIndex;
        public readonly List<Point> CalculatedPath;
        public float MovementCost;

        public MoveToNextEvent(List<Point> calculatedPath, int currentIndex = 0)
        {
            CurrentIndex = currentIndex;
            CalculatedPath = calculatedPath;
        }
    }
}