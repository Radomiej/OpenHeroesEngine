using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class AddArmySystem : EventBasedSystem
    {
        [Subscribe]
        public void AddArmyListener(AddArmyEvent addArmyEvent)
        {
            var armyEntity = entityWorld.CreateEntityFromTemplate("Army");
            Army army = armyEntity.GetComponent<Army>();
            army.Creatures = addArmyEvent.Army.Creatures;

            GeoEntity geoEntity = armyEntity.GetComponent<GeoEntity>();
            geoEntity.Position = addArmyEvent.Position;
        }
    }
}