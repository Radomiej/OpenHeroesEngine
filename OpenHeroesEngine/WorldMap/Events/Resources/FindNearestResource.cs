using Artemis;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events.Resources
{
    public class FindNearestResource : IFindEvent
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