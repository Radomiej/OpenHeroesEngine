using System.Collections.Generic;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.Dijkstra;
using OpenHeroesEngine.GameSystems.Components;
using OpenHeroesEngine.GameSystems.Events;
using OpenHeroesEngine.MapReader.SimpleArray;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Events.Structures;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.GameSystems.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class UrbanSystem : EntityComponentProcessingSystem<Urban, GeoEntity>
    {
        private JEventBus _eventBus;
        private Dictionary<Point, Entity> _urbans = new Dictionary<Point, Entity>();

        public override void LoadContent()
        {
            base.LoadContent();
            _eventBus = BlackBoard.GetEntry<JEventBus>("EventBus") ?? JEventBus.GetDefault();
            _eventBus.Register(this);
        }

        public override void UnloadContent()
        {
            _eventBus.Unregister(this);
        }

        [Subscribe]
        public void CreateUrbanListener(CreateUrbanEvent createUrbanEvent)
        {
            Entity resource = entityWorld.CreateEntityFromTemplate("Urban",
                createUrbanEvent.Position, createUrbanEvent.Population, createUrbanEvent.BirdsRate);
            _urbans[createUrbanEvent.Position] = resource;

            Structure structure = new Structure(new StructureDefinition("City", new Point(1, 1)));
            AddStructureOnWorldMapEvent addStructureOnWorldMap = new AddStructureOnWorldMapEvent(structure, createUrbanEvent.Position);
            _eventBus.Post(addStructureOnWorldMap);
        }

        [Subscribe]
        public void FindUrbanInformationListener(FindUrbanInformationEvent findUrbanInformationEvent)
        {
            findUrbanInformationEvent.Success = true;
            if(!_urbans.ContainsKey(findUrbanInformationEvent.Position)) return;
            
            Entity urban = _urbans[findUrbanInformationEvent.Position];
            findUrbanInformationEvent.Urban = urban.GetComponent<Urban>();
           
        }

        public override void Process(Entity entity, Urban urban, GeoEntity geoEntity)
        {
            urban.Population += (int) (urban.Population * urban.BirdsRate + 1);
            urban.Conscripts = (int) (urban.Population / 25f);
        }
    }
}