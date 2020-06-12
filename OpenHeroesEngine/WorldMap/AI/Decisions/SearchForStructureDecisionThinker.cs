using System.Diagnostics;
using Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.AI.State;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Events.Moves;
using OpenHeroesEngine.WorldMap.Events.Structures;
using OpenHeroesEngine.WorldMap.Events.Terrain;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.AI.Decisions
{
    public class SearchForStructureDecisionThinker : IDecisionThinker
    {
        private JEventBus _eventBus;

        public void Think(Entity thinker, JEventBus eventBus)
        {
            _eventBus = eventBus;
            GeoEntity geoEntity = thinker.GetComponent<GeoEntity>();
            ArmyAi armyAi = thinker.GetComponent<ArmyAi>();
            Army army = thinker.GetComponent<Army>();

            FindStructureInArea findStructureInArea = new FindStructureInArea(geoEntity.Position, armyAi.SearchRadius);
            JEventBus.GetDefault().Post(findStructureInArea);

            Entity nearestStructure = null;
            foreach (var structureEntity in findStructureInArea.Results)
            {
                Structure structure = structureEntity.GetComponent<Structure>();
                GeoEntity structureGeo = structureEntity.GetComponent<GeoEntity>();
                long geoIndex = GetGeoIndex(structureGeo.Position);
                if (StructureIsVisited(geoIndex, armyAi) || structure.Fraction == army.Fraction)
                    continue;
                //Check accessibility
                FindPathEvent findPathEvent = new FindPathEvent(geoEntity.Position, structureGeo.Position);
                _eventBus.Post(findPathEvent);
                if(findPathEvent.CalculatedPath == null) continue;
                
                nearestStructure = structureEntity;
                break;
            }

            if (nearestStructure == null)
            {
                Debug.WriteLine(army + " Skip to FindResources");
                armyAi.ArmyStateMachine.Fire(ArmyTrigger.FindResources);
                return;
            }

            armyAi.SearchRadius = 10;
            
            GeoEntity resourcePosition = nearestStructure.GetComponent<GeoEntity>();

            GoToEvent goToEvent = new GoToEvent(thinker, resourcePosition.Position);
            Debug.WriteLine(army + " Go For Structure: " + goToEvent.Goal);
            JEventBus.GetDefault().Post(goToEvent);

            if (goToEvent.Complete) armyAi.VisitedStructures.Add(GetGeoIndex(resourcePosition.Position));

            armyAi.ArmyStateMachine.Fire(ArmyTrigger.FinishAction);
        }

        private long GetGeoIndex(Point structureGeoPosition)
        {
           GeoIndexReceiverEvent geoIndexReceiverEvent = new GeoIndexReceiverEvent(structureGeoPosition);
           _eventBus.Post(geoIndexReceiverEvent);
           return geoIndexReceiverEvent.GeoIndex;
        }

        private bool StructureIsVisited(long geoIndex, ArmyAi armyAi)
        {
            return armyAi.VisitedStructures.Contains(geoIndex);
        }
    }
}