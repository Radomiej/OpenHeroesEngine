using System.Collections.Generic;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class WorldLoadedEvent
    {
        public readonly byte[,] TerrainLayer;

        public WorldLoadedEvent(byte[,] terrainLayer)
        {
            TerrainLayer = terrainLayer;
        }
    }
}