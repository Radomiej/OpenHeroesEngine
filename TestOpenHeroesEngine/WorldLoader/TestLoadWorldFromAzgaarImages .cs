using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenHeroesEngine;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.MapReader;
using Radomiej.JavityBus;

namespace TestOpenHeroesEngine.WorldLoader
{
    public class TestLoadWorldFromAzgaarImages
    {
        [SetUp]
        public void Setup()
        {
            JEventBus.GetDefault().ClearAll();
        }

        [Test]
        public void LoadSimple()
        {
            AzgaarMap azgaarMap = new AzgaarMap();
            using (StreamReader r = new StreamReader("Resources/Azgaar/Boy Mely_heightmap.jpeg"))
            {
                Bitmap originalBitmap = new Bitmap(Image.FromStream(r.BaseStream));
                byte[,] grayscaleLayer = BitmapToGrayscale(originalBitmap);
                azgaarMap.Heightmap = grayscaleLayer;
            }
            using (StreamReader r = new StreamReader("Resources/Azgaar/Boy Mely_routes.jpeg"))
            {
                Bitmap originalBitmap = new Bitmap(Image.FromStream(r.BaseStream));
                byte[,] grayscaleLayer = BitmapToGrayscale(originalBitmap);
                azgaarMap.Routes = grayscaleLayer;
            }

            GenerateMap(azgaarMap);
        }

        private byte[,] BitmapToGrayscale(Bitmap originalBitmap)
        {
            int max = 0;
            var scale = new HashSet<byte>();
            byte[,] grayscaleLevel = new byte[originalBitmap.Width, originalBitmap.Height];
            for (int x = 0; x < originalBitmap.Width; x++)
            {
                for (int y = 0; y < originalBitmap.Height; y++)
                {
                    Color color = originalBitmap.GetPixel(x, y);
                    grayscaleLevel[x, y] = originalBitmap.GetPixel(x, y).R;
                    max = Math.Max(max, grayscaleLevel[x, y]);
                    scale.Add(grayscaleLevel[x, y]);

                }
            }

            return grayscaleLevel;
        }

        private void GenerateMap(AzgaarMap map)
        {
            AzgaarMapLoader mapLoader = new AzgaarMapLoader(map);
            var runner = GenericOpenHeroesRunner.CreateInstance(mapLoader);

            for (int i = 0; i < 10; i++)
            {
                runner.Draw();
                runner.Update();
            }
        }
    }
}