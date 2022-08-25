using System.Collections.Generic;
using GoL.Entities;
using GoL.Models;
using NUnit.Framework;
using Shouldly;

namespace GoL.Tests {
    [TestFixture]
    public class SimulationTests {
        private static object[] setupTestCases = {
            new object[] {new List<Point> {new Point(0, 1)}, 9},
            new object[] {new List<Point> {new Point(-2000000000000, -2000000000000)}, 9},
            new object[] {
                new List<Point> {
                    new Point(0, 1),
                    new Point(1, 2),
                    new Point(2, 0),
                    new Point(2, 1),
                    new Point(2, 2),
                },
                // dead cells
                17 +
                // alive cells
                5
            },
            new object[] {
                new List<Point> {
                    new Point(0, 1),
                    new Point(1, 2),
                    new Point(2, 0),
                    new Point(2, 1),
                    new Point(2, 2),
                    new Point(-2000000000000, -2000000000000),
                    new Point(-2000000000001, -2000000000001),
                },
                // cell island one count
                22 +
                // cell island two count
                14
            },
        };

        [TestCaseSource(nameof(setupTestCases))]
        public void CanSetupCells(List<Point> input, int startingCellCount) {
            World world = new World(input);
            world.PointToCellMap.Count.ShouldBe(startingCellCount);
        }

        private static object[] advanceGenerationTestCases = {
            new object[] {
                // setup
                new List<Point> {
                    new Point(0, 1),
                    new Point(1, 2),
                    new Point(2, 0),
                    new Point(2, 1),
                    new Point(2, 2),
                    new Point(-2000000000000, -2000000000000),
                    new Point(-2000000000001, -2000000000001),
                    new Point(-2000000000002, -2000000000000),
                    new Point(-2000000000003, -2000000000002),
                },
                // alive points per generation
                new List<HashSet<Point>> {
                    // gen 1 ...
                    new HashSet<Point> {
                        new Point(1, 0),
                        new Point(1, 2),
                        new Point(2, 1),
                        new Point(2, 2),
                        new Point(3, 1),
                        new Point(-2000000000001, -2000000000000),
                        new Point(-2000000000001, -2000000000001),
                        new Point(-2000000000002, -2000000000001),
                    },
                    new HashSet<Point> {
                        new Point(2, 0),
                        new Point(3, 1),
                        new Point(3, 2),
                        new Point(2, 2),
                        new Point(1, 2),
                        new Point(-2000000000001, -2000000000000),
                        new Point(-2000000000001, -2000000000001),
                        new Point(-2000000000002, -2000000000001),
                        new Point(-2000000000002, -2000000000000),
                    },
                    new HashSet<Point> {
                        new Point(1, 1),
                        new Point(3, 1),
                        new Point(3, 2),
                        new Point(2, 2),
                        new Point(2, 3),
                        new Point(-2000000000001, -2000000000000),
                        new Point(-2000000000001, -2000000000001),
                        new Point(-2000000000002, -2000000000001),
                        new Point(-2000000000002, -2000000000000),
                    },
                    new HashSet<Point> {
                        new Point(1, 2),
                        new Point(3, 1),
                        new Point(3, 2),
                        new Point(3, 3),
                        new Point(2, 3),
                        new Point(-2000000000001, -2000000000000),
                        new Point(-2000000000001, -2000000000001),
                        new Point(-2000000000002, -2000000000001),
                        new Point(-2000000000002, -2000000000000),
                    },
                    new HashSet<Point> {
                        new Point(2, 1),
                        new Point(3, 2),
                        new Point(4, 2),
                        new Point(3, 3),
                        new Point(2, 3),
                        new Point(-2000000000001, -2000000000000),
                        new Point(-2000000000001, -2000000000001),
                        new Point(-2000000000002, -2000000000001),
                        new Point(-2000000000002, -2000000000000),
                    },
                    new HashSet<Point> {
                        new Point(3, 1),
                        new Point(4, 2),
                        new Point(4, 3),
                        new Point(3, 3),
                        new Point(2, 3),
                        new Point(-2000000000001, -2000000000000),
                        new Point(-2000000000001, -2000000000001),
                        new Point(-2000000000002, -2000000000001),
                        new Point(-2000000000002, -2000000000000),
                    },
                    new HashSet<Point> {
                        new Point(2, 2),
                        new Point(3, 3),
                        new Point(3, 4),
                        new Point(4, 2),
                        new Point(4, 3),
                        new Point(-2000000000001, -2000000000000),
                        new Point(-2000000000001, -2000000000001),
                        new Point(-2000000000002, -2000000000001),
                        new Point(-2000000000002, -2000000000000),
                    },
                    new HashSet<Point> {
                        new Point(4, 2),
                        new Point(4, 3),
                        new Point(4, 4),
                        new Point(3, 4),
                        new Point(2, 3),
                        new Point(-2000000000001, -2000000000000),
                        new Point(-2000000000001, -2000000000001),
                        new Point(-2000000000002, -2000000000001),
                        new Point(-2000000000002, -2000000000000),
                    },
                    new HashSet<Point> {
                        new Point(3, 2),
                        new Point(4, 3),
                        new Point(5, 3),
                        new Point(4, 4),
                        new Point(3, 4),
                        new Point(-2000000000001, -2000000000000),
                        new Point(-2000000000001, -2000000000001),
                        new Point(-2000000000002, -2000000000001),
                        new Point(-2000000000002, -2000000000000),
                    },
                    new HashSet<Point> {
                        new Point(4, 2),
                        new Point(5, 3),
                        new Point(5, 4),
                        new Point(4, 4),
                        new Point(3, 4),
                        new Point(-2000000000001, -2000000000000),
                        new Point(-2000000000001, -2000000000001),
                        new Point(-2000000000002, -2000000000001),
                        new Point(-2000000000002, -2000000000000),
                    },
                }
            },
        };

        [TestCaseSource(nameof(advanceGenerationTestCases))]
        public void CanSimulateNextGeneration(List<Point> input, List<HashSet<Point>> cellsAliveEachGeneration) {
            var world = new World(input);
            foreach (var alivePoints in cellsAliveEachGeneration) {
                world.AdvanceGeneration();
                var currentGeneration = world.GetLiveCellPoints();
                alivePoints.SetEquals(currentGeneration).ShouldBeTrue();
            }
        }
    }
}