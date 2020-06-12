namespace OpenHeroesEngine.Logger
{
    public class Logger
    {
        public static ILoggerInterface LoggerInterface;

        public static void Debug(string message)
        {
            LoggerInterface?.Debug(message);
        }

        public static void Warning(string message)
        {
            LoggerInterface?.Warning(message);
        }

        public static void Error(string message)
        {
            LoggerInterface?.Error(message);
        }
    }
}