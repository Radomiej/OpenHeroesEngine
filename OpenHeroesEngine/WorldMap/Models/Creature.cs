namespace OpenHeroesEngine.Game.Models
{
    public class Creature
    {
        public readonly CreatureDefinition Definition;
        public int Count;

        public Creature(CreatureDefinition definition, int count = 1)
        {
            Definition = definition;
            Count = count;
        }
    }
}