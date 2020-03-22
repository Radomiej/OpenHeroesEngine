using Artemis;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.AI.Decisions
{
    public interface IDecisionThinker
    {
        public void Think(Entity thinker, JEventBus eventBus);
    }
}