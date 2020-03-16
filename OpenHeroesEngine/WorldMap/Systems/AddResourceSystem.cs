using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.Game.Components;
using OpenHeroesEngine.Game.Events;
using OpenHeroesEngine.Game.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.Game.Systems
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