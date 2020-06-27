namespace OpenHeroesEngine.WorldMap.Events.Time
{
    public class TurnBeforeUpdateEvent : IStatusEvent
    {
        public readonly int TurnNumber;

        public TurnBeforeUpdateEvent(int turnNumber)
        {
            TurnNumber = turnNumber;
        }
    }
}