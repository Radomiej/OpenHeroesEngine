using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class ArmyAnalyzerSystem : EventBasedSystem
    {
        private Grid _grid;
        private HashSet<AddArmyEvent> _armyInCreation = new HashSet<AddArmyEvent>();
        private List<Entity> _armies = new List<Entity>(100);
        private Dictionary<Fraction, List<Entity>> _fractionToArmy = new Dictionary<Fraction, List<Entity>>();

        public ArmyAnalyzerSystem() : base(Aspect.All(typeof(Army), typeof(GeoEntity)))
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _grid = BlackBoard.GetEntry<Grid>("Grid");
        }

        [Subscribe]
        public void AddArmyListener(AddArmyEvent addArmyEvent)
        {
            _armyInCreation.Add(addArmyEvent);
            Army army = addArmyEvent.Army;
            Fraction fraction = army.Fraction;

            if (!_fractionToArmy.ContainsKey(fraction)) _fractionToArmy.Add(fraction, new List<Entity>());
        }

        public override void Process()
        {
            _armyInCreation.Clear();
        }

        public override void OnAdded(Entity entity)
        {
            _armies.Add(entity);
            Army army = entity.GetComponent<Army>();
            Fraction fraction = army.Fraction;

            if (!_fractionToArmy.ContainsKey(fraction)) _fractionToArmy.Add(fraction, new List<Entity>());
            _fractionToArmy[fraction].Add(entity);
        }

        public override void OnRemoved(Entity entity)
        {
            Debug.WriteLine("ArmyAnalyze REMOVE Army. Exist: " + _armies.Count);
            if (_armies.Contains(entity))
            {
                _armies.Remove(entity);

                Army army = entity.GetComponent<Army>();
                Fraction fraction = army.Fraction;
                _fractionToArmy[fraction].Remove(entity);
            }
        }

        [Subscribe]
        public void ArmyLoseListener(ArmyLoseEvent armyLoseEvent)
        {
            _armies.Remove(armyLoseEvent.Army);
        }

        [Subscribe]
        public void FindArmyInAreaListener(FindArmyInArea findArmyInArea)
        {
            Point middle = findArmyInArea.Location;
            foreach (var entity in _armies)
            {
                GeoEntity geoEntity = entity.GetComponent<GeoEntity>();
                float distance = Point.Distance(middle, geoEntity.Position);
                if (distance <= findArmyInArea.MaxDistance) findArmyInArea.Results.Add(entity);
            }
        }
        [Subscribe]
        public void FindArmiesInFractionListener(FindArmiesInFraction findArmiesInFraction)
        {
            findArmiesInFraction.Success = true;
            if (!_fractionToArmy.ContainsKey(findArmiesInFraction.Fraction)) return;

            findArmiesInFraction.Results.AddRange(_fractionToArmy[findArmiesInFraction.Fraction]);
        }

        

        [Subscribe]
        public void MoveInListener(MoveInEvent moveInEvent)
        {
            FindArmyInArea findArmyInArea = new FindArmyInArea(moveInEvent.Current, 1);
            FindArmyInAreaListener(findArmyInArea);

            if (findArmyInArea.Results.Count < 2) return;

            Army attacker = findArmyInArea.Results[0].GetComponent<Army>();
            Army defender = findArmyInArea.Results[1].GetComponent<Army>();

            Debug.WriteLine($"BATTLE ENCOUNTER {attacker} VS {defender}");
            BattleEncounterEvent battleEncounterEvent = new BattleEncounterEvent(findArmyInArea.Results[0],
                findArmyInArea.Results[1], moveInEvent.Current);
            _eventBus.Post(battleEncounterEvent);
        }
    }
}