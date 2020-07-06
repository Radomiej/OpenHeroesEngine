using System;
using Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.Utils;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Events.State;
using OpenHeroesEngine.WorldMap.Events.Terrain;
using OpenHeroesEngine.WorldMap.Factories;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.MapReader.SimpleArray
{
    public class ByteArrayMapLoader : IMapLoader
    {
        private byte[,] _map;
        private int pow2Size;
        private int playerNumber = 0;

        private bool aiEnabled = true;

        public ByteArrayMapLoader(byte[,] map)
        {
            _map = map;
            pow2Size = BinarySizeHelper.ComputeSize(Math.Max(map.GetLength(0), map.GetLength(1)));
        }

        public void DisableAi()
        {
            aiEnabled = false;
        }

        public void LoadMap(EntityWorld entityWorld)
        {
            int index = 0;

            for (int x = 0; x < pow2Size; x++)
            {
                for (int y = 0; y < pow2Size; y++)
                {
                    var position = new Point(x, y);
                    SetWater(position);
                }
            }

            // for (int y = _map.size - 1; y >= 0; y--)
            for (int x = 0; x < _map.GetLength(0); x++)
            {
                for (int y = 0; y < _map.GetLength(1); y++)
                {
                    var position = new Point(x, y);
                    position = GetOHPosition(position);
                    var tile = _map[y, x];
                    if (tile == 0) //Water
                    {
                        SetWater(position);
                    }
                    else if (tile == 1) //Ground
                    {
                        SetGround(position);
                    }
                    else SetEntrance(position, tile);

                    index++;
                }
            }

            JEventBus.GetDefault().Post(new WorldLoadedEvent(_map));
        }

        private void SetEntrance(Point position, int titleType)
        {
            if (titleType == 2)
                MapObjectFactory.AddObstacle(new Point(position.X, position.Y),
                    new ObstacleDefinition("Rock", new Point(1, 1)));
            else if (titleType == 3)
                MapObjectFactory.AddArmy((++playerNumber).ToString(), new Point(position.X, position.Y), aiEnabled);
            else if (titleType == 4)
                MapObjectFactory.AddMine(new Point(position.X, position.Y), new ResourceDefinition("Gold"), 100);
            else if (titleType == 5)
                MapObjectFactory.AddMine(new Point(position.X, position.Y), new ResourceDefinition("Wood"), 2);
        }

        private void SetGround(Point position)
        {
            JEventBus.GetDefault().Post(new SetToGroundEvent(position));
        }

        private void SetWater(Point position)
        {
            JEventBus.GetDefault().Post(new SetToWaterEvent(position));
        }

        public int GetMapSize()
        {
            return pow2Size;
        }

        private Point GetOHPosition(Point byteArrayPosition)
        {
            return new Point(byteArrayPosition.X, _map.GetLength(1) - 1 - byteArrayPosition.Y);
        }
    }
}