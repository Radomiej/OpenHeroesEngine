using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenHeroesEngine;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.MapReader;
using Radomiej.JavityBus;

namespace TestOpenHeroesEngine.WorldLoader
{
    public class TestLoadWorldFromHomm3Json
    {
        
        [SetUp]
        public void Setup()
        {
            JEventBus.GetDefault().ClearAll();
        }
        
        [Test]
        public void LoadSimple()
        {
            using (StreamReader r = new StreamReader("Resources/wings of war.h3m.json"))
            {
                string json = r.ReadToEnd();
                Homm3Map items = JsonConvert.DeserializeObject<Homm3Map>(json);
                GenerateMap(items);
            }
        }

        private void GenerateMap(Homm3Map map)
        {
            Homm3MapLoader mapLoader = new Homm3MapLoader(map);
            var runner = GenericOpenHeroesRunner.CreateInstance(mapLoader);
         
            for (int i = 0; i < 1000; i++)
            {
                runner.Draw();
                runner.Update();
            }
         
        }
    }
}