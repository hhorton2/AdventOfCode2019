using System;
using System.Collections.Generic;

namespace AdventOfCode2019.Day06
{
    public class OrbitalObject : IEquatable<OrbitalObject>
    {
        public string Name { get; set; }
        public OrbitalObject Parent { get; set; }
        public Dictionary<string, OrbitalObject> Children { get; set; }

        public int GetDepth()
        {
            var depth = 0;
            var current = this;
            while (current != null)
            {
                current = current.Parent;
                depth++;
            }

            return depth;
        }

        public bool Equals(OrbitalObject other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Equals(Parent, other.Parent) && Equals(Children, other.Children);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OrbitalObject) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Parent != null ? Parent.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Children != null ? Children.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}