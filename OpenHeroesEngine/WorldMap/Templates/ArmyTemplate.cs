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
                e.AddComponent(new GeoEntity());
                e.AddComponent(new Army());
                e.AddComponent(new ArmyAi());
                return e;
            }
    }
}