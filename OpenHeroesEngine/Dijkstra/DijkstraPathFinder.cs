using System;
using System.Collections.Generic;
using OpenHeroesEngine.AStar;

namespace OpenHeroesEngine.Dijkstra
{
    public class DijkstraPathFinder
    {
        private readonly byte[,] _grid;
        private readonly ushort _gridX;
        private readonly ushort _gridY;

        private Dictionary<Point, Hex> pointToHex;

        public DijkstraPathFinder(byte[,] grid)
        {
            if (grid == null)
            {
                throw new Exception("Grid cannot be null");
            }

            _grid = grid;
            _gridX = (ushort) (_grid.GetUpperBound(0) + 1);
            _gridY = (ushort) (_grid.GetUpperBound(1) + 1);

            pointToHex = new Dictionary<Point, Hex>(_gridX * _gridY * 100);
            for (int x = 0; x < _gridX; x += 1)
            {
                for (int y = 0; y < _gridY; y += 1)
                {
                    Point coord = new Point(x, y);
                    Hex hex = new Hex(coord);
                    hex.MovementCost = _grid[x, y];
                    pointToHex.Add(coord, hex);
                }
            }
        }

        public HexMovementInfo GetHexesInMovementRange(Point startingPosition, int movementRange)
        {
            Hex startingHex = GetHex(startingPosition);
            // Modified Dijkstra's Algorithm AKA Uniform Cost Search
            List<Hex> results = new List<Hex>();
            results.Add(startingHex);

            BucketPriority<Hex> frontier = new BucketPriority<Hex>(128, true);
            frontier.Enqueue(0, startingHex);
            Dictionary<Hex, int> costSoFar = new Dictionary<Hex, int>();
            Dictionary<Hex, Hex> cameFrom = new Dictionary<Hex, Hex>();

            costSoFar[startingHex] = 0;

            while (!frontier.Empty())
            {
                Hex currentHex = frontier.Dequeue();
                List<Hex> hexNeighbors = GetHexNeighbors(currentHex.coords);
                if (hexNeighbors.Count > 0)
                {
                    foreach (Hex nextHex in hexNeighbors)
                    {
                        int newCost = costSoFar[currentHex] + nextHex.MovementCost;
                        if ((!costSoFar.ContainsKey(nextHex) || newCost < costSoFar[nextHex]) &&
                            newCost < movementRange && nextHex.MovementCost != 0)
                        {
                            costSoFar[nextHex] = newCost;
                            int newPriority = newCost;
                            frontier.Enqueue(newPriority, nextHex);
                            cameFrom[nextHex] = currentHex;
                            results.Add(nextHex);
                        }
                    }
                }
            }

            return new HexMovementInfo(results, cameFrom);
        }

        public List<Hex> GetHexNeighbors(Point axialCoords)
        {
            List<Hex> neighbors = new List<Hex>();
            Hex currentHex = GetHex(axialCoords);
            for (int i = 0; i < 6; i++)
            {
                Point neighborCoords = currentHex.GetNeighborCoords(i);
                if (!object.ReferenceEquals(GetHex(neighborCoords), null))
                {
                    neighbors.Add(GetHex(neighborCoords));
                }
            }

            return neighbors;
        }

        private Hex GetHex(Point neighborCoords)
        {
            return pointToHex[neighborCoords];
        }

        public List<Hex> Find(Point start, Point end, HexMovementInfo movementInfo)
        {
            Dictionary<Hex, Hex> selectedUnitsTileReturnPath = movementInfo.tileReturnPath;

            List<Hex> movementPath = new List<Hex>();
            int movementCost = 0;
            Hex returnTile = GetHex(end);

            Hex startHex = GetHex(start);
            if (returnTile != startHex)
            {
                movementPath.Add(returnTile);
                movementCost += returnTile.MovementCost;
                while (selectedUnitsTileReturnPath[returnTile] != startHex)
                {
                    returnTile = selectedUnitsTileReturnPath[returnTile];
                    movementPath.Add(returnTile);
                    movementCost += returnTile.MovementCost;
                }

                movementPath.Add(startHex);
                movementCost += returnTile.MovementCost;
            }

            movementPath.Reverse();
            return movementPath;
        }
    }
}