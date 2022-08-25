using System.Collections.Generic;
using GoL.Models;
using System.Linq;

namespace GoL.Entities {
    public class Cell {
        public readonly Point Point;
        public bool IsAlive;
        private bool _isAliveNextGeneration;
        public readonly HashSet<Cell> Neighbors;
        private int LiveNeighborCount => Neighbors.Count(neighbor => neighbor.IsAlive);
        private readonly World _world;

        public Cell(Point point, bool isAlive, World world) {
            Point = point;
            IsAlive = isAlive;
            Neighbors = new HashSet<Cell>();
            _world = world;
        }

        public void SetIsAliveNextGeneration() {
            var liveNeighborCount = LiveNeighborCount;
            _isAliveNextGeneration = IsAlive
                ? liveNeighborCount == 2 || liveNeighborCount == 3
                : liveNeighborCount == 3;
        }

        public void AdvanceGeneration() {
            IsAlive = _isAliveNextGeneration;
            _isAliveNextGeneration = false;
        }

        // cells may not always have all neighbors populated due to memory concerns
        // only alive cells need to have a full list of neighbors to support the rules of GoL
        public bool IsAliveAndNeedsNewNeighbors() {
            if (!IsAlive) return false;
            return Neighbors.Count != Constants.Directions.Count;
        }


        public void DeleteSelfIfUnused() {
            if (IsAlive || LiveNeighborCount != 0) return;

            _world.PointToCellMap.Remove(Point);
            foreach (var neighbor in Neighbors) {
                neighbor.Neighbors.Remove(this);
            }
        }
    }
}