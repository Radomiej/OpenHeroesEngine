using System.Collections.Generic;
using System.Diagnostics;
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
    public class StructureAnalyzerSystem : EventBasedSystem
    {
        private Grid _grid;
        private List<Entity> _structures = new List<Entity>(100);

        public StructureAnalyzerSystem() : base(Aspect.All(typeof(Structure), typeof(GeoEntity)))
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _grid = BlackBoard.GetEntry<Grid>("Grid");
        }

        public override void OnAdded(Entity entity)
        {
            _structures.Add(entity);
        }

        public override void OnRemoved(Entity entity)
        {
            Debug.WriteLine("ResourceAnalyzer REMOVE Resource. Exist: " + _structures.Count);
            _structures.Remove(entity);
        }

        [Subscribe]
        public void FindStructureInAreaListener(FindStructureInArea findStructureInArea)
        {
            Point middle = findStructureInArea.Location;
            foreach (var entity in _structures)
            {
                GeoEntity geoEntity = entity.GetComponent<GeoEntity>();
                float distance = Point.Distance(middle, geoEntity.Position);
                if (distance <= findStructureInArea.MaxDistance) findStructureInArea.Results.Add(entity);
            }
        }
        
        [Subscribe]
        public void FindNearestStructureListener(FindNearestStructure findNearestStructure)
        {
            Point middle = findNearestStructure.Location;
            foreach (var entity in _structures)
            {
                GeoEntity geoEntity = entity.GetComponent<GeoEntity>();
                float distance = Point.Distance(middle, geoEntity.Position);
                if (distance <= findNearestStructure.MaxDistance)
                {
                    if (findNearestStructure.Nearest == null || findNearestStructure.Distance > distance)
                    {
                        findNearestStructure.Nearest = entity;
                        findNearestStructure.Distance = distance;
                    }
                }
            }
        }
    }
}