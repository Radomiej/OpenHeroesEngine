using Artemis;
using Artemis.Attributes;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Components
{
    [ArtemisComponentPool(InitialSize=100,IsResizable=true, ResizeSize=5, IsSupportMultiThread=false)]
    public class Resource : ComponentPoolable
    {
        public readonly ResourceDefinition Definition;
        public readonly int Amount;

        public Resource(ResourceDefinition definition, int amount = 1)
        {
            Definition = definition;
            Amount = amount;
        }
    }
}