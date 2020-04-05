using System.Collections.Generic;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.Utils;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class TerrainLayerSystem : EventBasedSystem
    {
        private Grid _grid;
        private TerrainLayer _terrainLayer;

        public override void LoadContent()
        {
            base.LoadContent();
            _grid = BlackBoard.GetEntry<Grid>("Grid");
            _terrainLayer = BlackBoard.GetEntry<TerrainLayer>("TerrainLayer");
        }

        [Subscribe]
        public void SetToWaterListener(SetToWaterEvent setToWaterEvent)
        {
            var position = setToWaterEvent.CellPosition;
            _terrainLayer.Terrain[position.X, position.Y] = 0;
        }
        
        [Subscribe]
        public void SetToWaterListener(SetToGroundEvent setToGroundEvent)
        {
            var position = setToGroundEvent.CellPosition;
            _terrainLayer.Terrain[position.X, position.Y] = 1;
        }
    }
}