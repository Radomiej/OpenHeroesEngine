using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Events.Structures;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Api
{
    public class StructureApi
    {
        public static bool IsEntrance(Point tilePosition, JEventBus eventBus = null)
        {
            if (eventBus == null) eventBus = JEventBus.GetDefault();
            
            FindEntranceEvent findEntranceEvent = new FindEntranceEvent(tilePosition);
            BaseApi.SendEvent(eventBus, findEntranceEvent);
            return findEntranceEvent.Result != null;
        }

    }
}