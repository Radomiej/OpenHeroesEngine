using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
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
            entityWorld.CreateEntityFromTemplate("Resource", 
                addResourceOnWorldMapEvent.Resource, 
                addResourceOnWorldMapEvent.Position);
        }
        
        [Subscribe]
        public void RemoveResourceListener(RemoveResourceOnWorldMapEvent removeResourceOnWorldMapEvent)
        {
            entityWorld.DeleteEntity(removeResourceOnWorldMapEvent.Resource);
        }
    }
}