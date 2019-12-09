using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.Intcode;

namespace AdventOfCode2019.Day05
{
    public class DayFiveSolver : ISolver
    {
        private readonly IntcodeProgramParser _parser = new IntcodeProgramParser();

        public string PartOneSolve(string input)
        {
            var ram = _parser.ParseProgram(input);
            var computer = new IntcodeComputer();
            var state = new IntcodeState
            {
                Output = new List<string>(),
                Input = new Queue<int>(),
                Memory = ram,
                InstructionPointer = 0
            };
            state.Input.Enqueue(1);
            computer.Compute(state);

            return string.Join("\n", state.Output);
        }

        public string PartTwoSolve(string input)
        {
            var ram = _parser.ParseProgram(input);
            var computer = new IntcodeComputer();
            var state = new IntcodeState
            {
                Output = new List<string>(),
                Input = new Queue<int>(),
                Memory = ram,
                InstructionPointer = 0
            };
            state.Input.Enqueue(5);
            computer.Compute(state);

            return string.Join("\n", state.Output);
        }
    }
}