using System.Collections.Generic;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.GameSystems.Events.Territory
{
    public class FindNeighboredEvent : IFindEvent
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