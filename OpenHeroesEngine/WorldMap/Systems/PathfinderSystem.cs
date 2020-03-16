using System.Linq;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class PathfinderSystem : EventBasedSystem
    {
        private PathFinder _pathFinder;
        
        [Subscribe]
        public void WorldLoadedListener(WorldLoadedEvent worldLoadedEvent)
        {
            byte[,] calculateGrid = CalculateGrid(worldLoadedEvent.Grid);
            _pathFinder = new PathFinder(calculateGrid);
        }
        private byte[,] CalculateGrid(Grid grid)
        {
            byte[,] byteGrid = new byte[grid.Width, grid.Height];
            for (int x = 0; x < byteGrid.GetLength(0); x += 1) {
                for (int y = 0; y < byteGrid.GetLength(1); y += 1)
                {
                    byteGrid[x, y] = 1;
                }
            }

            return byteGrid;
        }

        [Subscribe]
        public void FindPathListener(FindPathEvent findPathEvent)
        {
            var result = _pathFinder.FindPath(findPathEvent.Start, findPathEvent.End);
            findPathEvent.CalculatedPath = result.Select(step => new Point(step.X, step.Y)).ToList();
        }
    }
}