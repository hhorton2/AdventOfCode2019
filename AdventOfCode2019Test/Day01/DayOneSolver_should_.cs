using System.IO;
using System.Linq;
using AdventOfCode2019.Day01;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2019Test.Day01
{
    public class DayOneSolver_should_
    {
        private readonly ITestOutputHelper _outputHelper;

        public DayOneSolver_should_(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Theory]
        [InlineData("12", "2")]
        [InlineData("14", "2")]
        [InlineData("1969", "654")]
        [InlineData("100756", "33583")]
        public void solve_for_expected_answer_part_one(string input, string expected)
        {
            var solver = new DayOneSolver();

            var actual = solver.PartOneSolve(input);

            actual.Should().Be(expected);
        }
        
        [Fact]
        public void solve_for_unknown_answer_part_one()
        {
            var inputs = File.ReadAllLines("./Day01/input_part_one.txt");
            var solver = new DayOneSolver();

            var actual = inputs.Select(input => int.Parse(solver.PartOneSolve(input))).Sum();

            _outputHelper.WriteLine($"{actual}");
        }

        [Theory]
        [InlineData("12", "2")]
        [InlineData("14", "2")]
        [InlineData("1969", "966")]
        [InlineData("100756", "50346")]
        public void solve_for_expected_answer_part_two(string input, string expected)
        {
            var solver = new DayOneSolver();

            var actual = solver.PartTwoSolve(input);

            actual.Should().Be(expected);
        }
        
        [Fact]
        public void solve_for_unknown_answer_part_two()
        {
            var inputs = File.ReadAllLines("./Day01/input_part_one.txt");
            var solver = new DayOneSolver();

            var actual = inputs.Select(input => int.Parse(solver.PartTwoSolve(input))).Sum();

            _outputHelper.WriteLine($"{actual}");
        }
    }
}