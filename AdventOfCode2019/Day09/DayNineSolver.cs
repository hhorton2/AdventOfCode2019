using System;
using System.Linq;
using AdventOfCode2019.Intcode;

namespace AdventOfCode2019.Day09
{
    public class DayNineSolver : ISolver
    {
        private readonly IntcodeProgramParser _parser = new IntcodeProgramParser();


        public string PartOneSolve(string input)
        {
            return PartOneSolve(input, null);
        }

        public string PartOneSolve(string input, int? programInput)
        {
            var ram = _parser.ParseProgram(input);
            var state = new IntcodeState
            {
                Memory = ram
            };
            if (programInput != null)
            {
                state.Input.Enqueue(programInput.Value);
            }

            var computer = new IntcodeComputer();
            computer.Compute(state);

            return state.Output.Aggregate((current, next) => $"{current},{next}");
        }

        public string PartTwoSolve(string input)
        {
            throw new NotImplementedException();
        }
    }
}