using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using OpenHeroesEngine.Game.Components;

namespace OpenHeroesEngine.Game.Templates
{
    [ArtemisEntityTemplate("Army")]
    public class ArmyTemplate : IEntityTemplate
        {
            public Entity BuildEntity(Entity e, EntityWorld entityWorld, params object[] args)
            {
                e.AddComponent(new MovableEntity());
                e.AddComponent(new Army());
                return e;
            }
    }
}