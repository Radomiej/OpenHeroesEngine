﻿using OpenHeroesEngine.AStar;

 namespace OpenHeroesEngine.Utils
{
    public class PositionBuilder
    {
        public readonly Point Center;

        public PositionBuilder(Point center)
        {
            Center = center;
        }

        public Point Left()
        {
            return Center + new Point(-1, 0);
        }
        
        public Point Right()
        {
            return Center + new Point(1, 0);
        }
        
        public Point Top()
        {
            return Center + new Point(0, 1);
        }
        
        public Point TopLeft()
        {
            return Center + new Point(-1, 1);
        }
        
        public Point TopRight()
        {
            return Center + new Point(1, 1);
        }
        
        public Point Bottom()
        {
            return Center + new Point(0, -1);
        }
        
        public Point BottomLeft()
        {
            return Center + new Point(-1, -1);
        }
        
        public Point BottomRight()
        {
            return Center + new Point(1, -1);
        }
    }
}