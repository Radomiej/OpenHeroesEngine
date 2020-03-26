using System.Diagnostics;
using Artemis;
using OpenHeroesEngine.WorldMap.AI.State;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models.Actions;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.AI.Decisions
{
    public class ArmyEncounterDecisionThinker : IDecisionThinker
    {
        private JEventBus _eventBus;

        public void Think(Entity thinker, JEventBus eventBus)
        {
            _eventBus = eventBus;
            GeoEntity geoEntity = thinker.GetComponent<GeoEntity>();
            ArmyAi armyAi = thinker.GetComponent<ArmyAi>();
            Army army = thinker.GetComponent<Army>();
            
            FindArmyInArea findStructureInArea = new FindArmyInArea(geoEntity.Position, 10);
            JEventBus.GetDefault().Post(findStructureInArea);
            
            Entity nearestEnemyArmy = null;
            foreach (var structureEntity in findStructureInArea.Results)
            {
                Army encounteredArmy = structureEntity.GetComponent<Army>();
                if(encounteredArmy.Fraction == army.Fraction) continue;
                nearestEnemyArmy = structureEntity;
                break;
            }
           
            if (nearestEnemyArmy == null)
            {
                Debug.WriteLine(army + " Skip to FindStructure");
                armyAi.ArmyStateMachine.Fire(ArmyTrigger.FindStructure);
                return;
            }
            
            GeoEntity armyPosition = nearestEnemyArmy.GetComponent<GeoEntity>();
            
            GoToEvent goToEvent = new GoToEvent(thinker, armyPosition.Position);
            Debug.WriteLine(army + " Go For Battle!: " + goToEvent.Goal);
            JEventBus.GetDefault().Post(goToEvent);
            
            armyAi.ArmyStateMachine.Fire(ArmyTrigger.FinishAction);
        }
    }
}