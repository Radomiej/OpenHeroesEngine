using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.GameSystems.Events.Urban
{
    public class FindUrbanInformationEvent : IFindEvent
    {
        public readonly Point Position;
        public bool Success = false;
        public Fraction Owner;
        public Components.Urban Urban;

        public FindUrbanInformationEvent(Point position)
        {
            Position = position;
        }
    }
}