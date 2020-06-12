using System.Collections.Generic;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class FindPathRequestEvent
    {
        public string Id;
        public Point Start, End;
        public List<Point> CalculatedPath;

        public FindPathRequestEvent()
        {
            
        }
        public FindPathRequestEvent(Point start, Point end)
        {
            this.End = end;
            this.Start = start;
        }
    }
}