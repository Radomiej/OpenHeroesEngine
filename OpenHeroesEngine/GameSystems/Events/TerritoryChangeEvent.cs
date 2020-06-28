using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.GameSystems.Events
{
    public class TerritoryChangeEvent : IHardEvent
    {
        public readonly Point Position;
        public readonly Fraction NewOwner;

        public TerritoryChangeEvent(Point position, Fraction newOwner)
        {
            Position = position;
            NewOwner = newOwner;
        }
    }
}