using System;
using System.Collections.Generic;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.Utils
{
    public class SquareRadiusForeach
    {
        private readonly bool _omitMiddle;

        public delegate void SingleNodeHandler(int x, int y, object Data);

        public object Data;
        public readonly int StartX, StartY, EndX, EndY;
        public readonly int X, Y;

        public SquareRadiusForeach(Point position, int squareRadiusSize, int maxX, int maxY, bool omitMiddle = false)
        {
            if (squareRadiusSize < 0)
            {
                throw new NotSupportedException("squareRadiusSize must be 0 or great that 0");
            }

            _omitMiddle = omitMiddle;
            

            int positionX = position.X;
            X = positionX;

            StartX = positionX - squareRadiusSize;
            if (StartX < 0) StartX = 0;
            EndX = positionX + squareRadiusSize;
            if (EndX >= maxX) EndX = maxX;

            int positionY = position.Y;
            Y = positionY;

            StartY = positionY - squareRadiusSize;
            if (StartY < 0) StartY = 0;
            EndY = positionY + squareRadiusSize;
            if (EndY >= maxY) EndY = maxY;
        }

        public void ForEach(SingleNodeHandler singleNodeHandler)
        {
            for (int x = StartX; x <= EndX; x++)
            {
                for (int y = StartY; y <= EndY; y++)
                {
                    if(_omitMiddle && IsMiddle(x, y)) continue;
                    singleNodeHandler(x, y, Data);
                }
            }
        }

        private bool IsMiddle(int x, int y)
        {
            return x == X && y == Y;
        }

        public List<Point> LikePointList()
        {
            List<Point> results = new List<Point>();
            for (int x = StartX; x <= EndX; x++)
            {
                for (int y = StartY; y <= EndY; y++)
                {
                    if(_omitMiddle && IsMiddle(x, y)) continue;
                    results.Add(new Point(x, y));
                }
            }

            return results;
        }

        public override string ToString()
        {
            return $"{nameof(StartX)}: {StartX}, {nameof(StartY)}: {StartY}, {nameof(EndX)}: {EndX}, {nameof(EndY)}: {EndY}";
        }
    }
}