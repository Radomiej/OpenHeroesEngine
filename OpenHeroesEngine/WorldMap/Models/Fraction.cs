﻿using System.Collections.Generic;
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

        protected bool Equals(Fraction other)
        {
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Fraction) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}