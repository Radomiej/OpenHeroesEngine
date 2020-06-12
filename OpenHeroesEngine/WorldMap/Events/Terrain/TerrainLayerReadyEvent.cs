using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events.Terrain
{
    public class TerrainLayerReadyEvent : IStatusEvent
    {
        public readonly TerrainLayer TerrainLayer;

        public TerrainLayerReadyEvent(TerrainLayer terrainLayer)
        {
            TerrainLayer = terrainLayer;
        }
    }
}