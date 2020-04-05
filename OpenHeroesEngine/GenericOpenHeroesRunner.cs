using System;
using System.Diagnostics;
using Artemis;
using Artemis.Blackboard;
using Artemis.System;
using OpenHeroesEngine.MapReader;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine
{
    public class GenericOpenHeroesRunner
    {
        public static GenericOpenHeroesRunner CreateInstance(IMapLoader mapLoader = null)
        {
            return new GenericOpenHeroesRunner(mapLoader);
        }

        protected EntityWorld entityWorld;
        protected GameCalendar GameCalendar;

        public GenericOpenHeroesRunner(IMapLoader mapLoader = null)
        {
            GameCalendar = new GameCalendar();
            int? internalMapSize = mapLoader?.GetMapSize();
            if (!internalMapSize.HasValue) internalMapSize = 512;
            
            EntitySystem.BlackBoard.SetEntry("EventBus", JEventBus.GetDefault());
            EntitySystem.BlackBoard.SetEntry("Grid", new Grid(internalMapSize.Value, internalMapSize.Value));
            EntitySystem.BlackBoard.SetEntry("GameCalendar", GameCalendar);
            entityWorld = new EntityWorld(false, true, true) {PoolCleanupDelay = 1};
            mapLoader?.LoadMap(entityWorld);
            JEventBus.GetDefault().Post(new CoreLoadedEvent());
        }

        public void Draw()
        {
            entityWorld.Draw();
        }

        public void Update()
        {
            CanNextTurnEvent canNextTurnEvent = new CanNextTurnEvent();
            JEventBus.GetDefault().Post(canNextTurnEvent);

            if (canNextTurnEvent.ActionBlockers.Count > 0)
            {
                Debug.WriteLine("Are Actions To Finish");
                return;
            }
            TurnBeginEvent turnBeginEvent = new TurnBeginEvent(GameCalendar.CurrentTurn);
            JEventBus.GetDefault().Post(turnBeginEvent);
            entityWorld.Update(1000);
            TurnEndEvent turnEndEvent = new TurnEndEvent(GameCalendar.CurrentTurn++);
            JEventBus.GetDefault().Post(turnEndEvent);
        }
    }
}