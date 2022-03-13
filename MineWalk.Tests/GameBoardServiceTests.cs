using System.Linq;
using MineWalk.Core;
using Moq;
using Xunit;

namespace MineWalk.Tests
{
    public class GameBoardServiceTests
    {
        IGameBoardService gameBoardService;

        public GameBoardServiceTests()
        {
            var mockLogger = Mock.Of<ILogger>();
            gameBoardService = new GameBoardService(mockLogger);
        }

        [Theory]
        [InlineData(Direction.Left)]
        [InlineData(Direction.Up)]
        public void PlayerPostionOnStartBoundary_No_Move_Left_Up(Direction direction)
        {
            var expectedLocation = new Location() { X = 0, Y = 0 };

            gameBoardService.Move(direction);
            var currentLocation = gameBoardService.CurrentLocation;

            Assert.Equal(expectedLocation.ToString(), currentLocation.ToString());
        }

        [Theory]
        [InlineData(Direction.Right)]
        [InlineData(Direction.Down)]
        public void PlayerPostionOnEndBoundary_No_Move_Right_Down(Direction direction)
        {
            var expectedLocation = new Location() { X = 10, Y = 10 };
            gameBoardService.CurrentLocation = new Location { X = 10, Y = 10 };

            gameBoardService.Move(direction);
            var currentLocation = gameBoardService.CurrentLocation;

            Assert.Equal(expectedLocation.ToString(), currentLocation.ToString());
        }

        [Theory]
        [InlineData(Direction.Left, 4, 5)]
        [InlineData(Direction.Up, 5, 4)]
        [InlineData(Direction.Right, 6, 5)]
        [InlineData(Direction.Down, 5, 6)]
        public void PlayerCanMove_IncreaseByOne(Direction direction, int expectedX, int expectedY)
        {
            var expectedLocation = new Location() { X = expectedX, Y = expectedY };
            gameBoardService.CurrentLocation = new Location { X = 5, Y = 5 };

            gameBoardService.Move(direction);
            var currentLocation = gameBoardService.CurrentLocation;

            Assert.Equal(expectedLocation.ToString(), currentLocation.ToString());
        }

        [Fact]
        public void PlayerStepsOn_Mine_LivesDecrease()
        {
            var mine = gameBoardService.MineLocations.First();

            gameBoardService.CurrentLocation = new Location { X = mine.X - 1, Y = mine.Y };
            gameBoardService.Move(Direction.Right);

            Assert.Equal(2, gameBoardService.NumberOfLives);
        }

        [Fact]
        public void PlayerStepsOn_Mine_MinesDecrease()
        {
            var mine = gameBoardService.MineLocations.First();

            gameBoardService.CurrentLocation = new Location { X = mine.X - 1, Y = mine.Y };
            gameBoardService.Move(Direction.Right);

            Assert.Equal(9, gameBoardService.MineLocations.Count);
        }
    }
}
