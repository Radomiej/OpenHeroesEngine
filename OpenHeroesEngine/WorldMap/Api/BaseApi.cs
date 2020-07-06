using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Api
{
    public class BaseApi
    {
        public static void SendEvent(JEventBus eventBus, object eventToSend)
        {
            if(eventBus == null) eventBus = JEventBus.GetDefault();
            eventBus.Post(eventToSend);
        }
    }
}