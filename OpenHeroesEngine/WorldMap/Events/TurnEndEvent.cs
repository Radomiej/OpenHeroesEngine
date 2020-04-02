namespace OpenHeroesEngine.WorldMap.Events
{
    public class TurnEndEvent
    {
        public readonly int TurnNumber;

        public TurnEndEvent(int turnNumber)
        {
            TurnNumber = turnNumber;
        }
    }
}