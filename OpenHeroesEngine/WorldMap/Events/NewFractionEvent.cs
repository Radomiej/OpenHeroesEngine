using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class NewFractionEvent
    {
        public readonly Fraction NewFraction;
        public readonly Point Position;

        public NewFractionEvent(Fraction newFraction, Point position)
        {
            NewFraction = newFraction;
            Position = position;
        }
    }
}