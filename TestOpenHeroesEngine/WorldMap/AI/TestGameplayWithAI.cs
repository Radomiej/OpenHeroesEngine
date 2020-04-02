using System;
using NUnit.Framework;
using OpenHeroesEngine;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace TestOpenHeroesEngine.Game.AI
{
    public class TestGameplayWithAi
    {
        [Test]
        public void TestCreateRunnerAndInvokeGameLoop()
        {
            var runner = GenericOpenHeroesRunner.CreateInstance();
            AddArmy("Red", new Point(1, 1));
            AddArmy("Blue", new Point(128, 128));
            AddBuildings();
            AddResources();
            AddObstacles();
            for (int i = 0; i < 1000; i++)
            {
                runner.Draw();
                runner.Update();
            }
        }

        private void AddBuildings()
        {
            AddGoldMines();
            AddPeasantHabitats();
        }

        private void AddGoldMines()
        {
            Random random = new Random(93);

            StructureDefinition structureDefinition = new StructureDefinition("GoldMine", new Point(2, 1));
            for (int i = 0; i < 15; i++)
            {
                Point position = new Point(random.Next(512), random.Next(512));
                Structure structure = new Structure(structureDefinition);
                AddStructureOnWorldMapEvent addStructureOnWorldMapEvent =
                    new AddStructureOnWorldMapEvent(structure, position);
                JEventBus.GetDefault().Post(addStructureOnWorldMapEvent);
            }
        }
        
        private void AddPeasantHabitats()
        {
            Random random = new Random(93);

            StructureDefinition structureDefinition = new StructureDefinition("PeasantHabitat", new Point(2, 1));
            for (int i = 0; i < 15; i++)
            {
                Point position = new Point(random.Next(512), random.Next(512));
                Structure structure = new Structure(structureDefinition);
                AddStructureOnWorldMapEvent addStructureOnWorldMapEvent =
                    new AddStructureOnWorldMapEvent(structure, position);
                JEventBus.GetDefault().Post(addStructureOnWorldMapEvent);
            }
        }

        private void AddObstacles()
        {
            AddObstacleTrees();
            AddObstacleMountains();
        }

        private static void AddObstacleTrees()
        {
            Random random = new Random(456);

            ObstacleDefinition obstacleDefinition = new ObstacleDefinition("Tree", new Point(1, 1));
            for (int i = 0; i < 100; i++)
            {
                Point position = new Point(random.Next(512), random.Next(512));
                Obstacle obstacle = new Obstacle(obstacleDefinition);
                AddObstacleOnWorldMapEvent addObstacleOnWorldMapEvent =
                    new AddObstacleOnWorldMapEvent(obstacle, position);
                JEventBus.GetDefault().Post(addObstacleOnWorldMapEvent);
            }
        }

        private static void AddObstacleMountains()
        {
            Random random = new Random(789);

            ObstacleDefinition obstacleDefinition = new ObstacleDefinition("Mountain", new Point(2, 3));
            for (int i = 0; i < 25; i++)
            {
                Point position = new Point(random.Next(512), random.Next(512));
                Obstacle obstacle = new Obstacle(obstacleDefinition);
                AddObstacleOnWorldMapEvent addObstacleOnWorldMapEvent =
                    new AddObstacleOnWorldMapEvent(obstacle, position);
                JEventBus.GetDefault().Post(addObstacleOnWorldMapEvent);
            }
        }

        private void AddResources()
        {
            AddGoldPiles();
            AddChests();
        }

        private static void AddGoldPiles()
        {
            Random random = new Random(123);

            ResourceDefinition resourceDefinition = new ResourceDefinition("Gold");
            for (int i = 0; i < 100; i++)
            {
                Point position = new Point(random.Next(512), random.Next(512));
                Resource resource = new Resource(resourceDefinition);
                AddResourceOnWorldMapEvent addResourceOnWorldMapEvent =
                    new AddResourceOnWorldMapEvent(resource, position);
                JEventBus.GetDefault().Post(addResourceOnWorldMapEvent);
            }
        }

        private static void AddChests()
        {
            Random random = new Random(1234);

            ResourceDefinition resourceDefinition = new ResourceDefinition("Chest");
            for (int i = 0; i < 10; i++)
            {
                Point position = new Point(random.Next(512), random.Next(512));
                Resource resource = new Resource(resourceDefinition);
                AddResourceOnWorldMapEvent addResourceOnWorldMapEvent =
                    new AddResourceOnWorldMapEvent(resource, position);
                JEventBus.GetDefault().Post(addResourceOnWorldMapEvent);
            }
        }

        private void AddArmy(string name, Point startPosition)
        {
            CreatureDefinition creatureDefinition = new CreatureDefinition("Ork");
            Creature creature = new Creature(creatureDefinition, 10);

            Army army = new Army();
            army.Fraction = new Fraction(name);
            army.Creatures.Add(creature);

            AddArmyEvent addArmyEvent = new AddArmyEvent(army, startPosition);
            JEventBus.GetDefault().Post(addArmyEvent);
        }
    }
}