using System;
using AdventOfCode2019Test;

namespace AdventOfCode2019.Day01
{
    public class DayOneSolver : ISolver
    {
        public string PartOneSolve(string input)
        {
            var mass = int.Parse(input);
            var fuel = mass / 3 - 2;
            return fuel.ToString();
        }

        public string PartTwoSolve(string input)
        {
            var mass = int.Parse(input);
            var fuel = 0;
            while (mass > 0)
            {
                var fuelForCurrentMass = Math.Max(0, mass / 3 - 2);
                fuel += fuelForCurrentMass;
                mass = fuelForCurrentMass;
            }
            return fuel.ToString();
        }
    }
}