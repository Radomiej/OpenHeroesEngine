using NUnit.Framework;
using OpenHeroesEngine;
using Radomiej.JavityBus;

namespace TestOpenHeroesEngine.Game.Runners
{
    public class TestInitGenericRunner
    {
        [SetUp]
        public void Setup()
        {
            JEventBus.GetDefault().ClearAll();
        }
        
        [Test]
        public void TestCreateRunnerAndInvokeGameLoop()
        {
            var runner = GenericOpenHeroesRunner.CreateInstance();
            for (int i = 0; i < 1000; i++)
            {
                runner.Draw();
                runner.Update();
            }
        }
    }
}