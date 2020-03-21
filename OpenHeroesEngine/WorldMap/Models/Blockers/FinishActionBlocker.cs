using OpenHeroesEngine.WorldMap.Events;

namespace OpenHeroesEngine.WorldMap.Models.Blockers
{
    public class FinishActionBlocker : ActionBlocker
    {
        public readonly DynamicActionEvent ActionToFinish;

        public FinishActionBlocker(DynamicActionEvent actionToFinish)
        {
            ActionToFinish = actionToFinish;
        }
    }
}