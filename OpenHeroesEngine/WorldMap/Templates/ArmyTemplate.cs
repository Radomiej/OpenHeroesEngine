using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using OpenHeroesEngine.WorldMap.Components;

namespace OpenHeroesEngine.WorldMap.Templates
{
    [ArtemisEntityTemplate("Army")]
    public class ArmyTemplate : IEntityTemplate
        {
            public Entity BuildEntity(Entity e, EntityWorld entityWorld, params object[] args)
            {
                e.AddComponent(entityWorld.GetComponentFromPool<GeoEntity>());
                e.AddComponent(entityWorld.GetComponentFromPool<Army>());
                e.AddComponent(entityWorld.GetComponentFromPool<ArmyAi>());
                return e;
            }
    }
}