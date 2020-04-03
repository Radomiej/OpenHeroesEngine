using System;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.Utils
{
    public class DistanceHelper
    {
        public static int EuclideanDistance(Point vector)
        {
            return (int) Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
        }

        public static int EuclideanDistance(Point start, Point end)
        {
            int x = start.X - end.X;
            int y = start.Y - end.Y;
            return (int) Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
    }
}