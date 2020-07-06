using NUnit.Framework;
using Radomiej.JavityBus;
using TestOpenHeroesEngine.JavityBus.Events;

namespace TestOpenHeroesEngine.JavityBus.TestImplementation
{
    public class TestPriority3EventHandler
    {
        public readonly int AssertPriority = 3;

        [Subscribe(3)]
        public void TestEventListener(TestEventWithParam testEvent)
        {
            testEvent.Param++;
            Assert.AreEqual(AssertPriority, testEvent.Param);
        }
    }
}