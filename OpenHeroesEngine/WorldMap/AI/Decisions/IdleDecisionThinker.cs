using Artemis;
using OpenHeroesEngine.WorldMap.AI.State;
using OpenHeroesEngine.WorldMap.Components;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.AI.Decisions
{
    public class IdleDecisionThinker : IDecisionThinker
    {
        public void Think(Entity thinker, JEventBus eventBus)
        {
            thinker.GetComponent<ArmyAi>().ArmyStateMachine.Fire(ArmyTrigger.FindEnemy);
        }
    }
}