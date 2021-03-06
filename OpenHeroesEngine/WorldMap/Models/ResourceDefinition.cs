﻿namespace OpenHeroesEngine.WorldMap.Models
{
    public class ResourceDefinition
    {
        public readonly string Name;

        public ResourceDefinition(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}";
        }
    }
}