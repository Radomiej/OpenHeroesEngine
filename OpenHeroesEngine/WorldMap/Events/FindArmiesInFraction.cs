using System.Collections.Generic;
using Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class FindArmiesInFraction
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