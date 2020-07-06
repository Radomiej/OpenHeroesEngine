using Artemis;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events.Structures
{
    public class FindEntranceEvent
    {
        public readonly Point Location;
        public bool Success;
        public Entity Result;

        public FindEntranceEvent(Point location)
        {
            Location = location;
        }
    }
}