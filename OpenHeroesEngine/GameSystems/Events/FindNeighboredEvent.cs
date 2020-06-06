using System.Collections.Generic;
using System.Drawing;

namespace OpenHeroesEngine.GameSystems.Events
{
    public class FindNeighboredEvent
    {
        public Point Center;
        public int SquareRadiusSize;
        public List<Point> Neighbors;

        public FindNeighboredEvent(Point center, int squareRadiusSize = 1)
        {
            this.Center = center;
            this.SquareRadiusSize = squareRadiusSize;
        }
    }
}