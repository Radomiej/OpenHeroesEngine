namespace OpenHeroesEngine.WorldMap.Models
{
    public class CreatureDefinition
    {
        public readonly string Name;

        public readonly int Hp;
        public readonly int Damage;

        public CreatureDefinition(string name, int damage = 3, int hp = 10)
        {
            Name = name;
            Damage = damage;
            Hp = hp;
        }
    }
}