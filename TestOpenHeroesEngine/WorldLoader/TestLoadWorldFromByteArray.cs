using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenHeroesEngine;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.MapReader;
using OpenHeroesEngine.MapReader.SimpleArray;
using Radomiej.JavityBus;

namespace TestOpenHeroesEngine.WorldLoader
{
    public class TestLoadWorldFromByteArray
    {
        
        [SetUp]
        public void Setup()
        {
            JEventBus.GetDefault().ClearAll();
        }
        
        [Test]
        public void LoadSimple()
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
            ByteArrayMapLoader mapLoader = new ByteArrayMapLoader(map);
            var runner = GenericOpenHeroesRunner.CreateInstance(mapLoader);
         
            for (int i = 0; i < 1000; i++)
            {
                runner.Draw();
                runner.Update();
            }
        }
    }
}