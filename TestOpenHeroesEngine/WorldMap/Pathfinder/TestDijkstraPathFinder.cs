﻿using NUnit.Framework;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.Dijkstra;

namespace TestOpenHeroesEngine.WorldMap.Pathfinder
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
            DijkstraPathFinder pathFinder = new DijkstraPathFinder(new byte[,]
            {
                {1, 1, 1, 1},
                {1, 1, 1, 1},
                {1, 1, 1, 1},
                {1, 1, 1, 1}
            });
            pathFinder.ChangeCostOfMove(1, 1, 0);
            pathFinder.ChangeCostOfMove(2, 2, 0);
            var movementInfo = pathFinder.GetHexesInMovementRange(new Point(0, 0), 8);
            var result = pathFinder.Find(new Point(0, 0), new Point(3, 3), movementInfo);
            
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(5, result.Count);
        }
    }
}