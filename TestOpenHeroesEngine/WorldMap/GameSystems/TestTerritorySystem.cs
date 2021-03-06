﻿using NUnit.Framework;
using OpenHeroesEngine;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.GameSystems.Api;
using OpenHeroesEngine.GameSystems.Events;
using OpenHeroesEngine.GameSystems.Events.Territory;
using OpenHeroesEngine.MapReader.SimpleArray;
using OpenHeroesEngine.Utils;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace TestOpenHeroesEngine.WorldMap.GameSystems
{
    public class TestTerritorySystem
    {
        private GenericOpenHeroesRunner _runner;

        [SetUp]
        public void Setup()
        {
            JEventBus.GetDefault().ClearAll();
            byte[,] map =
            {
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1}
            };
            var loader = new ByteArrayMapLoader(map);
            _runner = GenericOpenHeroesRunner.CreateInstance(loader);
        }


        [Test]
        public void TestBaseFunctionality()
        {
            Fraction testFraction = new Fraction("test");
            Point testPosition = new Point(1, 1);
            TerritoryChangeEvent territoryChangeEvent = new TerritoryChangeEvent(testPosition, testFraction);
            JEventBus.GetDefault().Post(territoryChangeEvent);

            FindTerritoryOwnerEvent checkTestPositionOwner = new FindTerritoryOwnerEvent(testPosition);
            JEventBus.GetDefault().Post(checkTestPositionOwner);
            Assert.IsTrue(checkTestPositionOwner.Success);
            Assert.IsNotNull(checkTestPositionOwner.Owner);
            Assert.IsTrue(testFraction == checkTestPositionOwner.Owner);

            Assert.AreEqual(1, TerritoryApi.GetTerritoryCellsAmount(testFraction));

            foreach (var utp in new SquareRadiusForeach(testPosition, 1, 8, 8, true).LikePointList())
            {
                FindTerritoryOwnerEvent findTerritoryOwnerEvent = new FindTerritoryOwnerEvent(utp);
                JEventBus.GetDefault().Post(findTerritoryOwnerEvent);
                Assert.IsTrue(findTerritoryOwnerEvent.Success);
                Assert.IsNull(findTerritoryOwnerEvent.Owner);
            }
        }

        [Test]
        public void TestTwoFractionFunctionality()
        {
            Fraction testFraction1 = new Fraction("test1");
            Fraction testFraction2 = new Fraction("test2");
            Point testPosition1 = new Point(1, 1);
            Point testPosition2 = new Point(1, 2);
            TerritoryChangeEvent territoryChangeEvent = new TerritoryChangeEvent(testPosition1, testFraction1);
            JEventBus.GetDefault().Post(territoryChangeEvent);
            territoryChangeEvent = new TerritoryChangeEvent(testPosition2, testFraction2);
            JEventBus.GetDefault().Post(territoryChangeEvent);

            FindTerritoryOwnerEvent checkTestPositionOwner = new FindTerritoryOwnerEvent(testPosition1);
            JEventBus.GetDefault().Post(checkTestPositionOwner);
            Assert.IsTrue(checkTestPositionOwner.Success);
            Assert.IsNotNull(checkTestPositionOwner.Owner);
            Assert.IsTrue(testFraction1 == checkTestPositionOwner.Owner);

            checkTestPositionOwner = new FindTerritoryOwnerEvent(testPosition2);
            JEventBus.GetDefault().Post(checkTestPositionOwner);
            Assert.IsTrue(checkTestPositionOwner.Success);
            Assert.IsNotNull(checkTestPositionOwner.Owner);
            Assert.IsTrue(testFraction2 == checkTestPositionOwner.Owner);

            foreach (var utp in new SquareRadiusForeach(testPosition1, 1, 8, 8, true).LikePointList())
            {
                FindTerritoryOwnerEvent findTerritoryOwnerEvent = new FindTerritoryOwnerEvent(utp);
                JEventBus.GetDefault().Post(findTerritoryOwnerEvent);
                Assert.IsTrue(findTerritoryOwnerEvent.Success);
                if (!utp.Equals(testPosition2)) Assert.IsNull(findTerritoryOwnerEvent.Owner);
            }


            FindNeighboredEvent findNeighbored = new FindNeighboredEvent(testPosition1);
            JEventBus.GetDefault().Post(findNeighbored);
            Assert.IsTrue(findNeighbored.Success);
            Assert.IsNotNull(findNeighbored.NeighborFractions);
            Assert.IsNotNull(findNeighbored.Neighbors);
            Assert.AreEqual(2, findNeighbored.NeighborFractions.Count);
            Assert.AreEqual(8, findNeighbored.Neighbors.Count);
        }

        [Test]
        public void TestEncirclementBug1()
        {
            Fraction testFraction = new Fraction("test");
            Point testPosition = new Point(0, 0);
            PositionBuilder positionBuilder = new PositionBuilder(testPosition);
            TerritoryApi.ChangeTerritoryOwner(testPosition, testFraction);
            TerritoryApi.ChangeTerritoryOwner(positionBuilder.Top(), testFraction);
            TerritoryApi.ChangeTerritoryOwner(positionBuilder.Right(), testFraction);
            TerritoryApi.ChangeTerritoryOwner(positionBuilder.TopRight(), testFraction);

            FindTerritoryOwnerEvent checkTestPositionOwner = new FindTerritoryOwnerEvent(testPosition);
            JEventBus.GetDefault().Post(checkTestPositionOwner);
            Assert.IsTrue(checkTestPositionOwner.Success);
            Assert.IsNotNull(checkTestPositionOwner.Owner);
            Assert.AreEqual(testFraction, checkTestPositionOwner.Owner);
            
            Assert.AreEqual(4, TerritoryApi.GetTerritoryCellsAmount(testFraction));
        }
        
        [Test]
        public void TestEncirclementBug2()
        {
            Fraction testFraction = new Fraction("test");
            Point testPosition = new Point(2, 2);

            foreach (var utp in new SquareRadiusForeach(testPosition, 1, 8, 8).LikePointList())
            {
               TerritoryApi.ChangeTerritoryOwner(utp, testFraction);
            }
            
            FindTerritoryOwnerEvent checkTestPositionOwner = new FindTerritoryOwnerEvent(testPosition);
            JEventBus.GetDefault().Post(checkTestPositionOwner);
            Assert.IsTrue(checkTestPositionOwner.Success);
            Assert.IsNotNull(checkTestPositionOwner.Owner);
            Assert.AreEqual(testFraction, checkTestPositionOwner.Owner);
            
            Assert.AreEqual(9, TerritoryApi.GetTerritoryCellsAmount(testFraction));
        }

    }
}