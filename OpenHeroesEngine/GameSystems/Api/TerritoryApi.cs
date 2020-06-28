using OpenHeroesEngine.AStar;
using OpenHeroesEngine.GameSystems.Events;
using OpenHeroesEngine.WorldMap.Api;
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
        

    }
}