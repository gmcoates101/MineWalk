using System;

namespace MineWalk.Core
{
    public class Location : IEquatable<Location>
    {
        public int X { get; set; }

        public int Y { get; set; }

        public bool Equals(Location other)
        {
            return this.X == other.X && this.Y == other.Y;
        }
    }
}
