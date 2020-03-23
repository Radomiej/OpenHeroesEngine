using System.Collections.Generic;
using OpenHeroesEngine.WorldMap.Components;

namespace OpenHeroesEngine.WorldMap.Models
{
    public class Fraction
    {
        public readonly string Name;
        public readonly Dictionary<string, Resource> Resources = new Dictionary<string, Resource>(8);
        public readonly Dictionary<long, Structure> Structures = new Dictionary<long, Structure>(100);

        public Fraction(string name)
        {
            Name = name;
        }

    }
}