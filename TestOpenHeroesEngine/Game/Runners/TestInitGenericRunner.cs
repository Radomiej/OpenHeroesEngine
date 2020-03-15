using NUnit.Framework;
using OpenHeroesEngine;

namespace TestOpenHeroesEngine.Game.Runners
{
    public class TestInitGenericRunner
    {
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