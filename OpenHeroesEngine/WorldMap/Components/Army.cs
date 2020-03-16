using System.Collections.Generic;
using Artemis;
using Artemis.Attributes;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Components
{
    [ArtemisComponentPool(InitialSize=30,IsResizable=true, ResizeSize=5, IsSupportMultiThread=false)]
    public class Army : ComponentPoolable
    {
        public List<Creature> creatures = new List<Creature>(8);
    }
}