using System.Diagnostics;
using System.Diagnostics.Tracing;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Events.Moves;
using OpenHeroesEngine.WorldMap.Events.Obstacles;
using OpenHeroesEngine.WorldMap.Events.Structures;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class StructureManagerSystem : EventBasedSystem
    {
        [Subscribe]
        public void AddStructureListener(AddStructureOnWorldMapEvent addStructureOnWorldMapEvent)
        {
            IsFreeAreaEvent isFreeAreaEvent = new IsFreeAreaEvent(addStructureOnWorldMapEvent.Position, addStructureOnWorldMapEvent.Structure.Definition.Size);
            _eventBus.Post(isFreeAreaEvent);

            if (!isFreeAreaEvent.IsFree)
            {
                // Debug.WriteLine("Area is blocked! " + isFreeAreaEvent);
                return;
            }

            Entity obstacle = CreateEntityBasedOnStructureType(addStructureOnWorldMapEvent);
          
            
            PlaceObjectOnMapEvent placeObjectOnMapEvent = new PlaceObjectOnMapEvent(obstacle, addStructureOnWorldMapEvent.Position, addStructureOnWorldMapEvent.Structure.Definition.Size);
            _eventBus.Post(placeObjectOnMapEvent);
            
        }

        private Entity CreateEntityBasedOnStructureType(AddStructureOnWorldMapEvent addStructureOnWorldMapEvent)
        {
            Structure structure = addStructureOnWorldMapEvent.Structure;
            Point position = addStructureOnWorldMapEvent.Position;

            string templateName = "Structure";
            if (addStructureOnWorldMapEvent.Params.ContainsKey("template"))
            {
                if (addStructureOnWorldMapEvent.Params["template"] is string readValue) templateName = readValue;
            }

            // if (addStructureOnWorldMapEvent.Structure.Definition.Name.EndsWith("Mine")) return CreateMine(structure, position);
            // if (addStructureOnWorldMapEvent.Structure.Definition.Name.EndsWith("Habitat")) return CreateHabitat(structure, position);

            return entityWorld.CreateEntityFromTemplate(templateName,
                structure, position, addStructureOnWorldMapEvent.Params);
        }

        private Entity CreateHabitat(Structure structure, Point position)
        {
             return entityWorld.CreateEntityFromTemplate("Habitat",
                           structure,
                           position, new CreatureDefinition("Peasant"), 7);
        }

        private Entity CreateMine(Structure structure, Point position)
        {
            return entityWorld.CreateEntityFromTemplate("Mine",
                structure,
                position, new ResourceDefinition("Gold"), 500);
        }

    }
}