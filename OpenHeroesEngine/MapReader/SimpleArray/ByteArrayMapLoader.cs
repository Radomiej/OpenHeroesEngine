using System;
using Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.Utils;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Factories;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.MapReader.SimpleArray
{
    public class ByteArrayMapLoader : IMapLoader
    {
        private byte[,] _map;
        private int pow2Size;
        private int playerNumber = 0;

        public ByteArrayMapLoader(byte[,] map)
        {
            _map = map;
            pow2Size = BinarySizeHelper.ComputeSize(Math.Max(map.GetLength(0), map.GetLength(1)));
        }

        public void LoadMap(EntityWorld entityWorld)
        {
            int index = 0;

            // for (int y = _map.size - 1; y >= 0; y--)
            for (int x = 0; x < _map.GetLength(0); x++)
            {
                for (int y = 0; y < _map.GetLength(1); y++)
                {
                    var position = new Point(x, y);
                    var tile = _map[x, y];
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
            if (titleType == 3)
                MapObjectFactory.AddArmy((++playerNumber).ToString(), new Point(position.X, position.Y));
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
    }
}