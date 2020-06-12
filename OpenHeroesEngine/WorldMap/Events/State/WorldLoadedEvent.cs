namespace OpenHeroesEngine.WorldMap.Events.State
{
    public class WorldLoadedEvent : IStatusEvent
    {
        public readonly byte[,] TerrainLayer;

        public WorldLoadedEvent(byte[,] terrainLayer)
        {
            TerrainLayer = terrainLayer;
        }
    }
}