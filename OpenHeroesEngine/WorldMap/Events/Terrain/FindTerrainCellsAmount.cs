using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events.Terrain
{
    public class FindTerrainCellsAmount : IFindEvent
    {
        public readonly byte TerrainType;
        public bool Success;
        public long Result;

        public FindTerrainCellsAmount(byte terrainType)
        {
            TerrainType = terrainType;
        }
    }
}