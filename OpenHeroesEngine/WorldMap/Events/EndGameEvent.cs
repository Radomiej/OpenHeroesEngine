using Artemis;

namespace OpenHeroesEngine.WorldMap.Events
{
    public class EndGameEvent
    {
        public readonly Entity Winner;
        public EndGameEvent(Entity winner)
        {
            Winner = winner;
        }
    }
}