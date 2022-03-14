using System;
using System.Collections.Generic;

namespace MineWalk.Core
{
    public class GameBoardService : IGameBoardService
    {
        public List<Location> MineLocations { get; set; } = new();

        public Location CurrentLocation { get; set; } = new();

        public int NumberOfLives { get; set; }

        private int numberOfMoves;

        private readonly int gridSize;

        private readonly int numberOfMines;

        private readonly ILogger logger;

        public GameBoardService(ILogger logger, int numberOfLives = 3, int gridSize = 10, int numberOfMines = 10)
        {
            this.NumberOfLives = numberOfLives;
            this.gridSize = gridSize;
            this.numberOfMines = numberOfMines;
            this.logger = logger;

            logger.Log($"Get ready to Walk the Minefield, take care.{Environment.NewLine}");

            InitMines();
        }

        /// <summary>
        /// Setup mine locations.
        /// </summary>
        private void InitMines()
        {
            for (int i = 0; i < numberOfMines; i++)
            {
                AddMine();
            }
        }

        /// <summary>
        /// Add mine using recursion to ensure all mine locations are unique.
        /// </summary>
        private void AddMine()
        {
            var x = new Random().Next(0, gridSize);
            var y = new Random().Next(0, gridSize);

            var mineLocation = new Location() { X = x, Y = y };

            if (MineLocations.Contains(mineLocation))
            {
                AddMine();
            }
            else
            {
                MineLocations.Add(mineLocation);
            }
        }

        /// <summary>
        /// Move the player position, check this can be done and test game state.
        /// </summary>
        /// <param name="direction"></param>
        public void Move(Direction direction)
        {
            var allowMove = CanPlayerMove(direction);

            if (allowMove)
            {
                switch (direction)
                {
                    case Direction.Left:
                        CurrentLocation.X--;
                        break;
                    case Direction.Up:
                        CurrentLocation.Y--;
                        break;
                    case Direction.Right:
                        CurrentLocation.X++;
                        break;
                    case Direction.Down:
                        CurrentLocation.Y++;
                        break;
                }

                numberOfMoves++;

                CollisionDetection();

                CheckGameStatus();
            }
            else
            {
                logger.Log("You are unable to run away, try again.");
            }
        }

        private bool CanPlayerMove(Direction direction)
        {
            var allowMove = false;

            switch (direction)
            {
                case Direction.Left:
                    allowMove = (CurrentLocation.X - 1) > 0;
                    break;

                case Direction.Up:
                    allowMove = (CurrentLocation.Y - 1) > 0;
                    break;

                case Direction.Right:
                    allowMove = (CurrentLocation.X + 1) <= gridSize;
                    break;

                case Direction.Down:
                    allowMove = (CurrentLocation.Y + 1) <= gridSize;
                    break;
            }

            return allowMove;
        }

        private void CollisionDetection()
        {
            if (MineLocations.Contains(CurrentLocation))
            {
                NumberOfLives--;

                MineLocations.Remove(CurrentLocation); // Mine removed, you can't step on it twice!
                logger.Log($"You stepped on a mine, you have {NumberOfLives} lives remaining.");
            }
        }

        private void CheckGameStatus()
        {
            if (ReachedSafety && StillAlive)
            {
                Winner();
            }

            if (!StillAlive)
            {
                GameOver();
            }
        }

        private void Winner()
        {
            var life = NumberOfLives == 1 ? "life" : "lives";
            logger.Log($"{Environment.NewLine}Congratulations, you crossed the minefield in {numberOfMoves} steps, with {NumberOfLives} {life} to spare.");
            Environment.Exit(0);
        }

        private void GameOver()
        {
            logger.Log($"{Environment.NewLine}You ran out of lives, better luck next time.");
            Environment.Exit(0);
        }

        private bool StillAlive => NumberOfLives > 0;

        private bool ReachedSafety => (CurrentLocation.X == gridSize || CurrentLocation.Y == gridSize);
    }
}
