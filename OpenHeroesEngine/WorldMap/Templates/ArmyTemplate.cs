using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
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
                e.AddComponent(entityWorld.GetComponentFromPool<GeoEntity>());
                e.AddComponent(PrepareArmy(entityWorld));
                e.AddComponent(PrepareArmyAi(entityWorld));
                return e;
            }

            private Army PrepareArmy(EntityWorld entityWorld)
            {
                Army army = entityWorld.GetComponentFromPool<Army>();
                army.Fraction = new Fraction("Red");
                return army;
            }

            private ArmyAi PrepareArmyAi(EntityWorld entityWorld)
            {
                ArmyAi armyAi = entityWorld.GetComponentFromPool<ArmyAi>();
                armyAi.DefaultDecisionThinker = new DefaultDecisionThinker();
                armyAi.DecisionThinkers.Add(ArmyState.Idle, new IdleDecisionThinker());
                armyAi.DecisionThinkers.Add(ArmyState.SearchForResource, new SearchForResourceDecisionThinker());
                
                return armyAi;
            }
        }
}