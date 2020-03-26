using System;
using System.Diagnostics;
using Artemis;
using OpenHeroesEngine.WorldMap.Components;

namespace OpenHeroesEngine.Battle
{
    public class SimpleBattleRunner
    {
        public enum Winner
        {
            Attacker,
            Defender,
            None
        }

        public Winner BattleWinner { get; private set; }
        public readonly Army Attacker, Defender;

        public SimpleBattleRunner(Army attacker, Army defender)
        {
            Attacker = attacker;
            Defender = defender;
        }

        public void Run()
        {
            double attackerPower = GetArmyStrength(Attacker);
            attackerPower *= 1.1;

            double defenderPower = GetArmyStrength(Defender);

            double delta = attackerPower - defenderPower;
            double aliveInPercent = 0;
            Army winnerArmy = null;
            Army loserArmy = null;
            if (delta > 0)
            {
                BattleWinner = Winner.Attacker;
                winnerArmy = Attacker;
                loserArmy = Defender;
                aliveInPercent = Math.Abs(delta * 100 / attackerPower);
            }
            else if (delta < 0)
            {
                BattleWinner = Winner.Defender;
                winnerArmy = Defender;
                loserArmy = Defender;
                aliveInPercent = Math.Abs(delta * 100 / defenderPower);
            }
            else
            {
                BattleWinner = Winner.None;
                MultiplyCreaturesByPercent(Attacker, 0);
                MultiplyCreaturesByPercent(Defender, 0);
                return;
            }

            MultiplyCreaturesByPercent(winnerArmy, aliveInPercent, true);
            MultiplyCreaturesByPercent(loserArmy, 0);
        }

        private void MultiplyCreaturesByPercent(Army army, in double aliveInPercent,  bool freeUnit = false)
        {
           
            double decimalCasualties = aliveInPercent / 100d; 
            foreach (var creature in army.Creatures)
            {
                creature.Count = (int) (creature.Count * decimalCasualties);
                Debug.WriteLine($"Units after fight {creature.Definition.Name}: {creature.Count}" );
                if (freeUnit && creature.Count == 0)
                {
                    creature.Count = 1;
                    freeUnit = false;
                }
            }
        }

        private float GetArmyStrength(Army army)
        {
            float armyStrength = 0;
            foreach (var creature in army.Creatures)
            {
                armyStrength += creature.Count * creature.Definition.Hp;
                armyStrength += creature.Count * creature.Definition.Damage;
            }

            return armyStrength;
        }
    }
}