namespace OpenHeroesEngine.WorldMap.Events
{
    public class TurnBeginEvent
    {
        public readonly int TurnNumber;

        public TurnBeginEvent(int turnNumber)
        {
            TurnNumber = turnNumber;
        }
    }
}