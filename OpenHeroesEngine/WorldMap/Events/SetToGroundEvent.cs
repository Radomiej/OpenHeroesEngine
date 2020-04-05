using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class SetToGroundEvent
    {
        public readonly Point CellPosition;

        public SetToGroundEvent(Point cellPosition)
        {
            CellPosition = cellPosition;
        }
    }
}