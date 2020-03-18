using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class AddResourceToFractionEvent
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