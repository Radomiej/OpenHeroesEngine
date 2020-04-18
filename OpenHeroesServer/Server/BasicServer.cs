using System;
using System.IO;
using Newtonsoft.Json;
using OpenHeroesEngine;
using OpenHeroesEngine.MapReader;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesServer.Server.Events;
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
            JEventBus.GetDefault().Register(this);
            JEventBus.GetDefault().Register(JavityWebSocketServer.GetInstance());
            
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
            JEventBus.GetDefault().Unregister(this);
        }

        private void ProcessClientEvent(object clientEvent)
        {
            JEventBus.GetDefault().Post(clientEvent);
        }

        private void StartWebServer()
        {
            PrepareBindings();
            JavityWebSocketServer.GetInstance().Create();
            JavityWebSocketServer.GetInstance().AddWsService<PlayerWsService>("/Javity");
            JavityWebSocketServer.GetInstance().Start();
        }

        private void PrepareBindings()
        {
            WsMessageBuilder.AddBinding(typeof(CompleteTurnEvent));
            WsMessageBuilder.AddBinding("FindPathEvent",typeof(FindPathRequestEvent));
           
        }

        private void GenerateMap(Homm3Map map)
        {
            Homm3MapLoader mapLoader = new Homm3MapLoader(map);
            _runner = GenericOpenHeroesRunner.CreateInstance(mapLoader);
        }

        [Subscribe]
        public void EndTurnEvent(CompleteTurnEvent completeTurnEvent)
        {
            _runner.Update();
        }
    }
}