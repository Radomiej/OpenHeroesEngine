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
            GeoEntity geoEntity = entityWorld.GetComponentFromPool<GeoEntity>();
            geoEntity.Position = args[1] as Point;
            e.AddComponent(geoEntity);

            Resource argResource = args[0] as Resource;
            Resource resource = entityWorld.GetComponentFromPool<Resource>();
            resource.Amount = argResource.Amount;
            resource.Definition = argResource.Definition;
            
            e.AddComponent(args[0] as Resource);
            return e;
        }
    }
}