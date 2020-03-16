using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw)]
    public class WorldMapSystem : EventBasedSystem
    {
        private Grid _grid;

        [Subscribe]
        public void WorldLoadedListener(CoreLoadedEvent coreLoadedEvent)
        {
            _grid = new Grid(512, 512);
            BlackBoard.SetEntry("Grid", _grid);
            JEventBus.GetDefault().Post(new WorldLoadedEvent(_grid));
        }
    }
}