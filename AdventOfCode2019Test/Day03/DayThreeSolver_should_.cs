using System.IO;
using System.Linq;
using AdventOfCode2019.Day03;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2019Test.Day03
{
    public class DayThreeSolver_should_
    {
        private readonly ITestOutputHelper _outputHelper;

        public DayThreeSolver_should_(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Theory]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", "159")]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", "135")]
        public void solve_for_expected_answer_part_one(string wireOne, string wireTwo, string expected)
        {
            var solver = new DayThreeSolver();

            var actual = solver.PartOneSolve($"{wireOne}\n{wireTwo}");

            actual.Should().Be(expected);
        }

        [Fact]
        public void solve_for_unknown_answer_part_one()
        {
            var inputs = File.ReadAllLines("./Day03/input_part_one.txt");
            var solver = new DayThreeSolver();

            var actual = solver.PartOneSolve($"{inputs[0]}\n{inputs[1]}");

            _outputHelper.WriteLine($"{actual}");
        }

        [Theory]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", "159")]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", "135")]
        public void solve_for_expected_answer_part_two(string wireOne, string wireTwo, string expected)
        {
            var solver = new DayThreeSolver();

            var actual = solver.PartTwoSolve($"{wireOne}\n{wireTwo}");

            actual.Should().Be(expected);
        }

        [Fact]
        public void solve_for_unknown_answer_part_two()
        {
            var inputs = File.ReadAllLines("./Day03/input_part_one.txt");
            var solver = new DayThreeSolver();

            var actual = solver.PartTwoSolve(inputs[0]);

            _outputHelper.WriteLine($"{actual}");
        }
    }
}