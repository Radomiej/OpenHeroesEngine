using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Templates
{
    [ArtemisEntityTemplate("Structure")]
    public class StructureTemplate : IEntityTemplate
    {
        public Entity BuildEntity(Entity e, EntityWorld entityWorld, params object[] args)
        {
            GeoEntity geoEntity = entityWorld.GetComponentFromPool<GeoEntity>();
            geoEntity.Position = args[1] as Point;
            e.AddComponent(geoEntity);

            Structure argStructure = args[0] as Structure;
            Structure structure = entityWorld.GetComponentFromPool<Structure>();
            structure.Definition = argStructure.Definition;
            
            e.AddComponent(structure);
            
            return e;
        }
    }
}