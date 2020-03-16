using System;
using Artemis;
using Artemis.Blackboard;
using Artemis.System;
using OpenHeroesEngine.WorldMap.Events;
using Radomiej.JavityBus;

namespace OpenHeroesEngine
{
    public class GenericOpenHeroesRunner
    {
        public static GenericOpenHeroesRunner CreateInstance()
        {
            return new GenericOpenHeroesRunner();
        }

        protected EntityWorld entityWorld;
        public GenericOpenHeroesRunner()
        {
            EntitySystem.BlackBoard.SetEntry("EventBus", JEventBus.GetDefault());
            entityWorld = new EntityWorld(false, true, true) {PoolCleanupDelay = 1};
            JEventBus.GetDefault().Post(new CoreLoadedEvent());
        }

        public void Draw()
        {
            entityWorld.Draw();
        }

        public void Update()
        {
            entityWorld.Update(1000);
        }
    }
}