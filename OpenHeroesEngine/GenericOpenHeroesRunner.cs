using System;
using Artemis;
using OpenHeroesEngine.Game.Events;
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