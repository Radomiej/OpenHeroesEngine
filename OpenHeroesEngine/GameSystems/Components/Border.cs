using Artemis;
using Artemis.Attributes;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.GameSystems.Components
{
    [ArtemisComponentPool(InitialSize = 90, IsResizable = true, ResizeSize = 5, IsSupportMultiThread = false)]
    public class Border : ComponentPoolable
    {
        public Point TitleA, TitleB;

        public override void Initialize()
        {
            TitleA = null;
            TitleB = null;
        }

        public override string ToString()
        {
            return $"{nameof(TitleA)}: {TitleA}, {nameof(TitleB)}: {TitleB}";
        }
    }
}