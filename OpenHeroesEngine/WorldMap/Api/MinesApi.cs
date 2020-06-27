using Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events.Structures;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Api
{
    public class MinesApi
    {
        public static void AddMineToFraction(Entity structureEntity, Fraction fraction, JEventBus eventBus = null)
        {
            Structure structure = structureEntity.GetComponent<Structure>();
            if (structure == null) return;

            AddStructureToFractionEvent addStructureToFractionEvent =
                new AddStructureToFractionEvent(structure, fraction, structureEntity);
            BaseApi.SendEvent(eventBus, addStructureToFractionEvent);
        }

        public static void AddMineToFraction(Point tilePosition, Fraction fraction, JEventBus eventBus = null)
        {
            if (eventBus == null) eventBus = JEventBus.GetDefault();

            FindNearestStructure findNearestStructure = new FindNearestStructure(tilePosition, 0);
            eventBus.Post(findNearestStructure);
            var entity = findNearestStructure.Nearest;
            AddMineToFraction(entity, fraction, eventBus);
        }
    }
}