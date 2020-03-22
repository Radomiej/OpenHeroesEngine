using System.Collections.Generic;
using Artemis;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class FindResourceInArea
    {
        public readonly Point Location;
        public readonly float MaxDistance;
        public readonly List<Entity> Results = new List<Entity>();

        public FindResourceInArea(Point location, float maxDistance = float.PositiveInfinity)
        {
            Location = location;
            MaxDistance = maxDistance;
        }
    }
}