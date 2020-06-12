using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.GameSystems.Components;
using OpenHeroesEngine.WorldMap.Components;

namespace OpenHeroesEngine.GameSystems.Templates
{
    [ArtemisEntityTemplate("Influence")]
    public class InfluenceTemplate : IEntityTemplate
        {
            public Entity BuildEntity(Entity e, EntityWorld entityWorld, params object[] args)
            {
                Point position = args[0] as Point;
                float influenceBase = (float) args[1];
                GeoEntity geoEntity = entityWorld.GetComponentFromPool<GeoEntity>();
                geoEntity.Position = position;
                
                InfluencePropagationCenter influencePropagationCenter = entityWorld.GetComponentFromPool<InfluencePropagationCenter>();
                influencePropagationCenter.Position = position;
                influencePropagationCenter.InfluenceBase = influenceBase;
                
                e.AddComponent(geoEntity);
                e.AddComponent(influencePropagationCenter);
                return e;
            }

        }
}