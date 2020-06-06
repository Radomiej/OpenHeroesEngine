using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.GameSystems.Components;
using OpenHeroesEngine.WorldMap.Components;

namespace OpenHeroesEngine.GameSystems.Templates
{
    [ArtemisEntityTemplate("Urban")]
    public class UrbanTemplate : IEntityTemplate
        {
            public Entity BuildEntity(Entity e, EntityWorld entityWorld, params object[] args)
            {
                Point position = args[0] as Point;
                int population = (int) args[1];
                float birdsRate = (float) args[2];
                GeoEntity geoEntity = entityWorld.GetComponentFromPool<GeoEntity>();
                geoEntity.Position = position;
                
                Urban urban = entityWorld.GetComponentFromPool<Urban>();
                urban.Population = population;
                urban.BirdsRate = birdsRate;
                
                e.AddComponent(geoEntity);
                e.AddComponent(urban);
                return e;
            }

        }
}