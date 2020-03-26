using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class ArmyManagerSystem : EventBasedSystem
    {
        [Subscribe]
        public void AddArmyListener(AddArmyEvent addArmyEvent)
        {
            var armyEntity = entityWorld.CreateEntityFromTemplate("Army", addArmyEvent.Army, addArmyEvent.Position);
            Army army = armyEntity.GetComponent<Army>();
            army.Creatures = addArmyEvent.Army.Creatures;
        }
        
        [Subscribe]
        public void ArmyLoseListener(ArmyLoseEvent armyLoseEvent)
        {
            entityWorld.DeleteEntity(armyLoseEvent.Army);
        }
    }
}