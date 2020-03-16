using System.Collections.Generic;
using Artemis;
using Artemis.Attributes;
using OpenHeroesEngine.Game.Models;

namespace OpenHeroesEngine.Game.Components
{
    [ArtemisComponentPool(InitialSize=30,IsResizable=true, ResizeSize=5, IsSupportMultiThread=false)]
    public class Army : ComponentPoolable
    {
        public List<Creature> creatures = new List<Creature>(8);
    }
}