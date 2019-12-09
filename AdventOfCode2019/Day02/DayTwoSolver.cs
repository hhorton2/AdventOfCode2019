using System.Collections.Generic;
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
            var state = new IntcodeState
            {
                Output = new List<string>(),
                Input = new Queue<int>(),
                Memory = program,
                InstructionPointer = 0
            };
            computer.Compute(state);
            return state.Memory.Select(i => i.ToString()).Aggregate((output, next) => $"{output},{next}");
        }

        public string PartTwoSolve(string input)
        {
            var program = input.Split(",").Select(int.Parse).ToArray();
            var computer = new IntcodeComputer();
            var state = new IntcodeState
            {
                Output = new List<string>(),
                Input = new Queue<int>(),
                Memory = program,
                InstructionPointer = 0
            };
            computer.Compute(state);
            return state.Memory.Select(i => i.ToString()).Aggregate((output, next) => $"{output},{next}");
        }
    }
}