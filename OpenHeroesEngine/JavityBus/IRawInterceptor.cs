namespace Radomiej.JavityBus
{
    public interface IRawInterceptor
    {
        void SubscribeRaw(object incomingEvent);
        int GetPriority();
    }
}