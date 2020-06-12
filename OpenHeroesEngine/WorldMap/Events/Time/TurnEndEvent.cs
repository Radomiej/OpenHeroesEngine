namespace OpenHeroesEngine.WorldMap.Events.Time
{
    public class TurnEndEvent : IStatusEvent
    {
        public readonly int TurnNumber;

        public TurnEndEvent(int turnNumber)
        {
            TurnNumber = turnNumber;
        }
    }
}