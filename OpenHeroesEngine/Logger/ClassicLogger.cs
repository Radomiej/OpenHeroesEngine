namespace OpenHeroesEngine.Logger
{
    public class ClassicLogger : ILoggerInterface
    {
        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Warning(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Error(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}