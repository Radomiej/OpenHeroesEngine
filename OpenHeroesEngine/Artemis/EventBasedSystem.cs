using Artemis;
using Artemis.System;
using Radomiej.JavityBus;
using static OpenHeroesEngine.Logger.Logger;

namespace OpenHeroesEngine.Artemis
{
    public abstract class EventBasedSystem : EntitySystem
    {
        protected JEventBus _eventBus;

        protected EventBasedSystem()
        {
        }
        
        protected EventBasedSystem(Aspect aspect) : base(aspect)
        {
        }

        public override void LoadContent()
        {
            _eventBus = BlackBoard.GetEntry<JEventBus>("EventBus") ?? JEventBus.GetDefault();
            _eventBus.Register(this);
            Debug("System Loaded: " + GetType().Name);
        }
        
        public override void UnloadContent()
        {
            _eventBus.Unregister(this);
        }
    }
}