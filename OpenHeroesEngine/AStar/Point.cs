using System;
using System.Collections.Generic;

namespace OpenHeroesEngine.AStar
{
    [Serializable]
    public class Point {

        public int X, Y;

        public Point(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
        }

        protected bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
        

        public static float Distance(Point left, Point right)
        {
            return (float) Math.Sqrt(Math.Pow((right.X - left.X), 2) + Math.Pow((right.Y - left.Y), 2));
        }
        
        public static Point operator +(Point left, Point right)
        {
            return new Point(right.X + left.X, right.Y + left.Y);
        }
        
        public static Point operator -(Point left, Point right)
        {
            return new Point(right.X - left.X, right.Y - left.Y);
        }
    }
}
