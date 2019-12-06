using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Day02
{
    public class DayTwoSolver : ISolver
    {
        public string PartOneSolve(string input)
        {
            var program = input.Split(",").Select(int.Parse).ToArray();
            program[1] = 12;
            program[2] = 2;
            for (var i = 0; i < program.Length; i += 4)
            {
                var op = program[i];
                if (op == 99)
                {
                    break;
                }
                var inputLocOne = program[i + 1];
                var inputLocTwo = program[i + 2];
                var outputLoc = program[i + 3];
                UpdateProgram(op, inputLocOne, inputLocTwo, outputLoc, program);
            }

            return program.Select(i => i.ToString()).Aggregate((output, next) => $"{output},{next}");
        }

        public string PartTwoSolve(string input)
        {
            var program = input.Split(",").Select(int.Parse).ToArray();
            for (var i = 0; i < program.Length; i += 4)
            {
                var op = program[i];
                if (op == 99)
                {
                    break;
                }
                var inputLocOne = program[i + 1];
                var inputLocTwo = program[i + 2];
                var outputLoc = program[i + 3];
                UpdateProgram(op, inputLocOne, inputLocTwo, outputLoc, program);
            }

            return program.Select(i => i.ToString()).Aggregate((output, next) => $"{output},{next}");
        }

        private static void UpdateProgram(int op, int inputLocOne, int inputLocTwo, int outputLoc, IList<int> program)
        {
            if (op == 1)
            {
                program[outputLoc] = program[inputLocOne] + program[inputLocTwo];
            }
            else
            {
                program[outputLoc] = program[inputLocOne] * program[inputLocTwo];
            }
        }
    }
}