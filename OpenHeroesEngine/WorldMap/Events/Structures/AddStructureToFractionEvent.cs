using Artemis;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events.Structures
{
    public class AddStructureToFractionEvent : IHardEvent
    {
        public readonly Fraction Fraction;
        public readonly Structure Structure;
        public readonly Entity Entity;

        public AddStructureToFractionEvent(Structure structure, Fraction fraction, Entity entity)
        {
            Structure = structure;
            Fraction = fraction;
            Entity = entity;
        }
    }
}