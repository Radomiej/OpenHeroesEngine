using Artemis;
using Artemis.Attributes;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Components
{
    [ArtemisComponentPool(InitialSize = 100, IsResizable = true, ResizeSize = 5, IsSupportMultiThread = false)]
    public class Mine : ComponentPoolable
    {
        public ResourceDefinition ResourceDefinition;
        public int Production;

        public Mine()
        {
        }

        public Mine(ResourceDefinition resourceDefinition, int production)
        {
            ResourceDefinition = resourceDefinition;
            Production = production;
        }

        public override void Initialize()
        {
            ResourceDefinition = null;
            Production = 0;
        }
    }
}