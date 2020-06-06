using Artemis;
using Artemis.Attributes;

namespace OpenHeroesEngine.GameSystems.Components
{
    [ArtemisComponentPool(InitialSize = 10, IsResizable = true, ResizeSize = 5, IsSupportMultiThread = false)]
    public class Urban : ComponentPoolable
    {
        public int Population;
        public float BirdsRate;
        
        public override void Initialize()
        {
            Population = 0;
            BirdsRate = 0;
        }
    }
}