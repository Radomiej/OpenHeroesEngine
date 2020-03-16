using Artemis.System;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.Artemis
{
    public abstract class EventBasedSystem : EntitySystem
    {
        protected JEventBus _eventBus;
        public override void LoadContent()
        {
            _eventBus = BlackBoard.GetEntry<JEventBus>("EventBus");
            _eventBus.Register(this);
        }
        
        public override void UnloadContent()
        {
            _eventBus.Unregister(this);
        }
    }
}