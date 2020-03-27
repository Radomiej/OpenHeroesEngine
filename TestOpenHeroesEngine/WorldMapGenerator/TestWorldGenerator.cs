using NUnit.Framework;
using OpenHeroesEngine;
using OpenWorldGenerator.MapGenerator;

namespace TestOpenHeroesEngine.WorldMapGenerator
{
    public class TestWorldGenerator
    {
        [Test]
        public void TestCreateRunnerAndInvokeGameLoop()
        {
          MultiLayersGenerator multiLayersGenerator = new MultiLayersGenerator(123);
          multiLayersGenerator.Generate();
        }
    }
}