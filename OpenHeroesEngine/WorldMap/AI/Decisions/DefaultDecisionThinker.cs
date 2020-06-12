using Artemis;
using OpenHeroesEngine.WorldMap.Events.Actions;
using OpenHeroesEngine.WorldMap.Models.Actions;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.AI.Decisions
{
    public class DefaultDecisionThinker : IDecisionThinker
    {
        private JEventBus _eventBus;
        

        public void Think(Entity thinker, JEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        [Subscribe]
        public void AddActionToHandleListener(DynamicActionEvent dynamicActionEvent)
        {
            //TODO add more complex way to take actions
            ActionAnswer actionAnswer = dynamicActionEvent.ActionAnswers[0];
            ActionResponseEvent actionResponseEvent = new ActionResponseEvent(dynamicActionEvent, actionAnswer);
            _eventBus.Post(actionResponseEvent);
        }
    }
}