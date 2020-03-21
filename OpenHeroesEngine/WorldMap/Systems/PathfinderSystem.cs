﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class PathfinderSystem : EventBasedSystem
    {
        private PathFinder _pathFinder;
        private Grid _grid;

        private Dictionary<long, Entity> _nodeToEntityLinker = new Dictionary<long, Entity>(500);

        [Subscribe]
        public void WorldLoadedListener(WorldLoadedEvent worldLoadedEvent)
        {
            _grid = worldLoadedEvent.Grid;
            byte[,] calculateGrid = CalculateGrid(_grid);
            _pathFinder = new PathFinder(calculateGrid);
        }

        private byte[,] CalculateGrid(Grid grid)
        {
            byte[,] byteGrid = new byte[grid.Width, grid.Height];
            for (int x = 0; x < byteGrid.GetLength(0); x += 1)
            {
                for (int y = 0; y < byteGrid.GetLength(1); y += 1)
                {
                    byteGrid[x, y] = 1;
                }
            }

            return byteGrid;
        }

        [Subscribe]
        public void FindPathListener(FindPathEvent findPathEvent)
        {
            var result = _pathFinder.FindPath(findPathEvent.Start, findPathEvent.End);
            findPathEvent.CalculatedPath = result.Select(step => new Point(step.X, step.Y)).ToList();
            findPathEvent.CalculatedPath.Reverse();
        }

        [Subscribe]
        public void IsFreeAreaListener(IsFreeAreaEvent isFreeAreaEvent)
        {
            int positionX = isFreeAreaEvent.Position.X;
            int sizeX = isFreeAreaEvent.Size.X;

            int startX = sizeX > 0 ? positionX : positionX + sizeX;
            int endX = sizeX > 0 ? positionX + sizeX : positionX;

            int positionY = isFreeAreaEvent.Position.Y;
            int sizeY = isFreeAreaEvent.Size.Y;

            int startY = sizeY > 0 ? positionY : positionY + sizeY;
            int endY = sizeY > 0 ? positionY + sizeY : positionY;

            for (int x = startX; x < endX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    var index = _grid.GetNodeIndex(x, y);
                    if (_nodeToEntityLinker.ContainsKey(index))
                    {
                        return;
                    }
                }
            }

            isFreeAreaEvent.IsFree = true;
        }

        [Subscribe]
        public void PlaceObjectOnMapListener(PlaceObjectOnMapEvent placeObjectOnMapEvent)
        {
            SquareForeach squareForeach = new SquareForeach(placeObjectOnMapEvent.Position, placeObjectOnMapEvent.Size);
            squareForeach.Data = placeObjectOnMapEvent;
            squareForeach.ForEach(PlaceObject);
        }

        private void PlaceObject(int x, int y, object data)
        {
            PlaceObjectOnMapEvent placeObjectOnMapEvent = data as PlaceObjectOnMapEvent;
            var index = _grid.GetNodeIndex(x, y);
            _nodeToEntityLinker.Add(index, placeObjectOnMapEvent.Entity);
            _pathFinder.ChangeCostOfMove(x, y, 0);
        }
    }
}