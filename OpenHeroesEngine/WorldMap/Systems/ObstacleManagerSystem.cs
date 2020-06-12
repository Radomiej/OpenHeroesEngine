using System.Diagnostics;
using System.Diagnostics.Tracing;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Events.Moves;
using OpenHeroesEngine.WorldMap.Events.Obstacles;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class ObstacleManagerSystem : EventBasedSystem
    {
        [Subscribe]
        public void AddObstacleListener(AddObstacleOnWorldMapEvent addObstacleOnWorldMapEvent)
        {
            IsFreeAreaEvent isFreeAreaEvent = new IsFreeAreaEvent(addObstacleOnWorldMapEvent.Position, addObstacleOnWorldMapEvent.Obstacle.Definition.Size);
            _eventBus.Post(isFreeAreaEvent);

            if (!isFreeAreaEvent.IsFree)
            {
                // Debug.WriteLine("Area is blocked! " + isFreeAreaEvent);
                return;
            }
            
            Entity obstacle = entityWorld.CreateEntityFromTemplate("Obstacle",
                addObstacleOnWorldMapEvent.Obstacle,
                addObstacleOnWorldMapEvent.Position);
            
            PlaceObjectOnMapEvent placeObjectOnMapEvent = new PlaceObjectOnMapEvent(obstacle, addObstacleOnWorldMapEvent.Position, addObstacleOnWorldMapEvent.Obstacle.Definition.Size);
            _eventBus.Post(placeObjectOnMapEvent);
            
        }
    }
}