using System.Collections.Generic;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.AStar
{
    public interface IPathFinder
    {
        List<PathFinderNode> FindPath(Point start, Point end);
    }
}
