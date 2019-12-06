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
            var orbits = CreateOrbits(orbitsString);

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
            var orbitsString = input.Split("\n").Select(o => o.Trim());
            var orbits = CreateOrbits(orbitsString);

            var totalOrbits = 0;
            var toVisit = new Queue<OrbitalObject>();
            var laterVisits = new Queue<OrbitalObject>();
            var visited = new HashSet<OrbitalObject>();
            var distance = -2;
            toVisit.Enqueue(orbits["YOU"]);
            visited.Add(orbits["YOU"]);
            while (toVisit.Count > 0)
            {
                var currentObject = toVisit.Dequeue();
                if (currentObject.Name == "SAN")
                {
                    return distance.ToString();
                }
                foreach (var child in currentObject.Children.Values)
                {
                    if (!visited.Contains(child))
                    {
                        laterVisits.Enqueue(child);
                        visited.Add(child);
                    }
                }
                if (currentObject.Parent != null && !visited.Contains(currentObject.Parent))
                {
                    laterVisits.Enqueue(currentObject.Parent);
                    visited.Add(currentObject.Parent);
                }

                if (toVisit.Count == 0)
                {
                    toVisit = laterVisits;
                    laterVisits = new Queue<OrbitalObject>();
                    distance++;
                }
            }

            return "AHHHHHHH NO SANTA";
        }
        
        
        private static Dictionary<string, OrbitalObject> CreateOrbits(IEnumerable<string> orbitsString)
        {
            var orbits = new Dictionary<string, OrbitalObject>();
            foreach (var orbit in orbitsString)
            {
                var orbitalObjects = orbit.Split(")");
                var orbitalParentKey = orbitalObjects[0];
                var orbitalChildKey = orbitalObjects[1];
                if (!orbits.ContainsKey(orbitalParentKey))
                {
                    orbits.Add(orbitalParentKey,
                        new OrbitalObject {Name = orbitalParentKey, Children = new Dictionary<string, OrbitalObject>()});
                }

                if (!orbits.ContainsKey(orbitalChildKey))
                {
                    orbits.Add(orbitalChildKey,
                        new OrbitalObject {Name = orbitalChildKey, Children = new Dictionary<string, OrbitalObject>()});
                }

                orbits[orbitalParentKey].Children.Add(orbitalChildKey, orbits[orbitalChildKey]);
                orbits[orbitalChildKey].Parent = orbits[orbitalParentKey];
            }

            return orbits;
        }
    }
}