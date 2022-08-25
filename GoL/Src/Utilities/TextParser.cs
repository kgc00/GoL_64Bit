using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GoL.Models;

namespace GoL.Utilities {
    public static class TextParser {
        // match on (integer, integer)
        static readonly Regex MatchPointsInString = new Regex(@"(-?[0-9]+,(\s*)-?[0-9]+)", RegexOptions.Multiline);
        // match on integer
        static readonly Regex MatchNumbersInString = new Regex("(-?[0-9]+)", RegexOptions.Multiline);

        public static IEnumerable<Point> ParseStringAsPoints(string input) {
            // todo better error handling
            var pointsAsString = MatchPointsInString.Matches(input)
                .OfType<Match>()
                .Select(match => match.Groups[0].Value)
                .ToList();

            var inputAsPoints = new List<Point>();
            foreach (var pointAsString in pointsAsString) {
                var numbersInString = MatchNumbersInString.Matches(pointAsString)
                    .OfType<Match>()
                    .Select(match => long.Parse(match.Value)).ToList();
                
                if (numbersInString.Count != 2) {
                    continue;
                }
                
                inputAsPoints.Add(new Point(numbersInString[0], numbersInString[1]));
            }
            
            return inputAsPoints;
        }
    }
}