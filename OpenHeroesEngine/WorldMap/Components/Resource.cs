using Artemis;
using Artemis.Attributes;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Components
{
    [ArtemisComponentPool(InitialSize = 100, IsResizable = true, ResizeSize = 5, IsSupportMultiThread = false)]
    public class Resource : ComponentPoolable
    {
        public ResourceDefinition Definition;
        public int Amount;

        public Resource()
        {
            
        }
        
        public Resource(ResourceDefinition definition, int amount = 1)
        {
            Definition = definition;
            Amount = amount;
        }

        public override void Initialize()
        {
            Definition = null;
            Amount = 0;
        }
    }
}