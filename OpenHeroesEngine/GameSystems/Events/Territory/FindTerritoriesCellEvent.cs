using System.Collections.Generic;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.GameSystems.Events.Territory
{
    public class FindTerritoriesCellEvent : IFindEvent
    {
        public readonly Fraction Owner;
        public bool Success = false;
        public List<Point> Results;

        public FindTerritoriesCellEvent(Fraction owner)
        {
            Owner = owner;
        }
    }
}