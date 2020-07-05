using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.GameSystems.Events.Territory
{
    public class FindTerritoryOwnerEvent : IFindEvent
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