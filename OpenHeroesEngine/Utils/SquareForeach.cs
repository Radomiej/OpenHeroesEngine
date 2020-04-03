using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.Utils
{
    public class SquareForeach
    {
        public delegate void SingleNodeHandler(int x, int y, object Data);

        public object Data;
        public readonly int StartX, StartY, EndX, EndY;

        public SquareForeach(Point position, Point size)
        {
            int positionX = position.X;
            int sizeX = size.X;

            StartX = sizeX > 0 ? positionX : positionX + sizeX;
            EndX = sizeX > 0 ? positionX + sizeX : positionX;

            int positionY = position.Y;
            int sizeY = size.Y;

            StartY = sizeY > 0 ? positionY : positionY + sizeY;
            EndY = sizeY > 0 ? positionY + sizeY : positionY;
        }

        public void ForEach(SingleNodeHandler singleNodeHandler)
        {
            for (int x = StartX; x < EndX; x++)
            {
                for (int y = StartY; y < EndY; y++)
                {
                    singleNodeHandler(x, y, Data);
                }
            }
        }
    }
}