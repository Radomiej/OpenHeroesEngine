using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Events;

namespace OpenHeroesEngine.GameSystems.Events.Influence
{
    public class CreateInfluenceCenterEvent : IHardEvent
    {
        public Point Center;
        public float PropagationValue;

        public CreateInfluenceCenterEvent(Point center, float propagationValue)
        {
            Center = center;
            PropagationValue = propagationValue;
        }
    }
}