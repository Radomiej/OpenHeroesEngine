using System.Collections.Generic;
using OpenHeroesEngine.WorldMap.Components;

namespace OpenHeroesEngine.WorldMap.Models
{
    public class Fraction
    {
        public readonly Dictionary<string, Resource> Resources = new Dictionary<string, Resource>(8);
    }
}