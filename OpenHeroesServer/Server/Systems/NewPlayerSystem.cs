using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.WorldMap.Models;
using OpenHeroesServer.Server.Events;
using OpenHeroesServer.WebSocket;

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

        public void CreatePlayerListener(CreatePlayerEvent createPlayerEvent)
        {
            var player = PlayerManager.Instance.FindPlayer(createPlayerEvent.hid);
            var message = WsMessageBuilder.CreateWsMessage("public", _terrainLayer); 
            _eventBus.Post(message);
        }
    }
}