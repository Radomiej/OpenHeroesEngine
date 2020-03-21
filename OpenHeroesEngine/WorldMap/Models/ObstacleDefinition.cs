using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Models
{
    public class ObstacleDefinition
    {
        public readonly string Name;
        public readonly Point Size;

        public ObstacleDefinition(string name, Point size)
        {
            Name = name;
            Size = size;
        }
    }
}