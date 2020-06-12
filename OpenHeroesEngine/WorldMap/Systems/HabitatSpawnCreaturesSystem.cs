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
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class HabitatSpawnCreaturesSystem : EntityComponentProcessingSystem<Structure, Habitat>
    {
        private JEventBus _jEventBus;
        private GameCalendar _gameCalendar;
        public override void LoadContent()
        {
            _jEventBus = BlackBoard.GetEntry<JEventBus>("EventBus");
            _gameCalendar = BlackBoard.GetEntry<GameCalendar>("GameCalendar");
        }

        public override void Process(Entity entity, Structure structure, Habitat habitat)
        {
            if(_gameCalendar.CurrentTurn % 7 != 0) return;
            habitat.Current += habitat.Production;
        }
    }
}