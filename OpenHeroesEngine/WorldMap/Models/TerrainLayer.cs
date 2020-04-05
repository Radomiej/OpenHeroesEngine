namespace OpenHeroesEngine.WorldMap.Models
{
    public class TerrainLayer
    {
        public readonly byte[,] Terrain;

        public TerrainLayer(Grid grid)
        {
            this.Terrain = new byte[grid.Width, grid.Height];
            // for (int x = 0; x < Terrain.GetLength(0); x += 1)
            // {
            //     for (int y = 0; y < Terrain.GetLength(1); y += 1)
            //     {
            //         Terrain[x, y] = 1;
            //     }
            // }

        }
    }
}