using System;

namespace Radomiej.JavityBus
{
    public interface IJEventSubscriberRaw
    {
        void SubscribeRaw(object incomingEvent);
        Type GetEventType();

        int GetPriority();
    }
}