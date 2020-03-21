using System.Collections.Generic;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using OpenHeroesEngine.WorldMap.Models.Blockers;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class DynamicActionSystem : EventBasedSystem
    {
        private readonly Dictionary<string, DynamicActionEvent> _onGoingActions = new Dictionary<string, DynamicActionEvent>();
            
        [Subscribe]
        public void AddActionToHandleListener(DynamicActionEvent dynamicActionEvent)
        {
            _onGoingActions.Add(dynamicActionEvent.Id, dynamicActionEvent);
        }

        [Subscribe]
        private void AnswerToActionListener(ActionResponseEvent actionResponseEvent)
        {
            _onGoingActions.Remove(actionResponseEvent.ActionEvent.Id);
        }
        
        [Subscribe]
        private void PreNextTurnListener(PreNextTurnEvent preNextTurnEvent)
        {
            if(_onGoingActions.Count == 0) return;

            foreach (var action in _onGoingActions.Values)
            {
                preNextTurnEvent.AddTurnBlocker(new FinishActionBlocker(action));
            }
        }
    }
}