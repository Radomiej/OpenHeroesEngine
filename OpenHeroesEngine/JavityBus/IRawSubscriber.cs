using System;

namespace Radomiej.JavityBus
{
    public interface IRawSubscriber : IRawInterceptor
    {
        Type GetEventType();

    }
}