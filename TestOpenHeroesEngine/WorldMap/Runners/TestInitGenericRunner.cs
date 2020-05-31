using Artemis;
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
        
        [Test]
        public void TestCreateLazyRunner()
        {
            var entityWorld = new EntityWorld(false, true, false);
            var runner = GenericOpenHeroesRunner.CreateInstance(null, entityWorld);
            entityWorld.InitializeAll(true);
            for (int i = 0; i < 10; i++)
            {
                runner.Draw();
                runner.Update();
            }
        }
    }
}