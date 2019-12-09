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
            var volatileInstructions = programString.Split(",")
                .Select(long.Parse)
                .Select((value, index) => new KeyValuePair<long, long>(index, value));
            var ram = new Dictionary<long, long>(volatileInstructions);
            var computer = new IntcodeComputer();

            var state = new IntcodeState
            {
                Output = new List<string>(),
                Input = new Queue<int>(),
                Memory = ram,
                InstructionPointer = 0
            };
            state.Input.Enqueue(int.Parse(input));
            computer.Compute(state);

            state.Output.First().Should().Be(expected);
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