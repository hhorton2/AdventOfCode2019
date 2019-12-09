using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.Intcode;

namespace AdventOfCode2019.Day02
{
    public class DayTwoSolver : ISolver
    {
        private readonly IntcodeProgramParser _parser = new IntcodeProgramParser();

        public string PartOneSolve(string input)
        {
            var computer = new IntcodeComputer();
            var state = new IntcodeState
            {
                Output = new List<string>(),
                Input = new Queue<int>(),
                Memory = _parser.ParseProgram(input),
                InstructionPointer = 0
            };
            computer.Compute(state);
            return state.Memory.Select(d => d.Value).Select(i => i.ToString()).Aggregate((output, next) => $"{output},{next}");
        }

        public string PartTwoSolve(string input)
        {
            var computer = new IntcodeComputer();
            var state = new IntcodeState
            {
                Output = new List<string>(),
                Input = new Queue<int>(),
                Memory = _parser.ParseProgram(input),
                InstructionPointer = 0
            };
            computer.Compute(state);
            return state.Memory.Select(d => d.Value).Select(i => i.ToString()).Aggregate((output, next) => $"{output},{next}");
        }
    }
}