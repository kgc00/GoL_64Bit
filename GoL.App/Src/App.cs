using System;
using GoL.Entities;
using GoL.Utilities;

namespace GoL_App {
    internal static class App {
        private static int GenerationsToSimulate = 10;

        public static void Main(string[] args) {
            Console.WriteLine("You are playing Conway's Game Of Life!");
            Console.WriteLine("The program will print the output of the simulation after 10 generations.");
            Console.WriteLine();

            var userInput = GetUserInput();

            Console.WriteLine("Running simulation...");
            Console.WriteLine();

            var simulationInput = TextParser.ParseStringAsPoints(userInput);
            var world = new World(simulationInput);
            for (int i = 0; i < GenerationsToSimulate; i++) {
                world.AdvanceGeneration();
            }

            var liveCellPoints = world.GetLiveCellPoints();
            if (liveCellPoints.Count == 0) {
                Console.WriteLine($"No cells survived after {GenerationsToSimulate} generations.");
            }
            else {
                Console.WriteLine($"Live cells positions after {GenerationsToSimulate} generations:");
            }

            Console.WriteLine(TextFormatter.FormatPointsAsLifeString(liveCellPoints));
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static string GetUserInput() {
            while (true) {
                Console.WriteLine(
                    "Paste or type your input into the console. Input must be a list of (x, y) integer values, either on the same line or new lines.");
                Console.WriteLine();
                Console.WriteLine(
                    "When the system detects an empty line it will stop accepting input and run the simulation (so you will need to hit enter 2x to exit the input loop).");
                Console.WriteLine();

                var input = "";
                string line;
                while ((line = Console.ReadLine()) != null && line != "") {
                    input += string.IsNullOrEmpty(input) ? line : $"\n{line}";
                }

                Console.WriteLine("Your input was:");
                Console.WriteLine(input);
                Console.WriteLine();
                Console.WriteLine("Is this correct? (y/n)");
                var userConfirmation = Console.ReadKey();
                Console.WriteLine();
                Console.WriteLine();

                if (userConfirmation.Key != ConsoleKey.Y) continue;
                return input;
            }
        }
    }
}