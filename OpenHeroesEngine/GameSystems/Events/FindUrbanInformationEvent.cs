using OpenHeroesEngine.AStar;
using OpenHeroesEngine.GameSystems.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.GameSystems.Events
{
    public class FindUrbanInformationEvent
    {
        public readonly Point Position;
        public bool Success = false;
        public Fraction Owner;
        public Urban Urban;

        public FindUrbanInformationEvent(Point position)
        {
            Position = position;
        }
    }
}