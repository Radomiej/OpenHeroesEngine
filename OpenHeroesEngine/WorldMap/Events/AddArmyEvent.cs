using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class AddArmyEvent
    {
        public readonly Point Position;
        public readonly Army Army;

        public AddArmyEvent(Army army, Point position)
        {
            Army = army;
            Position = position;
        }
    }
}