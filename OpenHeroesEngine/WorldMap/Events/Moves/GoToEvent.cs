using Artemis;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events.Moves
{
    public class GoToEvent : ISoftEvent
    {
        public readonly Point Goal;
        public readonly Entity Entity;
        public bool Complete { get; set; }


        public GoToEvent(Entity entity, Point goal)
        {
            Entity = entity;
            Goal = goal;
        }
    }
}