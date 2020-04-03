using System.Collections.Generic;

namespace OpenHeroesEngine.MapReader
{
    public class Player
    {
        public string playerColor { get; set; }
        public List<string> allowedTowns { get; set; }
        public bool isRandomTown { get; set; }
        public bool hasMainTown { get; set; }
        public bool isTownsSet { get; set; }
        public bool? generateHeroAtMainTown { get; set; }
        public bool? generateHero { get; set; }
        public object hasRandomHero { get; set; }
        public object mainCustomHeroId { get; set; }
        public object mainTownType { get; set; }
        public int mainTownX { get; set; }
        public int mainTownY { get; set; }
        public int mainTownZ { get; set; }
    }

    public class Tile
    {
        public string terrain { get; set; }
        public int terrainImageIndex { get; set; }
        public string river { get; set; }
        public int riverImageIndex { get; set; }
        public string road { get; set; }
        public int roadImageIndex { get; set; }
        public List<object> flipConf { get; set; }
    }

    public class Def
    {
        public string spriteName { get; set; }
        public List<int> passableCells { get; set; }
        public List<int> activeCells { get; set; }
        public int placementOrder { get; set; }
        public int objectId { get; set; }
        public int objectClassSubId { get; set; }
        public bool visitable { get; set; }
    }

    public class Object
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public Def def { get; set; }
        public string obj { get; set; }
        public object owner { get; set; }
    }


    public class Homm3Map
    {
        public string version { get; set; }
        public int size { get; set; }
        public bool hasUnderground { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<Player> players { get; set; }
        public object availableArtifacts { get; set; }
        public List<Tile> tiles { get; set; }
        public List<Object> objects { get; set; }
        public Dictionary<string, long> towns { get; set; }
    }
}