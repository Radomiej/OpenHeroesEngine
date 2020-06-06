using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events.World
{
    public class TerritoryChangeEvent
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