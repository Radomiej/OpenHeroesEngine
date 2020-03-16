using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.Game.Components;
using OpenHeroesEngine.Game.Models;

namespace OpenHeroesEngine.Game.Templates
{
    [ArtemisEntityTemplate("Resource")]
    public class ResourceTemplate : IEntityTemplate
        {
            public Entity BuildEntity(Entity e, EntityWorld entityWorld, params object[] args)
            {
                MovableEntity movableEntity = new MovableEntity();
                movableEntity.Position = args[1] as Point;
                e.AddComponent(movableEntity);
                e.AddComponent(args[0] as Resource);
                return e;
            }
    }
}