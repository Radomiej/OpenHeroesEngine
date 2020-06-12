using System.Collections.Generic;
using System.Diagnostics;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Events.Resources;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class MinesGenerateResourcesSystem : EntityComponentProcessingSystem<Structure, Mine>
    {
        private JEventBus _jEventBus;
        public override void LoadContent()
        {
            _jEventBus = BlackBoard.GetEntry<JEventBus>("EventBus");
        }

        public override void Process(Entity entity, Structure structure, Mine mine)
        {
            if(structure.Fraction == null) return;
            
            Resource resource = new Resource(mine.ResourceDefinition, mine.Production);
            AddResourceToFractionEvent addResourceToFractionEvent = new AddResourceToFractionEvent(resource, structure.Fraction);
            _jEventBus.Post(addResourceToFractionEvent);
        }
    }
}