using System.Collections.Generic;
using Artemis;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class FindNearestResource
    {
        public readonly Point Location;
        public readonly float MaxDistance;
        public Entity Nearest;
        public float Distance;

        public FindNearestResource(Point location, float maxDistance = float.PositiveInfinity)
        {
            Location = location;
            MaxDistance = maxDistance;
        }
    }
}