using Artemis;

namespace OpenHeroesEngine.WorldMap.Events.State
{
    public class EndGameEvent : IStatusEvent
    {
        public readonly Entity Winner;

        public EndGameEvent(Entity winner)
        {
            Winner = winner;
        }
    }
}