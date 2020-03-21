using System.Collections.Generic;

namespace OpenHeroesEngine.WorldMap.Models.Actions
{
    public class Action
    {
        public readonly ActionDefinition Definition;
        public readonly Dictionary<string, object> Params = new Dictionary<string, object>();

        public Action(ActionDefinition definition)
        {
            Definition = definition;
        }

        public void AddParam(string key, object value)
        {
            Params.Add(key, value);
        }
    }
}