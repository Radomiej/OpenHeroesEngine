using System.Diagnostics;
using Artemis;
using OpenHeroesEngine.WorldMap.AI.State;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models.Actions;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.AI.Decisions
{
    public class SearchForResourceDecisionThinker : IDecisionThinker
    {
        private JEventBus _eventBus;

        public void Think(Entity thinker, JEventBus eventBus)
        {
            _eventBus = eventBus;
            GeoEntity geoEntity = thinker.GetComponent<GeoEntity>();
            ArmyAi armyAi = thinker.GetComponent<ArmyAi>();
            Army army = thinker.GetComponent<Army>();
            
            FindNearestResource findNearestResource = new FindNearestResource(geoEntity.Position);
            JEventBus.GetDefault().Post(findNearestResource);
            
            Entity nearestResource = findNearestResource.Nearest;
            if (nearestResource == null)
            {
                Debug.WriteLine(army + " Skip to IDLE");
                armyAi.ArmyStateMachine.Fire(ArmyTrigger.FinishAction);
                return;
            }
           
            GeoEntity resourcePosition = nearestResource.GetComponent<GeoEntity>();
            
            GoToEvent goToEvent = new GoToEvent(thinker, resourcePosition.Position);
            Debug.WriteLine(army + " Go For Resource: " + goToEvent.Goal);
            JEventBus.GetDefault().Post(goToEvent);
            
            armyAi.ArmyStateMachine.Fire(ArmyTrigger.FinishAction);
        }
    }
}