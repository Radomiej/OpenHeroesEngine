using System.Collections.Generic;
using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Templates
{
    [ArtemisEntityTemplate("Mine")]
    public class MineTemplate : IEntityTemplate
    {
        public Entity BuildEntity(Entity e, EntityWorld entityWorld, params object[] args)
        {
            Dictionary<string, object> paramsDictionary = (Dictionary<string, object>) args[2];
            
            GeoEntity geoEntity = entityWorld.GetComponentFromPool<GeoEntity>();
            geoEntity.Position = args[1] as Point;
            e.AddComponent(geoEntity);

            Structure argStructure = args[0] as Structure;
            Structure structure = entityWorld.GetComponentFromPool<Structure>();
            structure.Definition = argStructure.Definition;
            e.AddComponent(structure);
            
            Mine mine = entityWorld.GetComponentFromPool<Mine>();
            mine.ResourceDefinition = paramsDictionary["definition"] as ResourceDefinition;
            mine.Production = (int) paramsDictionary["production"];
            e.AddComponent(mine);

            return e;
        }
    }
}