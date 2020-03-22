using Artemis;
using Artemis.Attributes;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Components
{
    [ArtemisComponentPool(InitialSize = 100, IsResizable = true, ResizeSize = 5, IsSupportMultiThread = false)]
    public class Obstacle : ComponentPoolable
    {
        public ObstacleDefinition Definition;

        public Obstacle()
        {
        }

        public Obstacle(ObstacleDefinition definition)
        {
            Definition = definition;
        }

        public override void Initialize()
        {
            Definition = null;
        }
    }
}