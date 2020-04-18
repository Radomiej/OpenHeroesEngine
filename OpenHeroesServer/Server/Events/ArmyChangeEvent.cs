using OpenHeroesEngine.WorldMap.Components;

namespace OpenHeroesServer.Server.Events
{
    public class ArmyChangeEvent
    {
        public Army Army;
        public GeoEntity GeoEntity;

        public ArmyChangeEvent(Army army, GeoEntity geoEntity)
        {
            Army = army;
            GeoEntity = geoEntity;
        }
    }
}