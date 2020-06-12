using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events.Terrain
{
    public class SetToWaterEvent : IHardEvent
    {
        public readonly Point CellPosition;

        public SetToWaterEvent(Point cellPosition)
        {
            CellPosition = cellPosition;
        }
    }
}