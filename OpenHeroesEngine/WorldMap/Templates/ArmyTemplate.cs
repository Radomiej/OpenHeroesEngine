using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.AI.Decisions;
using OpenHeroesEngine.WorldMap.AI.State;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Templates
{
    [ArtemisEntityTemplate("Army")]
    public class ArmyTemplate : IEntityTemplate
        {
            public Entity BuildEntity(Entity e, EntityWorld entityWorld, params object[] args)
            {
                Point position = args[1] as Point;
                GeoEntity geoEntity = entityWorld.GetComponentFromPool<GeoEntity>();
                geoEntity.Position = position;
                
                Army argArmy = args[0] as Army;
                
                e.AddComponent(geoEntity);
                e.AddComponent(PrepareArmy(entityWorld, argArmy));
                e.AddComponent(PrepareArmyAi(entityWorld));
                return e;
            }

            private Army PrepareArmy(EntityWorld entityWorld, Army argArmy)
            {
                Army army = entityWorld.GetComponentFromPool<Army>();
                army.Creatures = argArmy.Creatures;
                army.Fraction = argArmy.Fraction;
                return army;
            }

            private ArmyAi PrepareArmyAi(EntityWorld entityWorld)
            {
                ArmyAi armyAi = entityWorld.GetComponentFromPool<ArmyAi>();
                armyAi.DefaultDecisionThinker = new DefaultDecisionThinker();
                armyAi.DecisionThinkers.Add(ArmyState.Idle, new IdleDecisionThinker());
                armyAi.DecisionThinkers.Add(ArmyState.SearchForResource, new SearchForResourceDecisionThinker());
                armyAi.DecisionThinkers.Add(ArmyState.SearchForStructure, new SearchForStructureDecisionThinker());
                armyAi.DecisionThinkers.Add(ArmyState.SearchForEnemy, new ArmyEncounterDecisionThinker());
                
                return armyAi;
            }
        }
}