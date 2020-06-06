using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.AI.Decisions;
using OpenHeroesEngine.WorldMap.AI.State;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Templates
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