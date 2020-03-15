using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using OpenHeroesEngine.Game.Models;

namespace OpenHeroesEngine.Game.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw)]
    public class WorldMapSystem : EntitySystem
    {
        private Grid _grid;
        public override void LoadContent()
        {
            _grid = new Grid(512, 512);
        }

        public override void UnloadContent()
        {
        }
    }
}