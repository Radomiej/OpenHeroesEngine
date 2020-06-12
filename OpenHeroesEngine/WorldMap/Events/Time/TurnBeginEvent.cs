namespace OpenHeroesEngine.WorldMap.Events.Time
{
    public class TurnBeginEvent : IStatusEvent
    {
        public readonly int TurnNumber;

        public TurnBeginEvent(int turnNumber)
        {
            TurnNumber = turnNumber;
        }
    }
}