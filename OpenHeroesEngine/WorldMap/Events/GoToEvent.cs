using System.Collections.Generic;
using Artemis;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class GoToEvent
    {
        public readonly Point Goal;
        public readonly Entity Entity;

        public GoToEvent(Entity entity, Point goal)
        {
            Entity = entity;
            Goal = goal;
        }
    }
}