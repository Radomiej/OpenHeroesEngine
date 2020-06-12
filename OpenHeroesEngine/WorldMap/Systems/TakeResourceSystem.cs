using System.Collections.Generic;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Events.Moves;
using OpenHeroesEngine.WorldMap.Events.Resources;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class TakeResourceSystem : EventBasedSystem
    {
        [Subscribe]
        public void CheckIfResourceIsToTake(MoveInEvent moveInEvent)
        {
            Point middle = moveInEvent.Current;
            FindNearestResource findNearestResource = new FindNearestResource(middle, 0);
            _eventBus.Post(findNearestResource);
            if (findNearestResource.Nearest == null) return;
            if (moveInEvent.MoveToNextEvent.Owner.HasComponent<Army>())
            {
                Army army = moveInEvent.MoveToNextEvent.Owner.GetComponent<Army>();
                AddResourceToFraction(findNearestResource.Nearest, army);
            }
        }

        private void AddResourceToFraction(Entity resourceEntity, Army army)
        {
            Resource resource = resourceEntity.GetComponent<Resource>();
            AddResourceToFractionEvent addResourceToFractionEvent = new AddResourceToFractionEvent(resource, army.Fraction);
            _eventBus.Post(addResourceToFractionEvent);
            
            RemoveResourceOnWorldMapEvent removeResourceOnWorldMapEvent = new RemoveResourceOnWorldMapEvent(resourceEntity);
            _eventBus.Post(removeResourceOnWorldMapEvent);
        }

    }
}