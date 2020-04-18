using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesServer.Server.Events;
using OpenHeroesServer.WebSocket;
using Radomiej.JavityBus;

namespace OpenHeroesServer.Server.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class ArmySynchronizerSystem  : EntityComponentProcessingSystem<Army, GeoEntity>
    {
        private JEventBus _jEventBus;
        public override void LoadContent()
        {
            _jEventBus = BlackBoard.GetEntry<JEventBus>("EventBus");
        }

        public override void Process(Entity entity, Army army, GeoEntity geoEntity)
        {
            ArmyChangeEvent armyChangeEvent = new ArmyChangeEvent(army, geoEntity);
            WsMessage wsMessage = WsMessageBuilder.CreateWsMessage("public", armyChangeEvent);
            _jEventBus.Post(wsMessage);
        }
    }
}