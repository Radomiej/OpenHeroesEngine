namespace OpenHeroesEngine.WorldMap.Events.Time
{
    public class TurnAfterUpdateEvent : IStatusEvent
    {
        public readonly int TurnNumber;

        public TurnAfterUpdateEvent(int turnNumber)
        {
            TurnNumber = turnNumber;
        }
    }
}