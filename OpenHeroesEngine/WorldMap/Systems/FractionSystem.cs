using System.Diagnostics;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class FractionSystem : EventBasedSystem
    {
        private Grid _grid;

        public override void LoadContent()
        {
            base.LoadContent();
            _grid = BlackBoard.GetEntry<Grid>("Grid");
        }

        [Subscribe]
        public void AddResourceListener(AddResourceToFractionEvent addResourceToFractionEvent)
        {
            if(addResourceToFractionEvent.Fraction == null) return;
            Fraction fraction = addResourceToFractionEvent.Fraction;
            Resource resource = addResourceToFractionEvent.Resource;

            if (!fraction.Resources.ContainsKey(resource.Definition.Name))
            {
                Resource fractionResource = new Resource(resource.Definition, 0);
                fraction.Resources.Add(resource.Definition.Name, fractionResource);
            }

            fraction.Resources[resource.Definition.Name].Amount += resource.Amount;

            string changedValue = resource.Amount >= 0 ? "+" : "-";
            changedValue += resource.Amount;
            Debug.WriteLine($"Resource of {fraction.Name} changed: " + fraction.Resources[resource.Definition.Name] + $"({changedValue})");
        }
        
        [Subscribe]
        public void AddStructureListener(AddStructureToFractionEvent addStructureToFractionEvent)
        {
            Fraction newFraction = addStructureToFractionEvent.Fraction;
            Structure structure = addStructureToFractionEvent.Structure;
            Fraction oldFraction = structure.Fraction;
            GeoEntity geoEntity = addStructureToFractionEvent.Entity.GetComponent<GeoEntity>();
            long geoIndex = _grid.GetNodeIndex(geoEntity.Position);
            
            if(newFraction == oldFraction) return;


            oldFraction?.Structures.Remove(geoIndex);
            newFraction?.Structures.Add(geoIndex, structure);
            structure.Fraction = newFraction;

            Debug.WriteLine("Mines captured: " + newFraction?.Structures[geoIndex]);
        }
    }
}