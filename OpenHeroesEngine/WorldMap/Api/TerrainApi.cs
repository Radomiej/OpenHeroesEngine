using Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events.Structures;
using OpenHeroesEngine.WorldMap.Events.Terrain;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Api
{
    public class TerrainApi
    {
        public static bool IsGround(Point tilePosition, JEventBus eventBus = null)
        {
            if (eventBus == null) eventBus = JEventBus.GetDefault();

            FindTileTypeEvent findTileTypeEvent = new FindTileTypeEvent(tilePosition);
            BaseApi.SendEvent(eventBus, findTileTypeEvent);
            return findTileTypeEvent.Result > 0;
        }

        public static bool IsWater(Point tilePosition, JEventBus eventBus = null)
        {
            FindTileTypeEvent findTileTypeEvent = new FindTileTypeEvent(tilePosition);
            BaseApi.SendEvent(eventBus, findTileTypeEvent);
            return findTileTypeEvent.Result <= 0;
        }

        public static int GetTerrainAmountsByType(byte terrainType, JEventBus eventBus = null)
        {
            FindTerrainCellsAmount findTerrainCellsAmount = new FindTerrainCellsAmount(terrainType);
            BaseApi.SendEvent(eventBus, findTerrainCellsAmount);
            return (int) findTerrainCellsAmount.Result;
        }
    }
}