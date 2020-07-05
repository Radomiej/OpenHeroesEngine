using NUnit.Framework;
using OpenHeroesEngine;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.GameSystems.Events;
using OpenHeroesEngine.GameSystems.Events.Influence;
using OpenHeroesEngine.MapReader.SimpleArray;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace TestOpenHeroesEngine.WorldMap.GameSystems
{
    public class TestInfluenceSystem
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

            CreateInfluenceCenterEvent createInfluenceCenterEvent = new CreateInfluenceCenterEvent(testPosition, 100f);
            JEventBus.GetDefault().Post(createInfluenceCenterEvent);

            _runner.Update();
        }
    }
}