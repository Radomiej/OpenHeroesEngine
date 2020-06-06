using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.GameSystems.Events
{
    public class CreateUrbanEvent
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