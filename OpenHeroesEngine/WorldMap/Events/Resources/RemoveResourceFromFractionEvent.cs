using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events.Resources
{
    public class RemoveResourceFromFractionEvent : ISoftEvent
    {
        public readonly Fraction Fraction;
        public readonly Resource Resource;
        public readonly int Dividend; // 0 - All or nothing, n > 0 - as much as possible to MAX (Dividend is cost per unit)
        public bool Success;
        public int CountOfDividend;
        
        public RemoveResourceFromFractionEvent(Resource resource, Fraction fraction, int dividend = 0)
        {
            Resource = resource;
            Fraction = fraction;
            Dividend = dividend;
        }
    }
}