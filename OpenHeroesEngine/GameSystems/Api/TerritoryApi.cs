using System.Collections.Generic;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.GameSystems.Events;
using OpenHeroesEngine.GameSystems.Events.Territory;
using OpenHeroesEngine.WorldMap.Api;
using OpenHeroesEngine.WorldMap.Events.Terrain;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.GameSystems.Api
{
    public class TerritoryApi
    {
        public static void ChangeTerritoryOwner(Point tilePosition, Fraction newOwner, JEventBus eventBus = null)
        {
            if (eventBus == null) eventBus = JEventBus.GetDefault();
            
            TerritoryChangeEvent territoryChangeEvent = new TerritoryChangeEvent(tilePosition, newOwner);
            BaseApi.SendEvent(eventBus, territoryChangeEvent);
        }

        public static List<Point> GetTerritoryCells(Fraction owner, JEventBus eventBus = null)
        {
            FindTerritoriesCellEvent findTerritoriesCellEvent = new FindTerritoriesCellEvent(owner);
            BaseApi.SendEvent(eventBus, findTerritoriesCellEvent);
            return findTerritoriesCellEvent.Results ?? new List<Point>();
        }
        
        public static int GetTerritoryCellsAmount(Fraction owner, JEventBus eventBus = null)
        {
            FindTerritoriesCellEvent findTerritoriesCellEvent = new FindTerritoriesCellEvent(owner);
            BaseApi.SendEvent(eventBus, findTerritoriesCellEvent);
            return findTerritoriesCellEvent.Results?.Count ?? 0;
        }
        
        public static float GetTerritoryCellPercentAmount(Fraction owner, JEventBus eventBus = null)
        {
            int territories = GetTerritoryCellsAmount(owner, eventBus);
            int grounds = TerrainApi.GetTerrainAmountsByType(1);
            float result = (territories / (float) grounds) * 100f;
            return result;
        }
    }
}