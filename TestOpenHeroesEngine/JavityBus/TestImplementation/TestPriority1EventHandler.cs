using NUnit.Framework;
using Radomiej.JavityBus;
using TestOpenHeroesEngine.JavityBus.Events;

namespace TestOpenHeroesEngine.JavityBus.TestImplementation
{
    public class TestPriority1EventHandler
    {
        public readonly int AssertPriority = 1;

        [Subscribe(1)]
        public void TestEventListener(TestEventWithParam testEvent)
        {
            testEvent.Param++;
            Assert.AreEqual(AssertPriority, testEvent.Param);
        }
    }
}