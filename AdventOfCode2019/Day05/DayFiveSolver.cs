using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.Intcode;

namespace AdventOfCode2019.Day05
{
    public class DayFiveSolver : ISolver
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
            state.Input.Enqueue(1);
            computer.Compute(state);

            return string.Join("\n", state.Output);
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
            state.Input.Enqueue(5);
            computer.Compute(state);

            return string.Join("\n", state.Output);
        }

        private static void UpdateProgram(int op, int inputLocOne, int inputLocTwo, int outputLoc, IList<int> program)
        {
            switch (op)
            {
                case 1:
                    program[outputLoc] = program[inputLocOne] + program[inputLocTwo];
                    break;
                case 2:
                    program[outputLoc] = program[inputLocOne] * program[inputLocTwo];
                    break;
                case 3:
                    break;
                default:
                    program[outputLoc] = program[inputLocOne] * program[inputLocTwo];
                    break;
            }
        }
    }
}