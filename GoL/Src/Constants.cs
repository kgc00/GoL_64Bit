using System.Collections.Generic;
using GoL.Models;

namespace GoL {
    public static class Constants {
        public static readonly List<Point> Directions = new List<Point> {
            new Point(0, 1),
            new Point(-1, 1),
            new Point(-1, 0),
            new Point(-1, -1),
            new Point(0, -1),
            new Point(1, -1),
            new Point(1, 0),
            new Point(1, 1)
        };
    }
}