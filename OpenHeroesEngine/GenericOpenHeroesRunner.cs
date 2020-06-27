using System;
using System.Diagnostics;
using Artemis;
using Artemis.Blackboard;
using Artemis.System;
using OpenHeroesEngine.MapReader;
using OpenHeroesEngine.MapReader.SimpleArray;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Events.State;
using OpenHeroesEngine.WorldMap.Events.Time;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine
{
    public class GenericOpenHeroesRunner
    {
        public static GenericOpenHeroesRunner CreateInstance(IMapLoader mapLoader = null, EntityWorld entityWorld = null)
        {
            if(mapLoader == null) mapLoader = new ByteArrayMapLoader(ByteArrayHelper.CreateBase());
            return new GenericOpenHeroesRunner(mapLoader, entityWorld);
        }

        protected readonly JEventBus EventBus;
        protected readonly EntityWorld EntityWorld;
        protected readonly GameCalendar GameCalendar;
        private readonly IMapLoader _mapLoader;
        public bool GameEnded { get; private set; }

        public GenericOpenHeroesRunner(IMapLoader mapLoader, EntityWorld entityWorld = null)
        {
            _mapLoader = mapLoader;
            EventBus = JEventBus.GetDefault();
            GameCalendar = new GameCalendar();
            int? internalMapSize = mapLoader?.GetMapSize();
            if (!internalMapSize.HasValue) internalMapSize = 512;
            Grid grid = new Grid(internalMapSize.Value, internalMapSize.Value);
            
            EntitySystem.BlackBoard.SetEntry("EventBus", EventBus);
            EntitySystem.BlackBoard.SetEntry("Grid", grid);
            EntitySystem.BlackBoard.SetEntry("GameCalendar", GameCalendar);
            EntitySystem.BlackBoard.SetEntry("TerrainLayer", new TerrainLayer(grid));

            if (entityWorld == null)
            {
                EntityWorld = new EntityWorld(false, true, true) {PoolCleanupDelay = 1};
                LoadMap();
            }
            else
            {
                EntityWorld = entityWorld;
            }
        }

        public void LoadMap()
        {
            _mapLoader.LoadMap(EntityWorld);
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
            TurnBeforeUpdateEvent turnBeforeUpdateEvent = new TurnBeforeUpdateEvent(GameCalendar.CurrentTurn);
            JEventBus.GetDefault().Post(turnBeforeUpdateEvent);
            EntityWorld.Update(1000);
            TurnAfterUpdateEvent turnAfterUpdateEvent = new TurnAfterUpdateEvent(GameCalendar.CurrentTurn++);
            JEventBus.GetDefault().Post(turnAfterUpdateEvent);
        }

        [Subscribe]
        private void EndGameListener(EndGameEvent endGameEvent)
        {
            GameEnded = true;
        }
    }
}