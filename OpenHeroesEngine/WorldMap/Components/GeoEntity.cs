using Artemis;
using Artemis.Attributes;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Components
{
    [ArtemisComponentPool(InitialSize = 100, IsResizable = true, ResizeSize = 5, IsSupportMultiThread = false)]
    public class GeoEntity : ComponentPoolable
    {
        public Point Position;
    }
}