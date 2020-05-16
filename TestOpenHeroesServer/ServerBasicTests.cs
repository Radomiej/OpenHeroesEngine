using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenHeroesEngine;
using OpenHeroesEngine.MapReader.SimpleArray;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using OpenHeroesServer.Server;
using OpenHeroesServer.Server.Events;
using TestOpenHeroesServer.Internal;

namespace TestOpenHeroesServer
{
    public class Tests
    {
        private int freePort;
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
            freePort = GetFreePortHelper.NextFreePort(4649);
            var basicServer = BasicServer.CreateInstance(freePort);
            basicServer.RunAsynch(new ByteArrayMapLoader(map));
            Thread.Sleep(2000);
        }

        [Test]
        public void SimpleConnectionToServerTest()
        {
            var client = new OHSWebSocketClient("localhost", freePort);
            client.Connect();
            client.WaitForConnectionId();
            client.PlayerLogin("TestName", "TestToken");
            

            HashSet<Type> waitingForTypes = new HashSet<Type>();
            waitingForTypes.Add(typeof(AddArmyEvent));
            waitingForTypes.Add(typeof(AddArmyEvent));
            waitingForTypes.Add(typeof(TerrainLayer));
            for (int i = 3; i > 0; i--)
            {
                var testEvent = client.Events.Take();
                if (waitingForTypes.Contains(testEvent.GetType()))
                {
                    waitingForTypes.Remove(testEvent.GetType());
                }
            }
            if(waitingForTypes.Count == 0) Assert.Pass();
            else Assert.Fail("Not All Events Are Valid");
            
        }
    }
}