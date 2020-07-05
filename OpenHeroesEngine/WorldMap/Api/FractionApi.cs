using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.WorldMap.Api
{
    public class FractionApi
    {
        public static int GetFractionAmountOfResource(Fraction fraction, ResourceDefinition resourceDefinition)
        {
            if (!fraction.Resources.ContainsKey(resourceDefinition.Name))
            {
                //The faction has no resources of this type
                return 0;
            }

            return fraction.Resources[resourceDefinition.Name].Amount;
        }
    }
}