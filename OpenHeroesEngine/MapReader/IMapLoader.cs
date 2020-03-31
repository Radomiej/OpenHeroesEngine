using Artemis;

namespace OpenHeroesEngine.MapReader
{
    public interface IMapLoader
    {
        void LoadMap(EntityWorld entityWorld);
        int GetMapSize();
    }
}