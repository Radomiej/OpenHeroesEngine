using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;

namespace OpenHeroesEngine.WorldMap.Events.Obstacles
{
    public class AddObstacleOnWorldMapEvent : IHardEvent
    {
        public readonly Point Position;
        public readonly Obstacle Obstacle;

        public AddObstacleOnWorldMapEvent(Obstacle obstacle, Point position)
        {
            Obstacle = obstacle;
            Position = position;
        }
    }
}