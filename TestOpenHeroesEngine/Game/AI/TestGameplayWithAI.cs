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
            AddArmy();
            AddResources();
            for (int i = 0; i < 1000; i++)
            {
                runner.Draw();
                runner.Update();
            }
        }

        private void AddResources()
        {
            Random random = new Random(123);

            ResourceDefinition resourceDefinition = new ResourceDefinition("Gold");
            for (int i = 0; i < 100; i++)
            {
                Point position = new Point(random.Next(512), random.Next(512));
                Resource resource = new Resource(resourceDefinition);
                AddResourceEvent addResourceEvent =
                    new AddResourceEvent(resource, position);
                JEventBus.GetDefault().Post(addResourceEvent);
            }
        }

        private void AddArmy()
        {
            CreatureDefinition creatureDefinition = new CreatureDefinition("Ork");
            Creature creature = new Creature(creatureDefinition, 10);

            Army army = new Army();
            army.creatures.Add(creature);

            AddArmyEvent addArmyEvent = new AddArmyEvent(army, new Point(1, 1));
            JEventBus.GetDefault().Post(addArmyEvent);
        }
    }
}