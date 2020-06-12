namespace OpenHeroesEngine.WorldMap.Models
{
    public class TerrainLayer
    {
        public readonly byte[,] Terrain;


        public TerrainLayer()
        {
            
        }
        public TerrainLayer(Grid grid)
        {
            this.Terrain = new byte[grid.Width, grid.Height];
        }
    }
}