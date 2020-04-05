using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class SetToWaterEvent
    {
        public readonly Point CellPosition;

        public SetToWaterEvent(Point cellPosition)
        {
            CellPosition = cellPosition;
        }
    }
}