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
        public void WorldLoadedListener(WorldLoadedEvent worldLoadedEvent)
        {
            byte[,] mapTerrain = worldLoadedEvent.TerrainLayer;
            for (int x = 0; x < mapTerrain.GetLength(0); x += 1)
            {
                for (int y = 0; y < mapTerrain.GetLength(1); y += 1)
                {
                    _terrainLayer.Terrain[x, y] = worldLoadedEvent.TerrainLayer[x, y];
                }
            }
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