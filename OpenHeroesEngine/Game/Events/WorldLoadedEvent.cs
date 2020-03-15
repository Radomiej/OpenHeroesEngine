using System.Collections.Generic;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.Game.Models;

namespace OpenHeroesEngine.Game.Events
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