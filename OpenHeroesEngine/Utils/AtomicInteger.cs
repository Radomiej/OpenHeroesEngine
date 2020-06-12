using System.Threading;

namespace OpenHeroesEngine.Utils
{
    public class AtomicInteger
    {
        private int _atomicValue = 0;

        public int IncrementAndGet()
        {
            return Interlocked.Increment(ref _atomicValue);
        }
        
        public int DecrementAndGet()
        {
            return Interlocked.Decrement(ref _atomicValue);
        }

        public int Get()
        {
            return Interlocked.CompareExchange(ref _atomicValue, 0, 0);
        }
    }
}