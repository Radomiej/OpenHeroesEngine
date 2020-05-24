using System;
using OpenHeroesEngine.Utils;

namespace TestOpenHeroesServer.Internal
{
    public class TypeWaiter
    {
        private readonly Type _type;
        private readonly CountdownLatch _countdownLatch;
        public volatile object Value;
        public TypeWaiter(Type type, CountdownLatch countdownLatch)
        {
            _type = type;
            _countdownLatch = countdownLatch;
        }

        public Type Type1 => _type;

        public void Signal()
        {
            _countdownLatch.Signal();
        }
    }
}