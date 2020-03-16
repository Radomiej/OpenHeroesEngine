using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.WorldMap.Events;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class AddResourceSystem : EventBasedSystem
    {
        [Subscribe]
        public void AddResourceListener(AddResourceEvent addResourceEvent)
        {
            entityWorld.CreateEntityFromTemplate("Resource", 
                addResourceEvent.Resource, 
                addResourceEvent.Position);
        }
    }
}