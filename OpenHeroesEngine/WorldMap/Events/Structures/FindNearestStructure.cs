using Artemis;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events.Structures
{
    public class FindNearestStructure : IFindEvent
    {
        public readonly Point Location;
        public readonly float MaxDistance;
        public Entity Nearest;
        public float Distance;

        public FindNearestStructure(Point location, float maxDistance = float.PositiveInfinity)
        {
            Location = location;
            MaxDistance = maxDistance;
        }
    }
}