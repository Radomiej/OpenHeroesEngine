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
            _grid = BlackBoard.GetEntry<Grid>("Grid");
            JEventBus.GetDefault().Post(new WorldLoadedEvent(_grid));
        }
        
        [Subscribe]
        public void GeoIndexListener(GeoIndexReceiverEvent geoIndexReceiverEvent)
        {
            geoIndexReceiverEvent.GeoIndex = _grid.GetNodeIndex(geoIndexReceiverEvent.PointToTranslate);
        }
    }
}