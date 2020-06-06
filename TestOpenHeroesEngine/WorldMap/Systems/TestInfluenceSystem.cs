using NUnit.Framework;
using NUnit.Framework.Internal.Commands;
using OpenHeroesEngine;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.MapReader;
using OpenHeroesEngine.MapReader.SimpleArray;
using OpenHeroesEngine.Utils;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Events.World;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace TestOpenHeroesEngine.Game.Pathfinder
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