using System.Collections.Generic;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.GameSystems.Events;
using OpenHeroesEngine.Utils;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;
using static OpenHeroesEngine.Logger.Logger;

namespace OpenHeroesEngine.GameSystems.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class TerritorySystem : EventBasedSystem
    {
        private Grid _grid;
        private Dictionary<Point, Fraction> territoryLinker = new Dictionary<Point, Fraction>(100);
        private HashSet<Point> _borders = new HashSet<Point>();

        public override void LoadContent()
        {
            base.LoadContent();
            _grid = BlackBoard.GetEntry<Grid>("Grid");
        }


        [Subscribe]
        public void FindTerritoryOwnerEventListener(FindTerritoryOwnerEvent findTerritoryOwnerEvent)
        {
            findTerritoryOwnerEvent.Owner = territoryLinker.GetValue(findTerritoryOwnerEvent.Position);
            findTerritoryOwnerEvent.Success = true;
        }

        [Subscribe]
        public void TerritoryChangeListener(TerritoryChangeEvent territoryChangeEvent)
        {
            Fraction oldOwner = territoryLinker.GetValue(territoryChangeEvent.Position);
            Fraction newOwner = territoryChangeEvent.NewOwner;
            territoryLinker[territoryChangeEvent.Position] = newOwner;

            if (oldOwner != null && newOwner != null && !oldOwner.Equals(newOwner))
            {
                Debug($"Territory {territoryChangeEvent.Position} switch from {oldOwner} to {newOwner}");
            }
            else if (oldOwner != null && !oldOwner.Equals(newOwner))
            {
                Debug($"Territory {territoryChangeEvent.Position} lost from {oldOwner}");
            }
            else if (newOwner != null && !newOwner.Equals(oldOwner))
            {
                Debug($"Territory {territoryChangeEvent.Position} gain to {newOwner}");
            }

            UpdateBorder(territoryChangeEvent.Position, newOwner);
        }

        private void UpdateBorder(Point position, Fraction owner, bool deepSearch = true)
        {
            bool isBorder = false;
            SquareRadiusForeach rectangleForeach = new SquareRadiusForeach(position, 1, _grid.Width, _grid.Height);
            rectangleForeach.ForEach((x, y, data) =>
            {
                Point point = new Point(x, y);
                Fraction compareFraction = null;
                if (territoryLinker.ContainsKey(point)) compareFraction = territoryLinker[point];

                if (compareFraction != owner) isBorder = true;
                if (deepSearch) UpdateBorder(point, compareFraction, false);
            });

            territoryLinker[position] = owner;
            
            if (isBorder)
            {
                _borders.Add(position);
            }
            else
            {
                _borders.Remove(position);
            }
        }

        [Subscribe]
        public void FindNeighboredListener(FindNeighboredEvent findNeighbored)
        {
            HashSet<Fraction> _neighboredFraction = new HashSet<Fraction>();
            List<Point> _neighbors = new List<Point>();

            SquareRadiusForeach rectangleForeach =
                new SquareRadiusForeach(findNeighbored.Center, 1, _grid.Width, _grid.Height, true);

            rectangleForeach.ForEach((x, y, data) =>
            {
                Point point = new Point(x, y);
                Fraction compareFraction = null;
                if (territoryLinker.ContainsKey(point)) compareFraction = territoryLinker[point];
                _neighboredFraction.Add(compareFraction);
                _neighbors.Add(point);
            });

            findNeighbored.Neighbors = _neighbors;
            findNeighbored.NeighborFractions = _neighboredFraction;
            findNeighbored.Success = true;
        }
    }
}