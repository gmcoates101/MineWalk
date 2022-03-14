using System;

namespace MineWalk.Core
{
    /// <summary>
    /// Game class including Game loop.
    /// </summary>
    public class Game
    {
        private readonly GameBoardService gameBoardService;

        private readonly ILogger logger;

        public Game(ILogger logger, GameBoardService gameBoardService)
        {
            this.logger = logger;
            this.gameBoardService = gameBoardService;
        }

        /// <summary>
        /// Run the game until complete
        /// </summary>
        public void Run()
        {
            Help();

            while(true)
            {
                Console.Write("> ");
                var command = Console.ReadKey(true).Key;

                switch (command)
                {
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        logger.Log("Moving Left");
                        gameBoardService.Move(Direction.Left);
                        break;

                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        logger.Log("Moving Up");
                        gameBoardService.Move(Direction.Up);
                        break;

                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        logger.Log("Moving Right");
                        gameBoardService.Move(Direction.Right);
                        break;

                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        logger.Log("Moving Down");
                        gameBoardService.Move(Direction.Down);
                        break;

                    case ConsoleKey.Q:
                        logger.Log("Quitting");
                        Environment.Exit(0);
                        break;

                    default:
                        logger.Log("Unknown entry please try again.");
                        Help();
                        break;
                }
            }
        }

        private void Help() =>
            logger.Log($"Use Arrow keys or A = left, W = Up, D = right, S = down, Q to quit.{Environment.NewLine}");
    }
}
