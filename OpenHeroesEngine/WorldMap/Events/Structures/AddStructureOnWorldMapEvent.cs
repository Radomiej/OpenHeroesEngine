using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;

namespace OpenHeroesEngine.WorldMap.Events.Structures
{
    public class AddStructureOnWorldMapEvent : IHardEvent
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