﻿using System;
using System.Diagnostics;
using Artemis;
using Artemis.System;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Factories;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.MapReader
{
    public class Homm3MapLoader : IMapLoader
    {
        private Homm3Map _map;
        private int pow2Size;
        private byte[,] terrain;

        private ObstacleDefinition _anyObstacleDefinition = new ObstacleDefinition("TitleLock", new Point(1, 1));
        private ObstacleDefinition _waterObstacleDefinition = new ObstacleDefinition("Water", new Point(1, 1));

        public Homm3MapLoader(Homm3Map map)
        {
            _map = map;
            pow2Size = ComputeSize(_map.size);
            terrain = new byte[_map.size, _map.size];
        }

        private int ComputeSize(in int mapSize)
        {
            for (int i = 4; i < 16; i++)
            {
                int pow2Size = (int) Math.Pow(2, i);
                if (mapSize < pow2Size)
                {
                    return pow2Size;
                }
            }

            throw new NotSupportedException("Map size is so large! " + mapSize);
        }

        public int GetMapSize()
        {
            return pow2Size;
        }

        public void LoadMap(EntityWorld entityWorld)
        {
            LoadTerrain();
            LoadObjects();
            CreateArmies();
            PrintMatrix(terrain);
        }

        private void CreateArmies()
        {
            foreach (var player in _map.players)
            {
                if(player.generateHeroAtMainTown == null) continue;
                MapObjectFactory.AddArmy(player.playerColor, new Point(player.mainTownX, player.mainTownY));
                terrain[player.mainTownX, player.mainTownY] = 5;
            }
        }

        private void LoadTerrain()
        {
            int index = 0;
            
            for (int y = 0; y < _map.size; y++)
            {
                for (int x = 0; x < _map.size; x++)
                {
                    var tile = _map.tiles[index];
                    if (tile.terrain == "Water")
                    {
                        SetWater(new Point(x, y));
                    }

                    if (tile.terrain == "Water") terrain[x, y] = 0;
                    else terrain[x, y] = 1;

                    index++;
                }
            }
        }

        private void PrintMatrix(byte[,] grid)
        {
            int colLength = grid.GetLength(0);
            int rowLength = grid.GetLength(1);
            string arrayString = "";

            for (int y = 0; y < rowLength; y++)
            {
                for (int x = 0; x < colLength; x++)
                {
                    arrayString += string.Format("{0} ", grid[x, y]);
                }

                arrayString += System.Environment.NewLine;
            }

            Debug.Write(arrayString);
        }

        private void LoadObjects()
        {
            foreach (var mapObject in _map.objects)
            {
                if (mapObject.z > 0) continue;
                Point position = new Point(mapObject.x, mapObject.y);
                int sizeX = 1;
                int sizeY = 1;
                int indexZ = 0;

                CreateWorldObject(mapObject, position);
                for (int y = 6; y > 0; y--)
                {
                    for (int x = 8; x > 0; x--)
                    {
                        Point cellPosition = new Point(position.X - (x - 1), position.Y - (y - 1));

                        if (cellPosition.X < 0 || cellPosition.X >= _map.size || cellPosition.Y < 0 ||
                            cellPosition.Y >= _map.size)
                        {
                            indexZ++;
                            continue;
                        }

                        if (mapObject.def.passableCells[indexZ] == 0)
                        {
                            sizeY = Math.Max(sizeY, y);
                            sizeX = Math.Max(sizeX, x);
                            BlockTile(cellPosition);
                            if(terrain[cellPosition.X, cellPosition.Y]  < 2) terrain[cellPosition.X, cellPosition.Y] = 0;
                        }

                        if (mapObject.def.activeCells.Count > 0 && mapObject.def.activeCells[indexZ] == 1)
                        {
                            // terrain[cellPosition.X, cellPosition.Y] = 3;
                        }

                        indexZ++;
                    }
                }

                Point size = new Point(sizeX, sizeY);
                if (sizeX > 1 || sizeY > 1)
                {
                    Debug.WriteLine("Size: " + size);
                }
            }
        }

        private void CreateWorldObject(Object mapObject, Point position)
        {
            
            if(mapObject.def.spriteName.StartsWith("AVTchst0.def")) MapObjectFactory.AddResourcePiles(position, "Chest");
            else if(mapObject.obj.Equals("TREASURE_CHEST")) MapObjectFactory.AddResourcePiles(position, "Chest");
            else if(mapObject.def.spriteName.Equals("adcfra.def")) MapObjectFactory.AddResourcePiles(position, "Chest");
            else if(mapObject.obj.Equals("CAMPFIRE")) MapObjectFactory.AddResourcePiles(position, "Chest");
            else if(mapObject.def.spriteName.StartsWith("avt")) MapObjectFactory.AddResourcePiles(position, "Chest");
            else if(mapObject.obj.Equals("RESOURCE")) MapObjectFactory.AddResourcePiles(position, "Gold");
            else if(mapObject.obj.Equals("RANDOM_RESOURCE")) MapObjectFactory.AddResourcePiles(position, "Gold");
            else if(mapObject.obj.Equals("CREATURE_GENERATOR1")) MapObjectFactory.AddStructure(position, "PeasantHabitat");
            else if(mapObject.def.spriteName.StartsWith("AVG")) MapObjectFactory.AddStructure(position, "PeasantHabitat");
            else if(mapObject.obj.Equals("ARTIFACT")) MapObjectFactory.AddResourcePiles(position, "Gold");
            else return;
            
            terrain[position.X, position.Y] = 4;
        }

        private void SetWater(Point position)
        {
            AddSingleCellObstacle(position, _waterObstacleDefinition);
        }

        private void BlockTile(Point position)
        {
            AddSingleCellObstacle(position, _anyObstacleDefinition);
        }

        private void AddSingleCellObstacle(Point position, ObstacleDefinition obstacleDefinition)
        {
            Obstacle obstacle = new Obstacle(obstacleDefinition);
            AddObstacleOnWorldMapEvent addObstacleOnWorldMapEvent =
                new AddObstacleOnWorldMapEvent(obstacle, position);
            JEventBus.GetDefault().Post(addObstacleOnWorldMapEvent);
        }
    }
}