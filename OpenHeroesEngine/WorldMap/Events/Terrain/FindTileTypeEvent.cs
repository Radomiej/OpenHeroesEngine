using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events.Terrain
{
    public class FindTileTypeEvent : IFindEvent
    {
        public readonly Point CellPosition;
        public bool Success;
        public int Result;

        public FindTileTypeEvent(Point cellPosition)
        {
            CellPosition = cellPosition;
        }
    }
}