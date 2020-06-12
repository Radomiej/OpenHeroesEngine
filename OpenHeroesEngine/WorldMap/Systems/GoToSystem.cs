using System.Diagnostics;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using Radomiej.JavityBus;
using static OpenHeroesEngine.Logger.Logger;

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
                Error("GoTo ERROR: Missing GeoEntity");
                return;
            }

            GeoEntity geoEntity = entity.GetComponent<GeoEntity>();
            Army army = entity.GetComponent<Army>();

            FindPathEvent findPathEvent = new FindPathEvent(geoEntity.Position, goToEvent.Goal);
            JEventBus.GetDefault().Post(findPathEvent);
            if (findPathEvent.CalculatedPath == null || findPathEvent.CalculatedPath.Count == 0)
            {
                Warning("GoTo ERROR: Path not found");
                return;
            }

            //Go to the same place (SPACE)
            if (findPathEvent.CalculatedPath.Count == 1)
            {
                findPathEvent.CalculatedPath.Add(findPathEvent.CalculatedPath[0]);
            }

            int i = 0;
            while (army.MovementPoints > 0)
            {
                if (i >= findPathEvent.CalculatedPath.Count - 1) break;
                MoveToNextEvent moveToNextEvent = new MoveToNextEvent(findPathEvent.CalculatedPath, entity, i);
                JEventBus.GetDefault().Post(moveToNextEvent);
                i++;
            }

            Debug("GoTo OK: " + geoEntity.Position);
            if (geoEntity.Position.Equals(goToEvent.Goal)) goToEvent.Complete = true;
        }
    }
}