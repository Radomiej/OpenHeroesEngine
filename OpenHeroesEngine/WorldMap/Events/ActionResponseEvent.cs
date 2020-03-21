using OpenHeroesEngine.WorldMap.Models.Actions;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class ActionResponseEvent
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