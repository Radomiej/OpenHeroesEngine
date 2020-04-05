using System;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Factories
{
    public class MapObjectFactory
    {
        public static void AddResourcePiles(Point position, string resourceName, int amount = 1)
        {
            ResourceDefinition resourceDefinition = new ResourceDefinition(resourceName);
            Resource resource = new Resource(resourceDefinition);
            AddResourceOnWorldMapEvent addResourceOnWorldMapEvent =
                new AddResourceOnWorldMapEvent(resource, position);
            JEventBus.GetDefault().Post(addResourceOnWorldMapEvent);
        }

        public static void AddArmy(string name, Point startPosition)
        {
            CreatureDefinition creatureDefinition = new CreatureDefinition("Peasant");
            Creature creature = new Creature(creatureDefinition, 10);

            Army army = new Army();
            army.Fraction = new Fraction(name);
            army.Fraction.Resources.Add("Gold", new Resource(new ResourceDefinition("Gold"), 500));
            army.Creatures.Add(creature);

            AddArmyEvent addArmyEvent = new AddArmyEvent(army, startPosition);
            JEventBus.GetDefault().Post(addArmyEvent);
        }

        public static void AddObstacle(Point position, ObstacleDefinition obstacleDefinition)
        {
            Obstacle obstacle = new Obstacle(obstacleDefinition);
            AddObstacleOnWorldMapEvent addObstacleOnWorldMapEvent =
                new AddObstacleOnWorldMapEvent(obstacle, position);
            JEventBus.GetDefault().Post(addObstacleOnWorldMapEvent);
        }

        public static void AddStructure(Point position, string structureName)
        {
            StructureDefinition structureDefinition = new StructureDefinition(structureName, new Point(1, 1));
            Structure structure = new Structure(structureDefinition);
            AddStructureOnWorldMapEvent addStructureOnWorldMapEvent =
                new AddStructureOnWorldMapEvent(structure, position);
            JEventBus.GetDefault().Post(addStructureOnWorldMapEvent);
        }
    }
}