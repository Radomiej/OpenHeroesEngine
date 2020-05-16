using System.Threading;
using NUnit.Framework;
using OpenHeroesEngine;
using OpenHeroesEngine.MapReader.SimpleArray;
using OpenHeroesServer.Server;
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
            freePort = GetFreePortHelper.NextFreePort();
            var basicServer = BasicServer.CreateInstance(freePort);
            basicServer.RunAsynch(new ByteArrayMapLoader(map));
            Thread.Sleep(2000);
        }

        [Test]
        public void SimpleConnectionToServerTest()
        {
            var client = new OHSWebSocketClient("localhost", freePort);
            client.Connect();
            client.PlayerLogin("TestName", "TestToken");
        }
    }
}