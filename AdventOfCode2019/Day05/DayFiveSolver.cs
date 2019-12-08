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
            var inputs = new Queue<int>();
            inputs.Enqueue(1);
            var output = computer.Compute(program, inputs);

            return string.Join("\n", output);
        }

        public string PartTwoSolve(string input)
        {
            var program = input.Split(",").Select(int.Parse).ToArray();
            var computer = new IntcodeComputer();
            var inputs = new Queue<int>();
            inputs.Enqueue(5);
            var output = computer.Compute(program, inputs);

            return string.Join("\n", output);
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