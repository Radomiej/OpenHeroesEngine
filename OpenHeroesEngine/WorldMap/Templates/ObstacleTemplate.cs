using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Templates
{
    [ArtemisEntityTemplate("Obstacle")]
    public class ObstacleTemplate : IEntityTemplate
    {
        public Entity BuildEntity(Entity e, EntityWorld entityWorld, params object[] args)
        {
            GeoEntity geoEntity = entityWorld.GetComponentFromPool<GeoEntity>();
            geoEntity.Position = args[1] as Point;
            e.AddComponent(geoEntity);

            Obstacle argObstacle = args[0] as Obstacle;
            Obstacle obstacle = entityWorld.GetComponentFromPool<Obstacle>();
            obstacle.Definition = argObstacle.Definition;
            
            e.AddComponent(obstacle);
            return e;
        }
    }
}