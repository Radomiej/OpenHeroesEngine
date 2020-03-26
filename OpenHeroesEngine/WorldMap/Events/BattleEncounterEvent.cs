using Artemis;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class BattleEncounterEvent
    {
        public readonly Entity Attacker, Deffender;
        public readonly Point Position;

        public BattleEncounterEvent(Entity attacker, Entity deffender, Point position)
        {
            Attacker = attacker;
            Deffender = deffender;
            Position = position;
        }
    }
}