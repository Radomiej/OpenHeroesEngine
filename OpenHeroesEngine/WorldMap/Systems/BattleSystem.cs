using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.Battle;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class BattleSystem : EventBasedSystem
    {
        [Subscribe]
        public void BattleEncounterListener(BattleEncounterEvent battleEncounterEvent)
        {
            var battleRunner = new SimpleBattleRunner(battleEncounterEvent.Attacker.GetComponent<Army>(),
                battleEncounterEvent.Defender.GetComponent<Army>());

            battleRunner.Run();

            if (battleRunner.BattleWinner == SimpleBattleRunner.Winner.Attacker)
            {
                ArmyLoseEvent armyLoseEvent =
                    new ArmyLoseEvent(battleEncounterEvent.Defender, battleEncounterEvent.Position);
                _eventBus.Post(armyLoseEvent);
            }
            else if (battleRunner.BattleWinner == SimpleBattleRunner.Winner.Defender)
            {
                ArmyLoseEvent armyLoseEvent =
                    new ArmyLoseEvent(battleEncounterEvent.Attacker, battleEncounterEvent.Position);
                _eventBus.Post(armyLoseEvent);
            }
            else
            {
                ArmyLoseEvent armyLoseEvent =
                    new ArmyLoseEvent(battleEncounterEvent.Attacker, battleEncounterEvent.Position);
                _eventBus.Post(armyLoseEvent);

                armyLoseEvent =
                    new ArmyLoseEvent(battleEncounterEvent.Defender, battleEncounterEvent.Position);
                _eventBus.Post(armyLoseEvent);
            }
        }
    }
}