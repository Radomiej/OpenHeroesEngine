using NUnit.Framework;
using OpenHeroesEngine;
using OpenHeroesEngine.MapReader.SimpleArray;

namespace TestOpenHeroesEngine.WorldMap.Runners
{
    public class TestMovementSystem
    {
        private GenericOpenHeroesRunner _runner;
        
        [SetUp]
        public void Setup()
        {
            byte[,] map = {
                {1, 1, 1, 3, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 3, 1, 1, 1}
            };
            var loader = new ByteArrayMapLoader(map);
            _runner = GenericOpenHeroesRunner.CreateInstance(loader);
        }

     
        
        [Test]
        public void TestSimpleInverseGridPropertiesAndIndexMath()
        {
        }
    }
}