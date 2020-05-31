using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class TerrainLayerReadyEvent
    {
        public readonly TerrainLayer TerrainLayer;

        public TerrainLayerReadyEvent(TerrainLayer terrainLayer)
        {
            TerrainLayer = terrainLayer;
        }
    }
}