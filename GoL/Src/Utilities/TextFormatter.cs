using System.Collections.Generic;
using System.Linq;
using GoL.Models;

namespace GoL.Utilities {
    public static class TextFormatter {
        public static string FormatPointsAsLifeString(IEnumerable<Point> points) {
            return "#Life 1.06\n" + string.Join("\n", points.Select(point => $"{point.X} {point.Y}"));
        }
    }
}