using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.GameSystems.Events
{
    public class FindTerritoryOwnerEvent
    {
        public readonly Point Position;
        public bool Success = false;
        public Fraction Owner;

        public FindTerritoryOwnerEvent(Point position)
        {
            Position = position;
        }
    }
}