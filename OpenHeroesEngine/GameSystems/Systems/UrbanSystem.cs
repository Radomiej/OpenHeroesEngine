using Artemis;
using Artemis.System;
using OpenHeroesEngine.Dijkstra;
using OpenHeroesEngine.GameSystems.Components;
using OpenHeroesEngine.GameSystems.Events;
using OpenHeroesEngine.MapReader.SimpleArray;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.GameSystems.Systems
{
    public class UrbanSystem : EntityComponentProcessingSystem<Urban, GeoEntity>
    {
        private DijkstraPathFinder _dijkstraPathFinder;
        private JEventBus _eventBus;

        public override void LoadContent()
        {
            base.LoadContent();
            _eventBus = BlackBoard.GetEntry<JEventBus>("EventBus") ?? JEventBus.GetDefault();
            _eventBus.Register(this);

            var _grid = BlackBoard.GetEntry<Grid>("Grid");
            _dijkstraPathFinder = new DijkstraPathFinder(ByteArrayHelper.CreateBase(_grid.Width));
        }

        public override void UnloadContent()
        {
            _eventBus.Unregister(this);
        }

        [Subscribe]
        public void CreateUrbanListener(CreateInfluenceCenterEvent createInfluenceCenter)
        {
            Entity resource = entityWorld.CreateEntityFromTemplate("Influence",
                createInfluenceCenter.Center, createInfluenceCenter.PropagationValue);
        }

        public override void Process(Entity entity, Urban urban, GeoEntity geoEntity)
        {
            
        }
    }

}