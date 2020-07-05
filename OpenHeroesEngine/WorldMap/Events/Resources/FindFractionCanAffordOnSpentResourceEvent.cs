using System.Collections.Generic;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events.Resources
{
    public class FindFractionCanAffordOnSpentResourceEvent : IFindEvent
    {
        public readonly List<Resource> Resources;
        public readonly Fraction Fraction;
        public bool Success;
        public int MaxAmount;

        public FindFractionCanAffordOnSpentResourceEvent(List<Resource> resources, Fraction fraction)
        {
            Resources = resources;
            Fraction = fraction;
        }
    }
}