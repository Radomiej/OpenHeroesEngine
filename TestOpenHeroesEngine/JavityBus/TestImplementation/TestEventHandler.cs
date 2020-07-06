using Radomiej.JavityBus;
using TestOpenHeroesEngine.JavityBus.Events;

namespace TestOpenHeroesEngine.JavityBus.TestImplementation
{
    public class TestEventHandler
    {
        public int EventCounter = 0;
        
        [Subscribe]
        public void TestEventListener(TestEvent testEvent)
        {
            EventCounter++;
        }
    }
}