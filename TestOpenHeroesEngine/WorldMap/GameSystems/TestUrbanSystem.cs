using NUnit.Framework;
using OpenHeroesEngine;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.GameSystems.Events;
using OpenHeroesEngine.GameSystems.Events.Urban;
using OpenHeroesEngine.MapReader.SimpleArray;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace TestOpenHeroesEngine.WorldMap.GameSystems
{
    public class TestUrbanSystem
    {
        private GenericOpenHeroesRunner _runner;

        [SetUp]
        public void Setup()
        {
            JEventBus.GetDefault().ClearAll();
            byte[,] map =
            {
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1}
            };
            var loader = new ByteArrayMapLoader(map);
            _runner = GenericOpenHeroesRunner.CreateInstance(loader);
        }


        [Test]
        public void TestBaseFunctionality()
        {
            Fraction testFraction = new Fraction("test");
            Point testPosition = new Point(4, 4);

            CreateUrbanEvent createUrbanEvent = new CreateUrbanEvent(testPosition, 1000, 0.1f);
            JEventBus.GetDefault().Post(createUrbanEvent);

            _runner.Update();
            FindUrbanInformationEvent findUrbanInformationEvent = new FindUrbanInformationEvent(testPosition);
            JEventBus.GetDefault().Post(findUrbanInformationEvent);
            Assert.IsTrue(findUrbanInformationEvent.Success);
            Assert.AreEqual(1101, findUrbanInformationEvent.Urban.Population);

            _runner.Update();
            findUrbanInformationEvent = new FindUrbanInformationEvent(testPosition);
            JEventBus.GetDefault().Post(findUrbanInformationEvent);
            Assert.IsTrue(findUrbanInformationEvent.Success);
            Assert.AreEqual(1212, findUrbanInformationEvent.Urban.Population);
        }
    }
}