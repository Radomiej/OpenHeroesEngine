using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class RemoveResourceFromFractionEvent
    {
        public readonly Fraction Fraction;
        public readonly Resource Resource;
        public readonly int Dividend; // 0 - All or nothing, n > 0 - as much as possible to MAX (Dicidend is cost per unit)
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