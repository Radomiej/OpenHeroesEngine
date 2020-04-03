using System.Collections.Generic;
using Artemis;
using Artemis.Attributes;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Components
{
    [ArtemisComponentPool(InitialSize = 30, IsResizable = true, ResizeSize = 5, IsSupportMultiThread = false)]
    public class Army : ComponentPoolable
    {
        public List<Creature> Creatures = new List<Creature>(8);
        public Fraction Fraction { get; set; }
        public float MovementPoints { get; set; }

        public override void Initialize()
        {
            Fraction = null;
            Creatures.Clear();
        }

        public override string ToString()
        {
            return $"Army of {Fraction.Name}";
        }
    }
}