using System;
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
    public class AzgaarMapLoader : IMapLoader
    {
        private AzgaarMap _map;
        private int Height;
        private int Width;
        private int pow2Size;
        public readonly byte[,] terrain;

        private ObstacleDefinition _anyObstacleDefinition = new ObstacleDefinition("TitleLock", new Point(1, 1));
        private ObstacleDefinition _waterObstacleDefinition = new ObstacleDefinition("Water", new Point(1, 1));

        public AzgaarMapLoader(AzgaarMap map)
        {
            _map = map;
            Width = map.Heightmap.GetLength(0);
            Height = map.Heightmap.GetLength(1);
            pow2Size = ComputeSize(Math.Max(Width, Height));
            terrain = new byte[Width, Height];
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
            // PrintMatrix(terrain);
        }

        private void CreateArmies()
        {
        }

        private void LoadTerrain()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    byte tile = _map.Heightmap[x, y];
                    if (tile == 0)
                    {
                        // SetWater(new Point(x, y));
                        terrain[x, y] = 0;
                    }else if (tile > 100)
                    {
                        BlockTile(new Point(x, y));
                        terrain[x, y] = 2;
                    }
                    else
                    {
                        terrain[x, y] = 1;
                    }
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