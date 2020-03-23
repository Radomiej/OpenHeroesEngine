using System.Diagnostics;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Events;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class ResourceManagerSystem : EventBasedSystem
    {
        [Subscribe]
        public void AddResourceListener(AddResourceOnWorldMapEvent addResourceOnWorldMapEvent)
        {
            Point staticResourceSize = new Point(1, 1);
            IsFreeAreaEvent isFreeAreaEvent = new IsFreeAreaEvent(addResourceOnWorldMapEvent.Position, staticResourceSize);
            _eventBus.Post(isFreeAreaEvent);

            if (!isFreeAreaEvent.IsFree)
            {
                Debug.WriteLine("Area is blocked! " + isFreeAreaEvent);
                return;
            }
            
            Entity resource = entityWorld.CreateEntityFromTemplate("Resource", 
                addResourceOnWorldMapEvent.Resource, 
                addResourceOnWorldMapEvent.Position);
            
            PlaceObjectOnMapEvent placeObjectOnMapEvent = new PlaceObjectOnMapEvent(resource, addResourceOnWorldMapEvent.Position, staticResourceSize);
            _eventBus.Post(placeObjectOnMapEvent);
        }
        
        [Subscribe]
        public void RemoveResourceListener(RemoveResourceOnWorldMapEvent removeResourceOnWorldMapEvent)
        {
            entityWorld.DeleteEntity(removeResourceOnWorldMapEvent.Resource);
        }
    }
}