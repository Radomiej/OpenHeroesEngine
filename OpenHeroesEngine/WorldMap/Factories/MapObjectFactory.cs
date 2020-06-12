using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Events.Armies;
using OpenHeroesEngine.WorldMap.Events.Obstacles;
using OpenHeroesEngine.WorldMap.Events.Resources;
using OpenHeroesEngine.WorldMap.Events.Structures;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Factories
{
    public class MapObjectFactory
    {
        private static void SendEvent(JEventBus eventBus, object eventToSend)
        {
            if(eventBus == null) eventBus = JEventBus.GetDefault();
            eventBus.Post(eventToSend);
        }

        public static void AddResourcePiles(Point position, string resourceName, int amount = 1, JEventBus eventBus = null)
        {
            ResourceDefinition resourceDefinition = new ResourceDefinition(resourceName);
            Resource resource = new Resource(resourceDefinition);
            AddResourceOnWorldMapEvent addResourceOnWorldMapEvent =
                new AddResourceOnWorldMapEvent(resource, position);
            
            SendEvent(eventBus, addResourceOnWorldMapEvent);
        }

        public static void AddArmy(string name, Point startPosition, JEventBus eventBus = null)
        {
            CreatureDefinition creatureDefinition = new CreatureDefinition("Peasant");
            Creature creature = new Creature(creatureDefinition, 10);

            Army army = new Army();
            army.Fraction = new Fraction(name);
            army.Fraction.Resources.Add("Gold", new Resource(new ResourceDefinition("Gold"), 500));
            army.Creatures.Add(creature);

            AddArmyEvent addArmyEvent = new AddArmyEvent(army, startPosition);
            SendEvent(eventBus, addArmyEvent);
        }

        public static void AddObstacle(Point position, ObstacleDefinition obstacleDefinition, JEventBus eventBus = null)
        {
            Obstacle obstacle = new Obstacle(obstacleDefinition);
            AddObstacleOnWorldMapEvent addObstacleOnWorldMapEvent =
                new AddObstacleOnWorldMapEvent(obstacle, position);
            SendEvent(eventBus, addObstacleOnWorldMapEvent);
        }

        public static void AddStructure(Point position, string structureName, JEventBus eventBus = null)
        {
            StructureDefinition structureDefinition = new StructureDefinition(structureName, new Point(1, 1));
            Structure structure = new Structure(structureDefinition);
            AddStructureOnWorldMapEvent addStructureOnWorldMapEvent =
                new AddStructureOnWorldMapEvent(structure, position);
            SendEvent(eventBus, addStructureOnWorldMapEvent);
        }
    }
}