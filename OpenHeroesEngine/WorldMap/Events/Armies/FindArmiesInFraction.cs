using System.Collections.Generic;
using Artemis;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events.Armies
{
    public class FindArmiesInFraction : IFindEvent
    {
        public readonly Fraction Fraction;
        public readonly List<Entity> Results = new List<Entity>();
        public bool Success;

        public FindArmiesInFraction(Fraction fraction)
        {
            Fraction = fraction;
        }
    }
}