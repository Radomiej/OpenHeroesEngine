using System;
using System.IO;
using Newtonsoft.Json;
using OpenHeroesEngine;
using OpenHeroesEngine.MapReader;
using OpenHeroesServer.WebSocket;
using Radomiej.JavityBus;

namespace OpenHeroesServer.Server
{
    public class BasicServer
    {
        public static BasicServer CreateInstance()
        {
            return new BasicServer();
        }

        private GenericOpenHeroesRunner _runner;
        public BasicServer()
        {
        }

        public void LoadSimple()
        {
            Homm3Map items = null;
            using (StreamReader r = new StreamReader("Resources/wings of war.h3m.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<Homm3Map>(json);
            }

            if (items == null) throw new NotSupportedException("Cannot load map! aborted.");
            
            GenerateMap(items);
            StartWebServer();
            RunGame();
        }

        private void RunGame()
        {
            while (!_runner.GameEnded)
            {
                var clientEvent = QueueEvents.Instance.Take();
                ProcessClientEvent(clientEvent);
            }
        }

        private void ProcessClientEvent(object clientEvent)
        {
            JEventBus.GetDefault().Post(clientEvent);
        }

        private void StartWebServer()
        {
            JavityWebSocketServer.GetInstance().Create();
            JavityWebSocketServer.GetInstance().AddWsService<PlayerWsService>("/Javity");
            JavityWebSocketServer.GetInstance().Start();
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