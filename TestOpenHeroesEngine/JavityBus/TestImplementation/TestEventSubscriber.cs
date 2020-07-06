using System;
using NUnit.Framework.Internal;
using Radomiej.JavityBus;
using TestOpenHeroesEngine.JavityBus.Events;
using Logger = OpenHeroesEngine.Logger.Logger;

namespace TestOpenHeroesEngine.JavityBus.TestImplementation
{
    public class TestEventSubscriber : IRawSubscriber
    {
        public int EventCounter = 0;
        public void SubscribeRaw(object incomingEvent)
        {
           Logger.Debug($"Incoming event {incomingEvent}");
           EventCounter++;
        }

        public Type GetEventType()
        {
            return typeof(TestEvent);
        }

        public int GetPriority()
        {
            return 0;
        }
    }
}