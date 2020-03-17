using System.Collections.Generic;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class ResourceAnalyzerSystem : EventBasedSystem
    {
        private Grid _grid;
        private List<Entity> _resources = new List<Entity>(100);

        public ResourceAnalyzerSystem() : base(Aspect.All(typeof(Resource), typeof(GeoEntity)))
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _grid = BlackBoard.GetEntry<Grid>("Grid");
        }

        public override void OnAdded(Entity entity)
        {
            _resources.Add(entity);
        }

        public override void OnRemoved(Entity entity)
        {
            _resources.Remove(entity);
        }

        [Subscribe]
        public void FindResourceInAreaListener(FindResourceInArea findResourceInArea)
        {
            Point middle = findResourceInArea.Location;
            foreach (var entity in _resources)
            {
                GeoEntity geoEntity = entity.GetComponent<GeoEntity>();
                float distance = Point.Distance(middle, geoEntity.Position);
                if (distance <= findResourceInArea.MaxDistance) findResourceInArea.Results.Add(entity);
            }
        }
        
        [Subscribe]
        public void FindNearestResourceListener(FindNearestResource findNearestResource)
        {
            Point middle = findNearestResource.Location;
            foreach (var entity in _resources)
            {
                GeoEntity geoEntity = entity.GetComponent<GeoEntity>();
                float distance = Point.Distance(middle, geoEntity.Position);
                if (distance <= findNearestResource.MaxDistance)
                {
                    if (findNearestResource.Nearest == null || findNearestResource.Distance > distance)
                    {
                        findNearestResource.Nearest = entity;
                        findNearestResource.Distance = distance;
                    }
                }
            }
        }
    }
}