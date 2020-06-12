using System.Collections.Generic;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events.Fractions
{
    public class FindFractionEvent : IFindEvent
    {
        public List<Fraction> Results = new List<Fraction>();
    }
}