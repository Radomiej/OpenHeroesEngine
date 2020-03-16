using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.Game.Components;
using OpenHeroesEngine.Game.Events;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.Game.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class AddArmySystem : EventBasedSystem
    {
        [Subscribe]
        public void AddArmyListener(AddArmyEvent addArmyEvent)
        {
            var armyEntity = entityWorld.CreateEntityFromTemplate("Army");
            Army army = armyEntity.GetComponent<Army>();
            army.creatures = addArmyEvent.Army.creatures;

            MovableEntity movableEntity = armyEntity.GetComponent<MovableEntity>();
            movableEntity.Position = addArmyEvent.Position;
        }
    }
}