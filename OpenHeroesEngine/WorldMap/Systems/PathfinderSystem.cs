﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.Utils;
using OpenHeroesEngine.WorldMap.Api;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Events.Moves;
using OpenHeroesEngine.WorldMap.Events.Obstacles;
using OpenHeroesEngine.WorldMap.Events.State;
using OpenHeroesEngine.WorldMap.Events.Terrain;
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

        public override void LoadContent()
        {
            base.LoadContent();
            _grid = BlackBoard.GetEntry<Grid>("Grid");
            byte[,] calculateGrid = CalculateGrid(_grid);
            _pathFinder = new PathFinder(calculateGrid);
        }

        [Subscribe]
        public void WorldLoadedListener(WorldLoadedEvent worldLoadedEvent)
        {
            //TODO use or remove
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
            byte costOfMove = _pathFinder.GetCostOfMove(findPathEvent.End.X, findPathEvent.End.Y);
            
            bool entranceMode = TerrainApi.IsGround(findPathEvent.End) && StructureApi.IsEntrance(findPathEvent.End);
            if (entranceMode)
            {
                _pathFinder.ChangeCostOfMove(findPathEvent.End.X, findPathEvent.End.Y, 1);
            }

            var result = _pathFinder.FindPath(findPathEvent.Start, findPathEvent.End);
            if (result != null)
            {
                findPathEvent.CalculatedPath = result.Select(step => new Point(step.X, step.Y)).ToList();
                findPathEvent.CalculatedPath.Reverse();
            }

            if (entranceMode)
            {
                _pathFinder.ChangeCostOfMove(findPathEvent.End.X, findPathEvent.End.Y, costOfMove);
            }
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

            //Bound checker
            if (startX < 0 || endX >= _grid.Width) return;
            if (startY < 0 || endY >= _grid.Height) return;

            //Obstackle checker
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
        public void SetToWaterListener(SetToWaterEvent setToWater)
        {
            _pathFinder.ChangeCostOfMove(setToWater.CellPosition.X, setToWater.CellPosition.Y, 0);
        }
        
        [Subscribe]
        public void SetToGroundListener(SetToGroundEvent setToGround)
        {
            _pathFinder.ChangeCostOfMove(setToGround.CellPosition.X, setToGround.CellPosition.Y, 1);
        }

        [Subscribe]
        public void PlaceObjectOnMapListener(PlaceObjectOnMapEvent placeObjectOnMapEvent)
        {
            RectangleForeach rectangleForeach = new RectangleForeach(placeObjectOnMapEvent.Position, placeObjectOnMapEvent.Size);
            rectangleForeach.Data = placeObjectOnMapEvent;
            rectangleForeach.ForEach(PlaceObject);
        }

        private void PlaceObject(int x, int y, object data)
        {
            PlaceObjectOnMapEvent placeObjectOnMapEvent = data as PlaceObjectOnMapEvent;
            var index = _grid.GetNodeIndex(x, y);
            if (_nodeToEntityLinker.ContainsKey(index))
            {
                Debug.WriteLine("Attend to override object");
            }

            _nodeToEntityLinker.Add(index, placeObjectOnMapEvent.Entity);
            _pathFinder.ChangeCostOfMove(x, y, 0);
        }

        private void UnplaceObject(int x, int y)
        {
            var index = _grid.GetNodeIndex(x, y);
            _nodeToEntityLinker.Remove(index);
            _pathFinder.ChangeCostOfMove(x, y, 1);
        }
    }
}