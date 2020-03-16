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
    }
}