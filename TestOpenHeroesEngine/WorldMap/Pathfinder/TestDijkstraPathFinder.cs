using NUnit.Framework;
using OpenHeroesEngine;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.Dijkstra;
using OpenHeroesEngine.WorldMap.Events;
using Radomiej.JavityBus;

namespace TestOpenHeroesEngine.Game.Pathfinder
{
    public class TestDijkstraPathFinder
    {

        [Test]
        public void TestPathfinder()
        {
            DijkstraPathFinder pathFinder = new DijkstraPathFinder(new byte[,]
            {
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1}
            });
            var result = pathFinder.GetHexesInMovementRange(new Point(4, 4), 3);
            
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.tileReturnPath);
            // Assert.AreEqual(4, result.Count);

            var pathfindingResult = pathFinder.Find(new Point(4, 4), new Point(6, 2), result);
        }
        
        [Test]
        public void TestPathfinderDynamicChangeCostOfMove()
        {
            PathFinder pathFinder = new PathFinder(new byte[,]
            {
                {1, 1, 1, 1},
                {1, 1, 1, 1},
                {1, 1, 1, 1},
                {1, 1, 1, 1}
            });
            pathFinder.ChangeCostOfMove(1, 1, 0);
            pathFinder.ChangeCostOfMove(2, 2, 0);
            var result = pathFinder.FindPath(new Point(0, 0), new Point(3, 3));
            
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(5, result.Count);
        }
    }
}