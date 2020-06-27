using System.Collections.Generic;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;

namespace OpenHeroesEngine.WorldMap.Events.Structures
{
    public class AddStructureOnWorldMapEvent : IHardEvent
    {
        public readonly Point Position;
        public readonly Structure Structure;
        public readonly Dictionary<string, object> Params = new Dictionary<string, object>();

        public AddStructureOnWorldMapEvent(Structure structure, Point position)
        {
            Structure = structure;
            Position = position;
        }
    }
}