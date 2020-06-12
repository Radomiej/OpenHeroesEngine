using Artemis;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.AI.Decisions
{
    public interface IDecisionThinker
    {
        void Think(Entity thinker, JEventBus eventBus);
    }
}