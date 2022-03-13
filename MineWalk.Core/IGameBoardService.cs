using System.Collections.Generic;

namespace MineWalk.Core
{
    public interface IGameBoardService
    {
        Location CurrentLocation { get; set; }

        List<Location> MineLocations { get; set; }

        int NumberOfLives { get; set; }

        void Move(Direction direction);
    }
}