using System;
using NUnit.Framework;
using OpenHeroesEngine;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Events.Obstacles;
using OpenHeroesEngine.WorldMap.Events.Resources;
using OpenHeroesEngine.WorldMap.Events.Structures;
using OpenHeroesEngine.WorldMap.Factories;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace TestOpenHeroesEngine.WorldMap.AI
{
    public class TestGameplayWithAi
    {
        [SetUp]
        public void Setup()
        {
            JEventBus.GetDefault().ClearAll();
        }
        
        [Test]
        public void TestCreateRunnerAndInvokeGameLoop()
        {
            var runner = GenericOpenHeroesRunner.CreateInstance();
            MapObjectFactory.AddArmy("Red", new Point(1, 1));
            MapObjectFactory.AddArmy("Blue", new Point(128, 128));
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

            ResourceDefinition resourceDefinition = new ResourceDefinition("Gold");
            for (int i = 0; i < 15; i++)
            {
                Point position = new Point(random.Next(512), random.Next(512));
                MapObjectFactory.AddMine(position, resourceDefinition);
            }
        }
        
        private void AddPeasantHabitats()
        {
            Random random = new Random(08);

            CreatureDefinition creatureDefinition = new CreatureDefinition("Peasant");
            for (int i = 0; i < 50; i++)
            {
                Point position = new Point(random.Next(512), random.Next(512));
                MapObjectFactory.AddHabitat(position, creatureDefinition);
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
                MapObjectFactory.AddObstacle(position, obstacleDefinition);
            }
        }

        private static void AddObstacleMountains()
        {
            Random random = new Random(789);

            ObstacleDefinition obstacleDefinition = new ObstacleDefinition("Mountain", new Point(2, 3));
            for (int i = 0; i < 25; i++)
            {
                Point position = new Point(random.Next(512), random.Next(512));
                MapObjectFactory.AddObstacle(position, obstacleDefinition);
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
    }
}