using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Events.State;
using OpenHeroesEngine.WorldMap.Events.Terrain;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class TerrainLayerSystem : EventBasedSystem
    {
        public static readonly byte WaterValue = 0;
        public static readonly byte GroundValue = 1;

        private Grid _grid;
        private TerrainLayer _terrainLayer;
        private long[] _counters = new long[byte.MaxValue];

        public override void LoadContent()
        {
            base.LoadContent();
            _grid = BlackBoard.GetEntry<Grid>("Grid");
            _terrainLayer = BlackBoard.GetEntry<TerrainLayer>("TerrainLayer");
            _counters[0] = _grid.Size;
        }

        [Subscribe]
        public void WorldLoadedListener(WorldLoadedEvent worldLoadedEvent)
        {
            byte[,] mapTerrain = worldLoadedEvent.TerrainLayer;

            for (int x = 0; x < mapTerrain.GetLength(0); x += 1)
            {
                for (int y = 0; y < mapTerrain.GetLength(1); y += 1)
                {
                    ChangeTerrain(x, y, mapTerrain[x, y]);
                }
            }

            _eventBus.Post(new TerrainLayerReadyEvent(_terrainLayer));
        }

        private void ChangeTerrain(int x, int y, byte type)
        {
            byte oldType = _terrainLayer.Terrain[x, y];
            if (oldType == type) return;

            _terrainLayer.Terrain[x, y] = type;
            _counters[oldType]--;
            _counters[type]++;
        }

        [Subscribe]
        public void SetToWaterListener(SetToWaterEvent setToWaterEvent)
        {
            var position = setToWaterEvent.CellPosition;
            ChangeTerrain(position.X, position.Y, WaterValue);
        }

        [Subscribe]
        public void SetToGroundListener(SetToGroundEvent setToGroundEvent)
        {
            var position = setToGroundEvent.CellPosition;
            ChangeTerrain(position.X, position.Y, GroundValue);
        }

        [Subscribe]
        public void FindTileTypeListener(FindTileTypeEvent findTileTypeEvent)
        {
            var position = findTileTypeEvent.CellPosition;
            findTileTypeEvent.Result = _terrainLayer.Terrain[position.X, position.Y];
            findTileTypeEvent.Success = true;
        }

        [Subscribe]
        public void FindTerrainCellsAmountListener(FindTerrainCellsAmount findTerrainCellsAmount)
        {
            findTerrainCellsAmount.Success = true;
            findTerrainCellsAmount.Result = _counters[findTerrainCellsAmount.TerrainType];
        }
    }
}