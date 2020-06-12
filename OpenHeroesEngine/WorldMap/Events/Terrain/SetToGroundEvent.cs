using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events.Terrain
{
    public class SetToGroundEvent : IHardEvent
    {
        public readonly Point CellPosition;

        public SetToGroundEvent(Point cellPosition)
        {
            CellPosition = cellPosition;
        }
    }
}