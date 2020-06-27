using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;

namespace OpenHeroesEngine.WorldMap.Events.Armies
{
    public class AddArmyEvent : IHardEvent
    {
        public readonly Point Position;
        public readonly Army Army;
        public readonly bool AddAi;

        public AddArmyEvent(Army army, Point position, bool addAi)
        {
            Army = army;
            Position = position;
            this.AddAi = addAi;
        }
    }
}