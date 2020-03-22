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
            Debug.WriteLine("Resource updated: " + fraction.Resources[resource.Definition.Name]);
        }
    }
}