using System.Collections.Generic;
using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Templates
{
    [ArtemisEntityTemplate("Habitat")]
    public class HabitatTemplate : IEntityTemplate
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
            
            Habitat habitat = entityWorld.GetComponentFromPool<Habitat>();
            habitat.CreatureDefinition = paramsDictionary["definition"] as CreatureDefinition;
            habitat.Production = (int) paramsDictionary["production"];
            e.AddComponent(habitat);

            return e;
        }
    }
}