using Artemis;
using Artemis.Attributes;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Components
{
    [ArtemisComponentPool(InitialSize = 100, IsResizable = true, ResizeSize = 5, IsSupportMultiThread = false)]
    public class Structure : ComponentPoolable
    {
        public StructureDefinition Definition;
        public Fraction Fraction;


        public Structure()
        {
        }

        public Structure(StructureDefinition definition)
        {
            Definition = definition;
        }

        public override void Initialize()
        {
            Definition = null;
        }

        public override string ToString()
        {
            return $"Structure {Definition.Name}, {nameof(Fraction)}: {Fraction.Name}";
        }
    }
}