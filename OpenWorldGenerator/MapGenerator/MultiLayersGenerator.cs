using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using OpenWorldGenerator.CustomizablePerlin;
using SimplexNoise;

namespace OpenWorldGenerator.MapGenerator
{
    public class MultiLayersGenerator
    {
        private readonly Random _random;
        private readonly int _mapSize = 16;

        List<byte[,]> layers = new List<byte[,]>(100);

        public MultiLayersGenerator(int seed)
        {
            _random = new Random(seed);
        }

        public void Generate()
        {
            byte[,] layer1 = GenerateSimpleNoise();
            layer1 = CompressLayer(layer1, 50, 85, 125, 140, 155, 180, 200);
            DebugPrintArray(layer1, "X1");
            layers.Add(layer1);

            byte[,] layer2 = GenerateCustomizablePerlinNoise();
            CompressLayer(layer2, 50, 85, 125, 140, 155, 180, 200);
            DebugPrintArray(layer2, "X1");
            layers.Add(layer2);

            byte[,] layer3 = GenerateCustomizablePerlinNoise();
            CompressLayer(layer3, 50, 85, 125, 140, 155, 180, 200);
            DebugPrintArray(layer3, "X1");
            layers.Add(layer3);
        }

        private byte[,] CompressLayer(byte[,] grid, params byte[] ranges)
        {
            for (int x = 0; x < _mapSize; x++)
            {
                for (int y = 0; y < _mapSize; y++)
                {
                    byte raw = grid[x, y];
                    byte smoothValue = 0;
                    foreach (var range in ranges)
                    {
                        if (raw < range)
                        {
                            break;
                        }
                        smoothValue++;
                    }
                    
                    grid[x, y] = smoothValue;
                }
            }

            return grid;
        }

        private byte[,] GenerateCustomizablePerlinNoise()
        {
            int seed = _random.Next(); // Change to whatever
            int octaves = 1; // Number of layers of perlin noise (stick with 1 for now)
            double amplitude = 256; // affects world height (default 4)
            double persistence = 1; // How much it stays at a particular height. Only has any affect when octaves > 1
            double frequency = 0.423; // Adjust for mountains/hills/plains (default 0.01)

            var perlinNoise = new CustomizableNoise(persistence, frequency, amplitude, octaves, seed);

            byte[,] results = new byte[_mapSize, _mapSize];

            for (int x = 0; x < _mapSize; x++)
            {
                for (int y = 0; y < _mapSize; y++)
                {
                    double noise = Math.Abs(perlinNoise.Get2D(x, y));
                    results[x, y] = (byte) noise;
                }
            }

            return results;
        }

        private byte[,] GenerateSimpleNoise()
        {
            Noise.Seed = _random.Next();
            float[,] values = Noise.Calc2D(_mapSize, _mapSize, 0.01f);
            byte[,] results = new byte[_mapSize, _mapSize];

            for (int x = 0; x < _mapSize; x++)
            {
                for (int y = 0; y < _mapSize; y++)
                {
                    results[x, y] = (byte) values[x, y];
                }
            }

            return results;
        }

        private void DebugPrintArray(byte[,] arr, string digitFormat = "X2")
        {
            int rowLength = arr.GetLength(0);
            int colLength = arr.GetLength(1);

            StringBuilder stringBuilder = new StringBuilder(10000);
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    stringBuilder.Append(string.Format("{0} ", arr[i, j].ToString(digitFormat)));
                }

                stringBuilder.Append(Environment.NewLine);
            }
            
            Debug.Write(stringBuilder.ToString());
        }
    }
}