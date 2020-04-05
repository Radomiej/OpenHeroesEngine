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

        public override void LoadContent()
        {
            base.LoadContent();
            _grid = BlackBoard.GetEntry<Grid>("Grid");
        }

        
        [Subscribe]
        public void GeoIndexListener(GeoIndexReceiverEvent geoIndexReceiverEvent)
        {
            geoIndexReceiverEvent.GeoIndex = _grid.GetNodeIndex(geoIndexReceiverEvent.PointToTranslate);
        }
    }
}