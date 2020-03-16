using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using OpenHeroesEngine.WorldMap.Components;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class ArmyAiSystem : EntityProcessingSystem
    {
        public ArmyAiSystem() : base(Aspect.All(typeof(Army), typeof(MovableEntity)))
        {
        }

        public override void Process(Entity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}