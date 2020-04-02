using System.Collections.Generic;
using OpenHeroesEngine.WorldMap.Components;

namespace OpenHeroesEngine.WorldMap.Models
{
    public class Fraction
    {
        public readonly string Name;
        public readonly Dictionary<string, Resource> Resources = new Dictionary<string, Resource>(8);
        public readonly Dictionary<long, Structure> Structures = new Dictionary<long, Structure>(100);
        /***
         * 0 - Undiscavered
         * 1 - Memoryprint: terrain, resources and structure are visible
         * 2 - In sight: all layers are visible(include army)
         */
        public byte[,] FogOfWar;

        public Fraction(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}";
        }
    }
}