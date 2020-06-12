using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenHeroesEngine;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.MapReader.SimpleArray;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using OpenHeroesServer.Server;
using OpenHeroesServer.Server.Events;
using TestOpenHeroesServer.Internal;

namespace TestOpenHeroesServer
{
    public class ServerCommandsTests
    {
        private int freePort;

        private OHSWebSocketClient _client;
        private string playerId;
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
            _client = new OHSWebSocketClient("localhost", freePort);
            _client.Connect();
            _client.WaitForConnectionId();
            _client.PlayerLogin("TestName");
            playerId = _client.PlayerInfo.Id;
        }

        [Test]
        public void FindPathRequestEventTest()
        {
            FindPathRequestEvent findPathRequestEvent = new FindPathRequestEvent(new Point(1, 1), new Point(5, 5));
            FindPathRequestEvent response =_client.SendAndWaitForEvent<FindPathRequestEvent>(findPathRequestEvent);
            Assert.IsNotNull(response.CalculatedPath);
            Assert.IsNotEmpty(response.CalculatedPath);
        }
    }
}