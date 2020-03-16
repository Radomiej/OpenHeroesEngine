using Artemis;
using Artemis.Attributes;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.Game.Components
{
    [ArtemisComponentPool(InitialSize = 100, IsResizable = true, ResizeSize = 5, IsSupportMultiThread = false)]
    public class MovableEntity : ComponentPoolable
    {
        public Point Position;
    }
}