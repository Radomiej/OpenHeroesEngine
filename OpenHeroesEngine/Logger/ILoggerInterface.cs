using System.Diagnostics;

namespace OpenHeroesEngine.Logger
{
    public interface ILoggerInterface
    {
        void Debug(string message);
        void Warning(string message);
        void Error(string message);
    }
}