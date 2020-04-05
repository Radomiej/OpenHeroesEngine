using System.Collections.Concurrent;
using System.Collections.Generic;
using OpenHeroesServer.WebSocket;

namespace OpenHeroesServer.Server
{
    public class QueueEvents
    {
        public static readonly QueueEvents Instance = new QueueEvents();

        private readonly BlockingCollection<object> _queue;

        private QueueEvents()
        {
            _queue = new BlockingCollection<object>();
        }

        public void Add(object wsMessage)
        {
            _queue.Add(wsMessage);
        }

        public object Take()
        {
            return _queue.Take();
        }

        public object TryTake()
        {
            _queue.TryTake(out var result);
            return result;
        }
    }
}