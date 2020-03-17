using System.Diagnostics;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class GoToSystem : EventBasedSystem
    {
        [Subscribe]
        public void GoToListener(GoToEvent goToEvent)
        {
            Entity entity = goToEvent.Entity;
            if (!entity.HasComponent<GeoEntity>())
            {
                Debug.WriteLine("GoTo ERROR: Missing GeoEntity");
                return;
            }

            GeoEntity geoEntity = entity.GetComponent<GeoEntity>();
            FindPathEvent findPathEvent = new FindPathEvent(geoEntity.Position, goToEvent.Goal);
            JEventBus.GetDefault().Post(findPathEvent);
            if (findPathEvent.CalculatedPath == null || findPathEvent.CalculatedPath.Count == 0)
            {
                Debug.WriteLine("GoTo ERROR: Path not found");
                return;
            }

            for (int i = 0; i < 5; i++) //TODO hardcoded 5 step to move
            {
                if (i >= findPathEvent.CalculatedPath.Count - 1) break;
                Point goal = findPathEvent.CalculatedPath[i];
                MoveToNextEvent moveToNextEvent = new MoveToNextEvent(findPathEvent.CalculatedPath, i);
                JEventBus.GetDefault().Post(moveToNextEvent);
                geoEntity.Position = goal;
            }

            Debug.WriteLine("GoTo OK: " + geoEntity.Position);
        }
    }
}