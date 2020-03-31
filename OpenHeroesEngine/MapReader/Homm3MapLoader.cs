using System;
using System.Diagnostics;
using Artemis;
using Artemis.System;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.MapReader
{
    public class Homm3MapLoader : IMapLoader
    {
        private Homm3Map _map;
        private int pow2Size;

        private ObstacleDefinition _anyObstacleDefinition = new ObstacleDefinition("TitleLock", new Point(1, 1));
        private ObstacleDefinition _waterObstacleDefinition = new ObstacleDefinition("Water", new Point(1, 1));
        public Homm3MapLoader(Homm3Map map)
        {
            _map = map;
            pow2Size = ComputeSize(_map.size);
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
        }

        private void LoadTerrain()
        {
            int index = 0;
            for (int x = 0; x < _map.size; x++)
            {
                for (int y = 0; y < _map.size; y++)
                {
                    var tile = _map.tiles[index];
                    if(tile.terrain == "Water") SetWater(new Point(x, y));
                    
                    index++;
                }
            }
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
                for (int y = 6; y > 0; y--)
                {
                    for (int x = 8; x > 0; x--)
                    {
                        if (mapObject.def.passableCells[indexZ] == 0)
                        {
                            sizeY = Math.Max(sizeY, y);
                            sizeX = Math.Max(sizeX, x);
                            Point cellPosition = new Point(position.X - (sizeX - 1), position.Y - (sizeY - 1));
                            BlockTile(cellPosition);
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

        private void SetWater(Point position)
        {
            AddSingleCellObstackle(position, _waterObstacleDefinition);
        }

        private void BlockTile(Point position)
        {
            AddSingleCellObstackle(position, _anyObstacleDefinition);
        }

        private void AddSingleCellObstackle(Point position, ObstacleDefinition obstacleDefinition)
        {
            Obstacle obstacle = new Obstacle(obstacleDefinition);
            AddObstacleOnWorldMapEvent addObstacleOnWorldMapEvent =
                new AddObstacleOnWorldMapEvent(obstacle, position);
            JEventBus.GetDefault().Post(addObstacleOnWorldMapEvent);
        }
    }
}