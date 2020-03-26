﻿using System.Diagnostics;
using Artemis;
using Artemis.Attributes;
using Artemis.Blackboard;
using Artemis.Manager;
using Artemis.System;
using OpenHeroesEngine.WorldMap.AI;
using OpenHeroesEngine.WorldMap.AI.Decisions;
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
        private JEventBus _eventBus;

        public override void LoadContent()
        {
            base.LoadContent();
            _eventBus = BlackBoard.GetEntry<JEventBus>("EventBus");
        }

        public override void OnAdded(Entity entity)
        {
            var armyAi = entity.GetComponent<ArmyAi>();
            var armyStateMachine = new StateMachine<ArmyState, ArmyTrigger>(ArmyState.Idle);
            armyAi.ArmyStateMachine = armyStateMachine;
            
            armyStateMachine.Configure(ArmyState.Idle).Permit(ArmyTrigger.GoTo, ArmyState.TakePosition);
            armyStateMachine.Configure(ArmyState.Idle).Permit(ArmyTrigger.FindEnemy, ArmyState.SearchForEnemy);

            armyStateMachine.Configure(ArmyState.SearchForEnemy).Permit(ArmyTrigger.FindStructure, ArmyState.SearchForStructure);
            armyStateMachine.Configure(ArmyState.SearchForEnemy).Permit(ArmyTrigger.FinishAction, ArmyState.Idle);
            
            armyStateMachine.Configure(ArmyState.SearchForResource).PermitReentry(ArmyTrigger.FindResources);
            armyStateMachine.Configure(ArmyState.SearchForResource).Permit(ArmyTrigger.FinishAction, ArmyState.Idle);
           
            armyStateMachine.Configure(ArmyState.SearchForStructure).Permit(ArmyTrigger.FindResources, ArmyState.SearchForResource);
            armyStateMachine.Configure(ArmyState.SearchForStructure).Permit(ArmyTrigger.FinishAction, ArmyState.Idle);
        }

     
        public ArmyAiSystem() : base(Aspect.All(typeof(Army), typeof(GeoEntity), typeof(ArmyAi)))
        {
          
        }

        public override void Process(Entity entity)
        {
            ArmyAi armyAi = entity.GetComponent<ArmyAi>();
            
            IDecisionThinker decisionThinker = armyAi.DecisionThinkers[armyAi.ArmyStateMachine.State];
            _eventBus.Register(armyAi.DefaultDecisionThinker);
            armyAi.DefaultDecisionThinker.Think(entity, _eventBus);
            decisionThinker.Think(entity, _eventBus);
            _eventBus.Unregister(armyAi.DefaultDecisionThinker);
            // Debug.WriteLine("Update AI");
        }
    }
}