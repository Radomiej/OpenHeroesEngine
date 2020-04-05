using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.Utils;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class OneAliveEndGameSystem : EventBasedSystem
    {
        private int counter = 0;
        [Subscribe]
        public void AddArmyListener(AddArmyEvent addArmyEvent)
        {
            counter++;
        }
        
        [Subscribe]
        public void ArmyLoseListener(ArmyLoseEvent armyLoseEvent)
        {
            counter--;
            if (counter <= 1) EndGame();
        }

        private void EndGame()
        {
            Bag<Entity> results = entityWorld.EntityManager.GetEntities(Aspect.GetOne(typeof(Army)));
            Entity winner = results.Get(0);
            _eventBus.Post(new EndGameEvent(winner));
        }
    }
}