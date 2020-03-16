using System.Collections.Generic;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class WorldLoadedEvent
    {
        public readonly Grid Grid;

        public WorldLoadedEvent(Grid grid)
        {
            this.Grid = grid;
        }
    }
}