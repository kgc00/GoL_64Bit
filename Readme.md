# Conway's Game of Life

Hello :wave:! 

This is Conway's Game of Life with support for 64 bit number space in cell positions and board size. It was written in C# and has decent test coverage for a small project. Some thoughts I had while writing this:
- Since the board is so large I cannot hold all the cells in memory at once
- I'll need to dynamically add them in run-time which means tweaking some normal architectural choices for this project
- Would be nice to clear up old cells as well, since a cell is not relevant if it has no living neighbors. Would allow better support for the very large space
- Wanted to push as much logic into `World` as possible, keep the `Cell` as mostly a data holder

## How To

You can clone the repo to run it locally. Otherwise, you can find a console app [here](https://github.com/kgc00/GoL_64Bit/blob/main/GoL.App/bin/Release.rar) (click download).

Instructions are in the game, but a brief summary will be provided here:
- Start the app
- Enter a list of `(integer, integer)` values, either on the same line or new lines
  - To exit input, submit an empty line (enter 2x) 
  - If you input is not in the expected format it will be dropped
- The app will print the output of the simulation after 10 generations

## Next Steps

- Add threads and workers for parallelizing the `Setup` and `AdvanceGeneration` work
  - The game is deterministic, so we can just batch stuff up
- Add pooling support since we are constantly spawning/recycling cells
- Add a GUI / import to Unity
  - Add fun features like rewind/restart, graphics, sounds, samples, etc
- Better error handling and UX for user input