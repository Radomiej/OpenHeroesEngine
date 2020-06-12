using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events.Resources
{
    public class AddResourceToFractionEvent : IHardEvent
    {
        public readonly Fraction Fraction;
        public readonly Resource Resource;

        public AddResourceToFractionEvent(Resource resource, Fraction fraction)
        {
            Resource = resource;
            Fraction = fraction;
        }
    }
}