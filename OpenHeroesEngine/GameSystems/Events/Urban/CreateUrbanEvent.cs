using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Events;

namespace OpenHeroesEngine.GameSystems.Events.Urban
{
    public class CreateUrbanEvent : IHardEvent
    {
        public Point Position;
        public int Population;
        public float BirdsRate;

        public CreateUrbanEvent(Point position, int population, float birdsRate = 0.1f)
        {
            Position = position;
            Population = population;
            BirdsRate = birdsRate;
        }
    }
}