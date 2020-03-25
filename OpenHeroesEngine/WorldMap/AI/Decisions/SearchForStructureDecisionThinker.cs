using System.Diagnostics;
using Artemis;
using OpenHeroesEngine.WorldMap.AI.State;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models.Actions;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.AI.Decisions
{
    public class SearchForStructureDecisionThinker : IDecisionThinker
    {
        private JEventBus _eventBus;

        public void Think(Entity thinker, JEventBus eventBus)
        {
            _eventBus = eventBus;
            GeoEntity geoEntity = thinker.GetComponent<GeoEntity>();
            ArmyAi armyAi = thinker.GetComponent<ArmyAi>();
            Army army = thinker.GetComponent<Army>();
            
            FindStructureInArea findStructureInArea = new FindStructureInArea(geoEntity.Position, 10);
            JEventBus.GetDefault().Post(findStructureInArea);
            
            Entity nearestStructure = null;
            foreach (var structureEntity in findStructureInArea.Results)
            {
                Structure structure = structureEntity.GetComponent<Structure>();
                if(structure.Fraction == army.Fraction) continue;
                nearestStructure = structureEntity;
                break;
            }
           
            if (nearestStructure == null)
            {
                Debug.WriteLine(army + " Skip to FindResources");
                armyAi.ArmyStateMachine.Fire(ArmyTrigger.FindResources);
                return;
            }
            
            GeoEntity resourcePosition = nearestStructure.GetComponent<GeoEntity>();
            
            GoToEvent goToEvent = new GoToEvent(thinker, resourcePosition.Position);
            Debug.WriteLine(army + " Go For Structure: " + goToEvent.Goal);
            JEventBus.GetDefault().Post(goToEvent);
            
            armyAi.ArmyStateMachine.Fire(ArmyTrigger.FinishAction);
        }
    }
}