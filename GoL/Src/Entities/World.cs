using System.Collections.Generic;
using System.Linq;
using GoL.Models;

namespace GoL.Entities {
    public class World {
        public Dictionary<Point, Cell> PointToCellMap { get; }
        public int CurrentGeneration { get; private set; }
        
        public List<Point> GetLiveCellPoints() =>
            PointToCellMap.Values
                .Where(cell => cell.IsAlive)
                .Select(cell => cell.Point)
                .ToList();

        public World(IEnumerable<Point> pointsToCreateCellsAt) {
            CurrentGeneration = 0;
            PointToCellMap = new Dictionary<Point, Cell>();
            SetupCells(pointsToCreateCellsAt);
        }

        void SetupCells(IEnumerable<Point> pointsToCreateCellsAt) {
            // create initial alive cells
            foreach (var point in pointsToCreateCellsAt) {
                if (PointToCellMap.ContainsKey(point)) {
                    // we did not dedupe this list, so handle that here
                    continue;
                }

                var newCell = new Cell(point, true, this);
                PointToCellMap.Add(point, newCell);
            }

            // generate dead neighbors 0_0 and cache neighbors on each cell
            var aliveCellPoints = new List<Point>(PointToCellMap.Keys);
            foreach (var point in aliveCellPoints) {
                var currentCell = PointToCellMap[point];
                SpawnOrCacheNeighborsForLivingCell(currentCell);
            }
        }
        

        // We do not populate the entire board due to memory concerns, so we need to dynamically
        // add new cells each generation.
        // 
        // To support the rules of GoL cells have different needs based on the cell's isAlive state:
        // - if a cell is alive it needs a full set of neighbors (8) to support underpopulation/exposure
        // - if a cell is dead it only needs to know about it's alive neighbors to support resurrection
        //  - dead cells will have their neighbors list updated by live neighbors each generation
        public void SpawnOrCacheNeighborsForLivingCell(Cell cell) {
            foreach (var direction in Constants.Directions) {
                var neighborPoint = cell.Point + direction;
                if (!PointToCellMap.TryGetValue(neighborPoint, out var neighbor)) {
                    neighbor = new Cell(neighborPoint, false, this);
                    PointToCellMap.Add(neighborPoint, neighbor);
                }
                
                if (!cell.Neighbors.Contains(neighbor)) {
                    cell.Neighbors.Add(neighbor);
                }
                
                // we only run this for live cells, so we need to update the surrounding dead cell's
                // neighbor count here so it can potentially revive
                if (!neighbor.Neighbors.Contains(cell)) {
                    neighbor.Neighbors.Add(cell);
                }
            }
        }

        public void AdvanceGeneration() {
            // todo parallelize work using async workers + threads
            foreach (var currentCell in PointToCellMap.Values) {
                currentCell.SetIsAliveNextGeneration();
            }

            var cellPoints = new List<Point>(PointToCellMap.Keys);
            foreach (var point in cellPoints) {
                var currentCell = PointToCellMap[point];
                currentCell.AdvanceGeneration();
                
                // spawn new cells, if necessary
                if (currentCell.IsAliveAndNeedsNewNeighbors()) {
                    SpawnOrCacheNeighborsForLivingCell(currentCell);
                }
            }

            // because our board is so massive, we can easily hit OOM exceptions.
            // recycling old, stale cells to avoid this.
            // todo use pooling
            cellPoints = new List<Point>(PointToCellMap.Keys);
            foreach (var point in cellPoints) {
                var currentCell = PointToCellMap[point];
                currentCell.DeleteSelfIfUnused();
            }

            CurrentGeneration++;
        }
    }
}