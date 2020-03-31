using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class GeoIndexReceiverEvent
    {
        public readonly Point PointToTranslate;
        public long GeoIndex;

        public GeoIndexReceiverEvent(Point pointToTranslate)
        {
            PointToTranslate = pointToTranslate;
        }
    }
}