using System.Collections.Generic;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class FindPathEvent
    {
        public readonly Point Start, End;
        public List<Point> CalculatedPath;

        public FindPathEvent(Point start, Point end)
        {
            this.End = end;
            this.Start = start;
        }
    }
}