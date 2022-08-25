using System.Collections.Generic;
using GoL.Models;
using GoL.Utilities;
using NUnit.Framework;
using Shouldly;

namespace GoL.Tests {
    [TestFixture]
    public class ParsingTests {
        public static object[] InputSamples = {
            new object[] {
                @"(0, 1)
(1, 2)
(2, 0)
(2, 1)
(2, 2)
(-2000000000000, -2000000000000)
(-2000000000001, -2000000000001)",
                new HashSet<Point> {
                    new Point(0, 1),
                    new Point(1, 2),
                    new Point(2, 0),
                    new Point(2, 1),
                    new Point(2, 2),
                    new Point(-2000000000000, -2000000000000),
                    new Point(-2000000000001, -2000000000001),
                }
            },
            new object[] {
                @"(0, 1)    (1, 2)
(2, 0)(2,   1)
(2,2) (2,2)
(-2000000000000, -2000000000000)
(-2000000000001, -2000000000001)",
                new HashSet<Point> {
                    new Point(0, 1),
                    new Point(1, 2),
                    new Point(2, 0),
                    new Point(2, 1),
                    new Point(2, 2),
                    new Point(-2000000000000, -2000000000000),
                    new Point(-2000000000001, -2000000000001),
                }
            }
        };

        [TestCaseSource(nameof(InputSamples))]
        public void CanParseTextAsPointsTest(string input, HashSet<Point> expectedOutput) {
            var result = TextParser.ParseStringAsPoints(input);
            expectedOutput.SetEquals(result).ShouldBeTrue();
        }
    }
}