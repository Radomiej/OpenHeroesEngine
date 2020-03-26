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
        private List<Entity> _armies = new List<Entity>(100);

        public ArmyAnalyzerSystem() : base(Aspect.All(typeof(Army), typeof(GeoEntity)))
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _grid = BlackBoard.GetEntry<Grid>("Grid");
        }

        public override void OnAdded(Entity entity)
        {
            _armies.Add(entity);
        }

        public override void OnRemoved(Entity entity)
        {
            Debug.WriteLine("ArmyAnalyze REMOVE Army. Exist: " + _armies.Count);
            _armies.Remove(entity);
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
        public void MoveInListener(MoveInEvent moveInEvent)
        {
            FindArmyInArea findArmyInArea = new FindArmyInArea(moveInEvent.Current, 1);
            FindArmyInAreaListener(findArmyInArea);
            
            if(findArmyInArea.Results.Count < 2) return;

            Army attacker = findArmyInArea.Results[0].GetComponent<Army>();
            Army defender = findArmyInArea.Results[1].GetComponent<Army>();
            
            BattleEncounterEvent battleEncounterEvent = new BattleEncounterEvent(findArmyInArea.Results[0], findArmyInArea.Results[1], moveInEvent.Current);
            _eventBus.Post(battleEncounterEvent);
            Debug.WriteLine($"BATTLE ENCOUNTER {attacker} VS {defender}");
        }
    }
}