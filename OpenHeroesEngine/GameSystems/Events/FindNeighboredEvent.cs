using System.Collections.Generic;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.GameSystems.Events
{
    public class FindNeighboredEvent
    {
        public readonly Point Center;
        public readonly int SquareRadiusSize;
        public bool Success;
        public List<Point> Neighbors;
        public HashSet<Fraction> NeighborFractions;

        public FindNeighboredEvent(Point center, int squareRadiusSize = 1)
        {
            this.Center = center;
            this.SquareRadiusSize = squareRadiusSize;
        }
    }
}