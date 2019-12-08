using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2019.Day05;
using AdventOfCode2019.Intcode;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2019Test.Day05
{
    public class DayFiveSolver_should_
    {
        private readonly ITestOutputHelper _outputHelper;

        public DayFiveSolver_should_(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void solve_for_unknown_answer_part_one()
        {
            var input = File.ReadAllLines("./Day05/input_part_one.txt")[0];
            var solver = new DayFiveSolver();

            var actual = solver.PartOneSolve(input);

            _outputHelper.WriteLine($"{actual}");
        }

        [Theory]
        [InlineData("7", "999")]
        [InlineData("8", "1000")]
        [InlineData("9", "1001")]
        public void use_new_instructions_correctly(string input, string expected)
        {
            var programString =
                "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";
            var memory = programString.Split(",").Select(int.Parse).ToArray();
            var computer = new IntcodeComputer();
            var inputs = new Queue<int>();
            inputs.Enqueue(int.Parse(input));
            var output = computer.Compute(memory, inputs);

            output.First().Should().Be(expected);
        }

        [Fact]
        public void solve_for_unknown_answer_part_two()
        {
            var input = File.ReadAllLines("./Day05/input_part_one.txt")[0];
            var solver = new DayFiveSolver();

            var actual = solver.PartTwoSolve(input);

            _outputHelper.WriteLine($"{actual}");
        }
    }
}