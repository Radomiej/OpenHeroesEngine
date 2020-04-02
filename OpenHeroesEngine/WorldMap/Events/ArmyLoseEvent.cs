using Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class ArmyLoseEvent
    {
        public readonly Point Position;
        public readonly Entity Army;

        public ArmyLoseEvent(Entity army, Point position)
        {
            Army = army;
            Position = position;
        }
    }
}