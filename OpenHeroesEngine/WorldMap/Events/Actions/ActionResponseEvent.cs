using OpenHeroesEngine.WorldMap.Models.Actions;

namespace OpenHeroesEngine.WorldMap.Events.Actions
{
    public class ActionResponseEvent : IHardEvent
    {
        public readonly DynamicActionEvent ActionEvent;
        public readonly ActionAnswer SelectedAnswer;

        public ActionResponseEvent(DynamicActionEvent actionEvent, ActionAnswer selectedAnswer)
        {
            ActionEvent = actionEvent;
            SelectedAnswer = selectedAnswer;
        }
    }
}