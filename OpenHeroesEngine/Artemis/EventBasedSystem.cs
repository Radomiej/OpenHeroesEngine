using Artemis.System;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.Artemis
{
    public abstract class EventBasedSystem : EntitySystem
    {
        public override void LoadContent()
        {
            JEventBus.GetDefault().Register(this);
        }
        
        public override void UnloadContent()
        {
            JEventBus.GetDefault().Unregister(this);
        }
    }
}