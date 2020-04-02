using System.Collections.Generic;
using OpenHeroesEngine.WorldMap.Models.Blockers;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class CanNextTurnEvent
    {
        public readonly List<ActionBlocker> ActionBlockers = new List<ActionBlocker>();

        public void AddTurnBlocker(ActionBlocker blocker)
        {
            ActionBlockers.Add(blocker);
        }
    }
}