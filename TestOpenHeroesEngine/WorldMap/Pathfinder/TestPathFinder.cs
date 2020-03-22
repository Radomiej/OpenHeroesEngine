using NUnit.Framework;
using OpenHeroesEngine;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Events;
using Radomiej.JavityBus;

namespace TestOpenHeroesEngine.Game.Pathfinder
{
    public class TestPathFinder
    {
        private GenericOpenHeroesRunner _runner;
        [SetUp]
        public void Setup()
        {
            _runner = GenericOpenHeroesRunner.CreateInstance();
        }

        [Test]
        public void TestPathfinderSystem()
        {
            FindPathEvent findPathEvent = new FindPathEvent(new Point(0, 0), new Point(100, 100));
            JEventBus.GetDefault().Post(findPathEvent);
            
            Assert.IsNotNull(findPathEvent.CalculatedPath);
            Assert.IsNotEmpty(findPathEvent.CalculatedPath);
            Assert.AreEqual(101, findPathEvent.CalculatedPath.Count);
        }
        
        [Test]
        public void TestPathfinderTeleport()
        {
            PathFinder pathFinder = new PathFinder(new byte[,]
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
            pathFinder.AddTeleport(new Point(1, 1), new Point(6, 6));
            var result = pathFinder.FindPath(new Point(0, 0), new Point(7, 7));
            
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(4, result.Count);
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