using System.Collections.Generic;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.Game.Events
{
    public class MoveToNextEvent
    {
        public readonly int CurrentIndex;
        public readonly List<Point> CalculatedPath;

        public MoveToNextEvent(List<Point> calculatedPath, int currentIndex)
        {
            CurrentIndex = currentIndex;
            CalculatedPath = calculatedPath;
        }
    }
}