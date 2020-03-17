using System.Diagnostics;
using Artemis;
using Artemis.Attributes;
using Artemis.Blackboard;
using Artemis.Manager;
using Artemis.System;
using OpenHeroesEngine.WorldMap.AI;
using OpenHeroesEngine.WorldMap.AI.State;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using Radomiej.JavityBus;
using Stateless;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class ArmyAiSystem : EntityProcessingSystem
    {
        public override void OnAdded(Entity entity)
        {
            var armyAi = entity.GetComponent<ArmyAi>();
            var armyStateMachine = new StateMachine<ArmyState, ArmyTrigger>(ArmyState.Idle);
            armyAi.ArmyStateMachine = armyStateMachine;
            
            armyStateMachine.Configure(ArmyState.Idle).Permit(ArmyTrigger.GoTo, ArmyState.TakePosition);
            armyStateMachine.Configure(ArmyState.Idle).Permit(ArmyTrigger.FindResources, ArmyState.SearchForResource);
            armyStateMachine.Configure(ArmyState.SearchForResource).OnEntry(t => GoToNearestResource(entity));
            armyStateMachine.Configure(ArmyState.SearchForResource).PermitReentry(ArmyTrigger.FindResources);
            
            armyStateMachine.Configure(ArmyState.SearchForResource).Permit(ArmyTrigger.FinishAction, ArmyState.Idle);
        }

        private void GoToNearestResource(Entity entity)
        {
            GeoEntity geoEntity = entity.GetComponent<GeoEntity>();
            ArmyAi armyAi = entity.GetComponent<ArmyAi>();
            
            FindNearestResource findNearestResource = new FindNearestResource(geoEntity.Position);
            JEventBus.GetDefault().Post(findNearestResource);
            
            Entity nearestResource = findNearestResource.Nearest;
            if (nearestResource == null)
            {
                armyAi.ArmyStateMachine.Fire(ArmyTrigger.FinishAction);
                return;
            }
           
            GeoEntity resourcePosition = nearestResource.GetComponent<GeoEntity>();
            
            GoToEvent goToEvent = new GoToEvent(entity, resourcePosition.Position);
            JEventBus.GetDefault().Post(goToEvent);
            
            armyAi.ArmyStateMachine.Fire(ArmyTrigger.FinishAction);
        }

        public ArmyAiSystem() : base(Aspect.All(typeof(Army), typeof(GeoEntity), typeof(ArmyAi)))
        {
          
        }

        public override void Process(Entity entity)
        {
            Army army = entity.GetComponent<Army>();
            GeoEntity geoEntity = entity.GetComponent<GeoEntity>();
            ArmyAi armyAi = entity.GetComponent<ArmyAi>();
            armyAi.ArmyStateMachine.Fire(ArmyTrigger.FindResources);
            // Debug.WriteLine("Update AI");
        }
    }
}