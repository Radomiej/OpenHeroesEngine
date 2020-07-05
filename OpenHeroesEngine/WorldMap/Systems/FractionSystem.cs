using System;
using System.Collections.Generic;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.WorldMap.Api;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Events.Armies;
using OpenHeroesEngine.WorldMap.Events.Fractions;
using OpenHeroesEngine.WorldMap.Events.Resources;
using OpenHeroesEngine.WorldMap.Events.Structures;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;
using static OpenHeroesEngine.Logger.Logger;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class FractionSystem : EventBasedSystem
    {
        private Grid _grid;
        private HashSet<Fraction> _fractions = new HashSet<Fraction>();

        public override void LoadContent()
        {
            base.LoadContent();
            _grid = BlackBoard.GetEntry<Grid>("Grid");
        }

        [Subscribe]
        public void FractionCanAffordOnSpentResourceListener(FindFractionCanAffordOnSpentResourceEvent fractionCanAfford)
        {
            Fraction fraction = fractionCanAfford.Fraction;
            int maxAmount = int.MaxValue;
            
            foreach (var resource in fractionCanAfford.Resources)
            {
                int fractionAmount = GetFractionAmountOfResource(fraction, resource.Definition);
                int tempAmount = fractionAmount / resource.Amount;
                maxAmount = Math.Min(tempAmount, maxAmount);
            }

            fractionCanAfford.Success = true;
            fractionCanAfford.MaxAmount = maxAmount;
        }

        private int GetFractionAmountOfResource(Fraction fraction, ResourceDefinition resourceDefinition)
        {
            return FractionApi.GetFractionAmountOfResource(fraction, resourceDefinition);
        }

        [Subscribe]
        public void RemoveResourceListener(RemoveResourceFromFractionEvent removeResourceFromFraction)
        {
            if(removeResourceFromFraction.Fraction == null) return;
            Fraction fraction = removeResourceFromFraction.Fraction;
            Resource resource = removeResourceFromFraction.Resource;

            if (GetFractionAmountOfResource(fraction, resource.Definition) >= resource.Amount) // The faction can afford to buy MAX
            {
                removeResourceFromFraction.Success = true;
            }
            else if(removeResourceFromFraction.Dividend > 0)//The factions can afford to buy parts
            {
                int parts = fraction.Resources[resource.Definition.Name].Amount % removeResourceFromFraction.Dividend;
                int realCost = parts * removeResourceFromFraction.Dividend;
                Resource realResourceCost = new Resource(removeResourceFromFraction.Resource.Definition, realCost);
                ChangeResourceAmount(fraction, realResourceCost);
                removeResourceFromFraction.CountOfDividend = parts;
            }
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

            ChangeResourceAmount(fraction, resource);
        }

        private static void ChangeResourceAmount(Fraction fraction, Resource resource)
        {
            fraction.Resources[resource.Definition.Name].Amount += resource.Amount;

            string changedValue = resource.Amount >= 0 ? "+" : "-";
            changedValue += resource.Amount;
            Debug($"Resource of {fraction.Name} changed: " + fraction.Resources[resource.Definition.Name] +
                    $"({changedValue})");
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

            Debug("Mines captured: " + newFraction?.Structures[geoIndex]);
        }

        [Subscribe]
        public void AddArmyListener(AddArmyEvent addArmyEvent)
        {
            Fraction fraction = addArmyEvent.Army.Fraction;
            if (!_fractions.Contains(fraction))
            {
                _fractions.Add(fraction);
                NewFractionEvent newFractionEvent = new NewFractionEvent(fraction, addArmyEvent.Position);
                _eventBus.Post(newFractionEvent);
            }
        }
        [Subscribe]
        public void FindFractionListener(FindFractionEvent findFractionEvent)
        {
           findFractionEvent.Results = new List<Fraction>(_fractions);
        }
        
    }
}