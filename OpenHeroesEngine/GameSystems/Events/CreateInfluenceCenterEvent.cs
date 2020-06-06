using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.GameSystems.Events
{
    public class CreateInfluenceCenterEvent
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