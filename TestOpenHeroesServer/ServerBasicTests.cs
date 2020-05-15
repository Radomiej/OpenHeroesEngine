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
            int freePort = GetFreePortHelper.NextFreePort();
            var basicServer = BasicServer.CreateInstance(freePort);
            basicServer.RunAsynch(new ByteArrayMapLoader(map));
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}