using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events.Fractions
{
    public class NewFractionEvent : IHardEvent
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