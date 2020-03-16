using OpenHeroesEngine.AStar;
using OpenHeroesEngine.Game.Components;

namespace OpenHeroesEngine.Game.Events
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