using System;
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
            var connectionPlayer = PlayerManager.Instance.FindPlayerByConnectionId(loginPlayerEvent.ConnectionId);
            var player = PlayerManager.Instance.FindPlayerById(loginPlayerEvent.PlayerId);
            if(player == null) player = CreatePlayer(loginPlayerEvent, connectionPlayer);
            else // Merge to old player
            {
                PlayerManager.Instance.RemovePlayer(connectionPlayer);
                player.PlayerWsService = connectionPlayer.PlayerWsService;
                player.ConnectionId = connectionPlayer.ConnectionId;
            }

            var yourPlayerEvent = new YourPlayerEvent(player);
            player.PlayerWsService.SendPrivate(yourPlayerEvent);


            var createPlayerEvent = new CreatePlayerEvent(player);
            QueueEvents.Instance.Add(createPlayerEvent);
        }

        private JPlayer CreatePlayer(LoginPlayerEvent loginPlayerEvent, JPlayer connectionPlayer)
        {
            connectionPlayer.Id = Guid.NewGuid().ToString();
            connectionPlayer.Name = loginPlayerEvent.Name;
            return connectionPlayer;
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