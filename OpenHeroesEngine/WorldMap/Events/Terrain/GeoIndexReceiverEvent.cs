using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events.Terrain
{
    public class GeoIndexReceiverEvent : IFindEvent
    {
        public readonly Point PointToTranslate;
        public long GeoIndex;

        public GeoIndexReceiverEvent(Point pointToTranslate)
        {
            PointToTranslate = pointToTranslate;
        }
    }
}