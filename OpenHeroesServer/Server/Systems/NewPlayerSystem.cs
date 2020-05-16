using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using OpenHeroesServer.Server.Events;
using OpenHeroesServer.Server.Models;
using OpenHeroesServer.WebSocket;
using Radomiej.JavityBus;

namespace OpenHeroesServer.Server.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class NewPlayerSystem : EventBasedSystem
    {
        private TerrainLayer _terrainLayer;
        public override void LoadContent()
        {
            base.LoadContent();
            _terrainLayer = BlackBoard.GetEntry<TerrainLayer>("TerrainLayer");
        }

        
        [Subscribe]
        public void LoginPlayerListener(LoginPlayerEvent loginPlayerEvent)
        {
            var player = PlayerManager.Instance.FindPlayerByConnectionId(loginPlayerEvent.connectionId);
            if (player != null) ResumePlayer(player, loginPlayerEvent);
            
            var yourPlayerEvent = new YourPlayerEvent(player);
            // Send(WsMessageBuilder.CreateWsText("private", yourPlayerEvent));
            
          
            var createPlayerEvent = new CreatePlayerEvent(player);
            // Send(WsMessageBuilder.CreateWsText("player", createPlayerEvent));
            QueueEvents.Instance.Add(createPlayerEvent); //TODO add logic
            
        }

        private void ResumePlayer(JPlayer player, LoginPlayerEvent loginPlayerEvent)
        {
            throw new System.NotImplementedException();
        }

        [Subscribe]
        public void CreatePlayerListener(CreatePlayerEvent createPlayerEvent)
        {
            var player = PlayerManager.Instance.FindPlayerByConnectionId(createPlayerEvent.hid);
            var message = WsMessageBuilder.CreateWsMessage("public", _terrainLayer); 
            _eventBus.Post(message);
        }
    }
}