using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Templates
{
    [ArtemisEntityTemplate("Resource")]
    public class ResourceTemplate : IEntityTemplate
        {
            public Entity BuildEntity(Entity e, EntityWorld entityWorld, params object[] args)
            {
                GeoEntity geoEntity = new GeoEntity();
                geoEntity.Position = args[1] as Point;
                e.AddComponent(geoEntity);
                e.AddComponent(args[0] as Resource);
                return e;
            }
    }
}