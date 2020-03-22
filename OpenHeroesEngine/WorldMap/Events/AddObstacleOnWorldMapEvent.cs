using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class AddObstacleOnWorldMapEvent
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