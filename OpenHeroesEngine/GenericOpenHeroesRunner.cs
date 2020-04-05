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

        protected readonly JEventBus EventBus;
        protected readonly EntityWorld EntityWorld;
        protected readonly GameCalendar GameCalendar;
        public bool GameEnded { get; private set; }

        public GenericOpenHeroesRunner(IMapLoader mapLoader = null)
        {
            EventBus = JEventBus.GetDefault();
            GameCalendar = new GameCalendar();
            int? internalMapSize = mapLoader?.GetMapSize();
            if (!internalMapSize.HasValue) internalMapSize = 512;
            
            EntitySystem.BlackBoard.SetEntry("EventBus", EventBus);
            EntitySystem.BlackBoard.SetEntry("Grid", new Grid(internalMapSize.Value, internalMapSize.Value));
            EntitySystem.BlackBoard.SetEntry("GameCalendar", GameCalendar);
            EntityWorld = new EntityWorld(false, true, true) {PoolCleanupDelay = 1};
            mapLoader?.LoadMap(EntityWorld);
            EventBus.Post(new CoreLoadedEvent());
            
            EventBus.Register(this);
        }

        public void Draw()
        {
            EntityWorld.Draw();
        }

        public void Update()
        {
            if(GameEnded) return;
            
            CanNextTurnEvent canNextTurnEvent = new CanNextTurnEvent();
            JEventBus.GetDefault().Post(canNextTurnEvent);

            if (canNextTurnEvent.ActionBlockers.Count > 0)
            {
                Debug.WriteLine("Are Actions To Finish");
                return;
            }
            TurnBeginEvent turnBeginEvent = new TurnBeginEvent(GameCalendar.CurrentTurn);
            JEventBus.GetDefault().Post(turnBeginEvent);
            EntityWorld.Update(1000);
            TurnEndEvent turnEndEvent = new TurnEndEvent(GameCalendar.CurrentTurn++);
            JEventBus.GetDefault().Post(turnEndEvent);
        }

        [Subscribe]
        private void EndGameListener(EndGameEvent endGameEvent)
        {
            GameEnded = true;
        }
    }
}