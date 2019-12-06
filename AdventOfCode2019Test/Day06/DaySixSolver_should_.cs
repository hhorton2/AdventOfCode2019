using System.IO;
using AdventOfCode2019.Day06;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2019Test.Day06
{
    public class DaySixSolver_should_
    {
        private readonly ITestOutputHelper _outputHelper;

        public DaySixSolver_should_(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Theory]
        [InlineData("COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L", "42")]
        public void solve_for_expected_answer_part_one(string input, string expected)
        {
            var solver = new DaySixSolver();

            var actual = solver.PartOneSolve(input);

            actual.Should().Be(expected);
        }

        [Fact]
        public void solve_for_unknown_answer_part_one()
        {
            var inputs = File.ReadAllLines("./Day06/input_part_one.txt");
            var solver = new DaySixSolver();

            var actual = solver.PartOneSolve(string.Join("\n", inputs));

            _outputHelper.WriteLine($"{actual}");
        }
    }
}