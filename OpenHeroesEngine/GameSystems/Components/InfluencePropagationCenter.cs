using Artemis;
using Artemis.Attributes;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.GameSystems.Components
{
    [ArtemisComponentPool(InitialSize = 10, IsResizable = true, ResizeSize = 5, IsSupportMultiThread = false)]
    public class InfluencePropagationCenter : ComponentPoolable
    {
        public Point Position;
        public float InfluenceBase;

        public override void Initialize()
        {
            Position = null;
            InfluenceBase = 0;
        }
    }
}