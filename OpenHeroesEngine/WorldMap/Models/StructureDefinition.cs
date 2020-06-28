using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Models
{
    public class StructureDefinition
    {
        public readonly string Name;
        public readonly Point Size;
        public readonly Point EntranceOffset;

        public StructureDefinition(string name, Point size, Point entranceOffset = null)
        {
            Name = name;
            Size = size;
            EntranceOffset = entranceOffset ?? new Point(0, 0);
        }
    }
}