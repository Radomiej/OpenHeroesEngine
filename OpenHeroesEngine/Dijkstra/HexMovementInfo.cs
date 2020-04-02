using System.Collections.Generic;

namespace OpenHeroesEngine.Dijkstra
{
    public class HexMovementInfo {
        public List<Hex> traversableTiles { get; private set; }
        public Dictionary<Hex, Hex> tileReturnPath { get; private set; }

        public HexMovementInfo(List<Hex> traversableTiles, Dictionary<Hex, Hex> tileReturnPath) {
            this.traversableTiles = traversableTiles;
            this.tileReturnPath = tileReturnPath;
        }
    }
}