using System;
using System.Collections.Generic;
using Artemis;
using OpenHeroesEngine.WorldMap.Models.Actions;
using Action = OpenHeroesEngine.WorldMap.Models.Actions.Action;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class DynamicActionEvent
    {
        public readonly string Id = Guid.NewGuid().ToString();
        public readonly Action Action;
        public readonly List<ActionAnswer> ActionAnswers;
        public readonly Entity Target;

        public DynamicActionEvent(Action action, List<ActionAnswer> actionAnswers, Entity target = null)
        {
            Action = action;
            ActionAnswers = actionAnswers;
            Target = target;
        }
    }
}