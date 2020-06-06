namespace OpenHeroesEngine.MapReader.SimpleArray
{
    public class ByteArrayHelper
    {
        public static byte[,] CreateBase(int size = 512, byte defaultValue = 1)
        {
            byte[,] mapBase = new byte[size, size];
            for (int x = 0; x < mapBase.GetLength(0); x += 1)
            {
                for (int y = 0; y < mapBase.GetLength(1); y += 1)
                {
                    mapBase[x, y] = defaultValue;
                }
            }

            return mapBase;
        }
    }
}