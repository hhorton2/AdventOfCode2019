using System;

namespace AdventOfCode2019.Day03
{
    public struct Coord : IComparable<Coord>, IEquatable<Coord>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(Coord other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is Coord other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public int CompareTo(Coord other)
        {
            var xComparison = X.CompareTo(other.X);
            return xComparison != 0 ? xComparison : Y.CompareTo(other.Y);
        }
    }
}