using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.Utils;
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
        private Dictionary<Point, Entity> _entrances = new Dictionary<Point, Entity>();

        [Subscribe]
        public void AddStructureListener(AddStructureOnWorldMapEvent addStructureOnWorldMapEvent)
        {
            IsFreeAreaEvent isFreeAreaEvent = new IsFreeAreaEvent(addStructureOnWorldMapEvent.Position,
                addStructureOnWorldMapEvent.Structure.Definition.Size);
            _eventBus.Post(isFreeAreaEvent);

            if (!isFreeAreaEvent.IsFree)
            {
                // Debug.WriteLine("Area is blocked! " + isFreeAreaEvent);
                return;
            }

            Entity structure = CreateEntityBasedOnStructureType(addStructureOnWorldMapEvent);
            AddEntrance(structure, addStructureOnWorldMapEvent.Position +
                                   addStructureOnWorldMapEvent.Structure.Definition.EntranceOffset);
            PlaceObjectOnMapEvent placeObjectOnMapEvent = new PlaceObjectOnMapEvent(structure,
                addStructureOnWorldMapEvent.Position, addStructureOnWorldMapEvent.Structure.Definition.Size);
            _eventBus.Post(placeObjectOnMapEvent);
        }

        private void AddEntrance(Entity structure, Point entrancePosition)
        {
            _entrances.Add(entrancePosition, structure);
        }

        [Subscribe]
        public void FindEntranceListener(FindEntranceEvent findEntranceEvent)
        {
            findEntranceEvent.Success = true;
            findEntranceEvent.Result = _entrances.GetValue(findEntranceEvent.Location, null);
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

            return entityWorld.CreateEntityFromTemplate(templateName,
                structure, position, addStructureOnWorldMapEvent.Params);
        }
    }
}