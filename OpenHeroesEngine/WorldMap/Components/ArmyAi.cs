using Artemis;
using Artemis.Attributes;
using OpenHeroesEngine.WorldMap.AI.State;
using Stateless;

namespace OpenHeroesEngine.WorldMap.Components
{
    [ArtemisComponentPool(InitialSize=30,IsResizable=true, ResizeSize=5, IsSupportMultiThread=false)]
    public class ArmyAi : ComponentPoolable
    {
        public StateMachine<ArmyState, ArmyTrigger> ArmyStateMachine;
    }
}