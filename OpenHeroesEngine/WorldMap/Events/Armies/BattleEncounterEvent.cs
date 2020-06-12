using Artemis;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events.Armies
{
    public class BattleEncounterEvent : ISoftEvent
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