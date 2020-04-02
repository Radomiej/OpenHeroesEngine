using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class AddStructureOnWorldMapEvent
    {
        public readonly Point Position;
        public readonly Structure Structure;

        public AddStructureOnWorldMapEvent(Structure structure, Point position)
        {
            Structure = structure;
            Position = position;
        }
    }
}