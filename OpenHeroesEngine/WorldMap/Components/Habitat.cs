using Artemis;
using Artemis.Attributes;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Components
{
    [ArtemisComponentPool(InitialSize = 100, IsResizable = true, ResizeSize = 5, IsSupportMultiThread = false)]
    public class Habitat : ComponentPoolable
    {
        public CreatureDefinition CreatureDefinition;
        public int Production;
        public int Current;

        public Habitat()
        {
        }

        public Habitat(CreatureDefinition creatureDefinition, int production)
        {
            CreatureDefinition = creatureDefinition;
            Production = production;
        }

        public override void Initialize()
        {
            CreatureDefinition = null;
            Production = 0;
        }
    }
}