using Artemis;
using Artemis.Attributes;

namespace OpenHeroesEngine.GameSystems.Components
{
    [ArtemisComponentPool(InitialSize = 10, IsResizable = true, ResizeSize = 5, IsSupportMultiThread = false)]
    public class Urban : ComponentPoolable
    {
        public int Population;
        public int Conscripts;
        public float BirdsRate;
        
        public override void Initialize()
        {
            Population = 0;
            BirdsRate = 0;
        }

        public int TakeConscripts(int conscripts)
        {
            if (conscripts > Conscripts) conscripts = Conscripts;
            Conscripts -= conscripts;
            Population -= conscripts;
            return conscripts;
        }
    }
}