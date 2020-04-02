using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Models
{
    public class StructureDefinition
    {
        public readonly string Name;
        public readonly Point Size;

        public StructureDefinition(string name, Point size)
        {
            Name = name;
            Size = size;
        }
    }
}