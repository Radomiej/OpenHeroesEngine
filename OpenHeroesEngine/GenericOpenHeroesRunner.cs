using System;
using System.Diagnostics;
using Artemis;
using Artemis.Blackboard;
using Artemis.System;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
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
            EntitySystem.BlackBoard.SetEntry("Grid", new Grid(512, 512));
            entityWorld = new EntityWorld(false, true, true) {PoolCleanupDelay = 1};
            JEventBus.GetDefault().Post(new CoreLoadedEvent());
        }

        public void Draw()
        {
            entityWorld.Draw();
        }

        public void Update()
        {
            PreNextTurnEvent preNextTurnEvent = new PreNextTurnEvent();
            JEventBus.GetDefault().Post(preNextTurnEvent);

            if (preNextTurnEvent.ActionBlockers.Count > 0)
            {
                Debug.WriteLine("Are Actions To Finish");
                return;
            }
            
            entityWorld.Update(1000);
        }
    }
}