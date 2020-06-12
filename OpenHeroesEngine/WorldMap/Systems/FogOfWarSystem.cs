using System.Collections.Generic;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.Utils;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Events.Fractions;
using OpenHeroesEngine.WorldMap.Events.Moves;
using OpenHeroesEngine.WorldMap.Events.Time;
using OpenHeroesEngine.WorldMap.Models;
using Radomiej.JavityBus;
using static OpenHeroesEngine.Logger.Logger;

namespace OpenHeroesEngine.WorldMap.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class FogOfWarSystem : EventBasedSystem
    {
        private Grid _grid;
        private HashSet<Fraction> _fractions = new HashSet<Fraction>();

        public override void LoadContent()
        {
            base.LoadContent();
            _grid = BlackBoard.GetEntry<Grid>("Grid");
            Debug("System Loaded: " + GetType().Name);
        }

        [Subscribe]
        public void NewFractionListener(NewFractionEvent newFractionEvent)
        {
            Fraction fraction = newFractionEvent.NewFraction;
            _fractions.Add(newFractionEvent.NewFraction);
            fraction.FogOfWar = new byte[_grid.Width, _grid.Height];
            ExpandSeeArea(newFractionEvent.Position, fraction);
        }

        [Subscribe]
        public void MoveInListener(MoveInEvent moveInEvent)
        {
            Point position = moveInEvent.Current;
            Fraction fraction = moveInEvent.MoveToNextEvent.Owner.GetComponent<Army>().Fraction;
            ExpandSeeArea(position, fraction);
        }

        public void ExpandSeeArea(Point position, Fraction fraction)
        {
            int sightRange = 8;

            for (int y = -sightRange; y < sightRange; y++)
            {
                for (int x = -sightRange; x < sightRange; x++)
                {
                    Point offset = new Point(x, y);
                    if (DistanceHelper.EuclideanDistance(offset) > sightRange) continue;
                    Point visibleCell = position + offset;
                    if (visibleCell.X < 0 || visibleCell.X >= _grid.Size || visibleCell.Y < 0 ||
                        visibleCell.Y >= _grid.Size)
                    {
                        continue;
                    }

                    fraction.FogOfWar[visibleCell.X, visibleCell.Y] = 2;
                }
            }
        }

        [Subscribe]
        public void TurnEndListener(TurnBeginEvent turnBeginEvent)
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    foreach (var fraction in _fractions)
                    {
                        if (fraction.FogOfWar[x, y] > 1) fraction.FogOfWar[x, y] = 1;
                    }
                }
            }
        }

        [Subscribe]
        public void TurnBeginListener(TurnBeginEvent turnBeginEvent)
        {
            var armies = entityWorld.EntityManager.GetEntities(Aspect.All(typeof(Army)));
            foreach (var army in armies)
            {
                Fraction fraction = army.GetComponent<Army>().Fraction;
                Point position = army.GetComponent<GeoEntity>().Position;
                ExpandSeeArea(position, fraction);
            }
        }
    }
}