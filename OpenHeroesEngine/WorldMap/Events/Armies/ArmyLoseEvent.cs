using Artemis;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.WorldMap.Events.Armies
{
    public class ArmyLoseEvent : IStatusEvent
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