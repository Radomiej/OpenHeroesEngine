using Artemis;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class BattleEncounterEvent
    {
        public readonly Entity Attacker, Defender;
        public readonly Point Position;

        public BattleEncounterEvent(Entity attacker, Entity defender, Point position)
        {
            Attacker = attacker;
            Defender = defender;
            Position = position;
        }
    }
}