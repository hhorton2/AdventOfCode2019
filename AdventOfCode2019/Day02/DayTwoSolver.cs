using System.Linq;
using AdventOfCode2019.Intcode;

namespace AdventOfCode2019.Day02
{
    public class DayTwoSolver : ISolver
    {
        public string PartOneSolve(string input)
        {
            var program = input.Split(",").Select(int.Parse).ToArray();
            var computer = new IntcodeComputer();
            computer.Compute(program);
            return program.Select(i => i.ToString()).Aggregate((output, next) => $"{output},{next}");
        }

        public string PartTwoSolve(string input)
        {
            var program = input.Split(",").Select(int.Parse).ToArray();
            var computer = new IntcodeComputer();
            computer.Compute(program);
            return program.Select(i => i.ToString()).Aggregate((output, next) => $"{output},{next}");
        }
    }
}