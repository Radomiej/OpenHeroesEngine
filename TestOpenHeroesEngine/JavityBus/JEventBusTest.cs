using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenHeroesEngine.Logger;
using OpenHeroesEngine.MapReader;
using Radomiej.JavityBus;
using TestOpenHeroesEngine.JavityBus.Events;
using TestOpenHeroesEngine.JavityBus.TestImplementation;

namespace TestOpenHeroesEngine.JavityBus
{
    [TestFixture]
    public class JEventBusTest
    {
        [SetUp]
        public void Setup()
        {
            JEventBus.GetDefault().ClearAll();
        }

        [Test]
        public void RegisterCustomSubscriberRaw()
        {
            var eventSubscriber = new TestEventSubscriberRaw();
            JEventBus.GetDefault().Register(this, eventSubscriber);

            var testEvent = new TestEvent();
            for (int i = 0; i < 100; i++)
            {
                JEventBus.GetDefault().Post(testEvent);
            }

            Assert.AreEqual(100, eventSubscriber.EventCounter);
        }

        [Test]
        public void RegisterCustomSubscriber()
        {
            int counter = 0;
            var subscriber = new JEventSubscriber<TestEvent>(myEvent => counter++);
            JEventBus.GetDefault().Register(this, subscriber);
            
            var testEvent = new TestEvent();
            for (int i = 0; i < 100; i++)
            {
                JEventBus.GetDefault().Post(testEvent);
            }
            Assert.AreEqual(100, counter);
        }
        
        [Test]
        public void RegisterCustomSubscriberWithPriority()
        {
            var subscriber1 = new JEventSubscriber<TestEventWithParam>(myEvent =>
            {
                myEvent.Param++;
                Assert.AreEqual(1, myEvent.Param);
            });
            var subscriber2 = new JEventSubscriber<TestEventWithParam>(myEvent =>
            {
                myEvent.Param++;
                Assert.AreEqual(2, myEvent.Param);
            }, 1);
            var subscriber3 = new JEventSubscriber<TestEventWithParam>(myEvent =>
            {
                myEvent.Param++;
                Assert.AreEqual(3, myEvent.Param);
            }, 2);
            JEventBus.GetDefault().Register(this, subscriber3);
            JEventBus.GetDefault().Register(this, subscriber2);
            JEventBus.GetDefault().Register(this, subscriber1);
            
            for (int i = 0; i < 100; i++)
            {
                var testEvent = new TestEventWithParam();
                JEventBus.GetDefault().Post(testEvent);
                Assert.AreEqual(3, testEvent.Param);
            }
       
        }
        
        [Test]
        public void RegisterClassicSubscriberWithPriority()
        {
            var subscriber1 = new TestPriority1EventHandler();
            var subscriber2 = new TestPriority2EventHandler();
            var subscriber3 = new TestPriority3EventHandler();
           
            JEventBus.GetDefault().Register(subscriber3);
            JEventBus.GetDefault().Register(subscriber2);
            JEventBus.GetDefault().Register(subscriber1);
            
            for (int i = 0; i < 100; i++)
            {
                var testEvent = new TestEventWithParam();
                JEventBus.GetDefault().Post(testEvent);
                Assert.AreEqual(3, testEvent.Param);
            }
        }
        
        [Test]
        public void RegisterClassicSubscriberWithPriority2()
        {
            var subscriber1 = new TestPriority1EventHandler();
            var subscriber2 = new TestPriority2EventHandler();
            var subscriber3 = new TestPriority3EventHandler();

            JEventBus.GetDefault().Register(subscriber1);
            JEventBus.GetDefault().Register(subscriber2);
            JEventBus.GetDefault().Register(subscriber3);

            for (int i = 0; i < 100; i++)
            {
                var testEvent = new TestEventWithParam();
                JEventBus.GetDefault().Post(testEvent);
                Assert.AreEqual(3, testEvent.Param);
            }
        }
        
        [Test]
        public void TestNormalCase()
        {
            var subscriber = new TestEventHandler();
            JEventBus.GetDefault().Register(subscriber);
            
            var testEvent = new TestEvent();
            for (int i = 0; i < 100; i++)
            {
                JEventBus.GetDefault().Post(testEvent);
            }
            Assert.AreEqual(100, subscriber.EventCounter);
        }

        [Test]
        public void PerformanceTest()
        {
            int iteration = 100000;
            var subscriber = new TestEventHandler();
            JEventBus.GetDefault().Register(subscriber);
            
            var testEvent = new TestEvent();
            long time = NanoTime();
            for (int i = 0; i < iteration; i++)
            {
                JEventBus.GetDefault().Post(testEvent);
            }

            long executionTime = NanoTime() - time;
            // Console.Out.WriteLine($"Execution time: {executionTime / 1000000}");
            Console.WriteLine($"Execution time: {executionTime / 1000000} ms | {executionTime} ns | {executionTime / iteration} ns/event" );
            // Logger.Warning($"Execution time: {executionTime / 1000000}" );
            Assert.AreEqual(iteration, subscriber.EventCounter);
        }
        
        private static long NanoTime() {
            long nano = 10000L * Stopwatch.GetTimestamp();
            nano /= TimeSpan.TicksPerMillisecond;
            nano *= 100L;
            return nano;
        }
    }
}