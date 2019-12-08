using System.IO;
using AdventOfCode2019.Day07;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2019Test.Day07
{
    public class DaySevenSolver_should_
    {
        private readonly ITestOutputHelper _outputHelper;

        public DaySevenSolver_should_(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
        [Theory]
        [InlineData("3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", "43210")]
        [InlineData("3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0", "54321")]
        [InlineData(
            "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0",
            "65210")]
        public void solve_part_one_for_known_values(string program, string expected)
        {
            var solver = new DaySevenSolver();

            var thrust = solver.PartOneSolve(program);

            thrust.Should().Be(expected);
        }

        [Fact]
        public void solve_for_unknown_value()
        {
            var inputs = File.ReadAllLines("./Day07/input_part_one.txt");
            var solver = new DaySevenSolver();

            var actual = solver.PartOneSolve(inputs[0]);

            _outputHelper.WriteLine($"{actual}");
        }
    }
}