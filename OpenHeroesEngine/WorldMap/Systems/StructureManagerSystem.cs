using System.Diagnostics;
using System.Diagnostics.Tracing;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
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
                Debug.WriteLine("Area is blocked! " + isFreeAreaEvent);
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
            
            if (addStructureOnWorldMapEvent.Structure.Definition.Name.EndsWith("Mine")) return CreateMine(structure, position);
            return entityWorld.CreateEntityFromTemplate("Structure",
                addStructureOnWorldMapEvent.Structure,
                addStructureOnWorldMapEvent.Position);
        }

        private Entity CreateMine(Structure structure, Point position)
        {
            return entityWorld.CreateEntityFromTemplate("Mine",
                structure,
                position, new ResourceDefinition("Gold"), 500);
        }

    }
}