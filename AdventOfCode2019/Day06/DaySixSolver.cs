using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Day06
{
    public class DaySixSolver : ISolver
    {
        public string PartOneSolve(string input)
        {
            var orbitsString = input.Split("\n").Select(o => o.Trim());
            var orbits = new Dictionary<string, OrbitalObject>();
            foreach (var orbit in orbitsString)
            {
                var orbitalObjects = orbit.Split(")");
                var orbitalParentKey = orbitalObjects[0];
                var orbitalChildKey = orbitalObjects[1];
                if (!orbits.ContainsKey(orbitalParentKey))
                {
                    orbits.Add(orbitalParentKey, new OrbitalObject {Name = orbitalParentKey, Children = new Dictionary<string, OrbitalObject>()});
                }

                if (!orbits.ContainsKey(orbitalChildKey))
                {
                    orbits.Add(orbitalChildKey, new OrbitalObject {Name = orbitalChildKey, Children = new Dictionary<string, OrbitalObject>()});
                }

                orbits[orbitalParentKey].Children.Add(orbitalChildKey, orbits[orbitalChildKey]);
                orbits[orbitalChildKey].Parent = orbits[orbitalParentKey];
            }

            var totalOrbits = 0;
            var toVisit = new Queue<OrbitalObject>();
            var visited = new HashSet<OrbitalObject>();
            toVisit.Enqueue(orbits["COM"]);
            visited.Add(orbits["COM"]);
            while (toVisit.Count > 0)
            {
                var currentObject = toVisit.Dequeue();
                totalOrbits += currentObject.Children.Count * currentObject.GetDepth();
                foreach (var child in currentObject.Children.Values)
                {
                    if (!visited.Contains(child))
                    {
                        toVisit.Enqueue(child);
                        visited.Add(child);
                    }
                }
            }

            return totalOrbits.ToString();
        }

        public string PartTwoSolve(string input)
        {
            throw new NotImplementedException();
        }
    }
}