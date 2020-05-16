using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using OpenHeroesEngine;
using OpenHeroesEngine.MapReader;
using OpenHeroesEngine.MapReader.SimpleArray;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using OpenHeroesServer.Server.Events;
using OpenHeroesServer.WebSocket;
using Radomiej.JavityBus;

namespace OpenHeroesServer.Server
{
    public class BasicServer
    {
        private int _port;
        private JEventBus _eventBus;

        public static BasicServer CreateInstance(int port = 4649)
        {
            return new BasicServer(port);
        }

        private GenericOpenHeroesRunner _runner;

        public BasicServer(int port, JEventBus eventBus = null)
        {
            _eventBus ??= JEventBus.GetDefault();
            _port = port;
        }

        public void RunAsynch(IMapLoader mapLoader = null)
        {
            Thread thread1 = new Thread(() => Run(mapLoader));
            thread1.Start();
        }

        public void Run(IMapLoader mapLoader = null)
        {
            _eventBus.Register(this);
            _eventBus.Register(JavityWebSocketServer.GetInstance());

            mapLoader ??= LoadClassicMap();
            GenerateMap(mapLoader);
            StartWebServer();
            RunGame();
        }

        private IMapLoader LoadClassicMap()
        {
            Homm3Map items = null;
            using (StreamReader r = new StreamReader("Resources/wings of war.h3m.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<Homm3Map>(json);
            }

            if (items == null) throw new NotSupportedException("Cannot load map! aborted.");

            return new Homm3MapLoader(items);
        }

        private void RunGame()
        {
            while (!_runner.GameEnded)
            {
                var clientEvent = QueueEvents.Instance.Take();
                ProcessClientEvent(clientEvent);
            }

            _eventBus.Unregister(this);
        }

        private void ProcessClientEvent(object clientEvent)
        {
            _eventBus.Post(clientEvent);
        }

        private void StartWebServer()
        {
            PrepareBindings();
            JavityWebSocketServer.GetInstance().Create(_port);
            JavityWebSocketServer.GetInstance().AddWsService<PlayerWsService>("/Javity");
            JavityWebSocketServer.GetInstance().Start();
        }

        private void PrepareBindings()
        {
            WsMessageBuilder.AddBinding(typeof(CompleteTurnEvent));
            WsMessageBuilder.AddBinding("FindPathEvent", typeof(FindPathRequestEvent));
            WsMessageBuilder.AddBinding("LoginPlayerEvent", typeof(LoginPlayerEvent));
            WsMessageBuilder.AddBinding(typeof(LoginPlayerEvent));
            WsMessageBuilder.AddBinding("AddArmyEvent", typeof(AddArmyEvent));
            WsMessageBuilder.AddBinding(typeof(AddArmyEvent));
            WsMessageBuilder.AddBinding(typeof(TerrainLayer));
        }

        private void GenerateMap(IMapLoader mapLoader)
        {
            _runner = GenericOpenHeroesRunner.CreateInstance(mapLoader);
        }

        [Subscribe]
        public void EndTurnEvent(CompleteTurnEvent completeTurnEvent)
        {
            _runner.Update();
        }
    }
}