using System.Collections.Generic;
using OpenHeroesEngine.WorldMap.Models.Blockers;

namespace OpenHeroesEngine.WorldMap.Events.Time
{
    public class CanNextTurnEvent : IFindEvent
    {
        public readonly List<ActionBlocker> ActionBlockers = new List<ActionBlocker>();

        public void AddTurnBlocker(ActionBlocker blocker)
        {
            ActionBlockers.Add(blocker);
        }
    }
}