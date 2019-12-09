using System.IO;
using AdventOfCode2019.Day07;
using AdventOfCode2019.Day09;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2019Test.Day09
{
    public class DayNineSolver_should_
    {
        private readonly ITestOutputHelper _outputHelper;

        public DayNineSolver_should_(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Theory]
        [InlineData("109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99",
            "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99")]
        [InlineData("1102,34915192,34915192,7,4,7,99,0", "1219070632396864")]
        [InlineData("104,1125899906842624,99", "1125899906842624")]
        public void solve_part_one_for_known_values(string program, string expected)
        {
            var solver = new DayNineSolver();

            var output = solver.PartOneSolve(program);

            output.Should().Be(expected);
        }

        [Fact]
        public void solve_for_unknown_value_part_one()
        {
            var inputs = File.ReadAllLines("./Day09/input_part_one.txt");
            var solver = new DayNineSolver();

            var actual = solver.PartOneSolve(inputs[0], 1);

            _outputHelper.WriteLine($"{actual}");
        }
        
        [Fact]
        public void solve_for_unknown_value_part_two()
        {
            var inputs = File.ReadAllLines("./Day09/input_part_one.txt");
            var solver = new DayNineSolver();

            var actual = solver.PartOneSolve(inputs[0], 2);

            _outputHelper.WriteLine($"{actual}");
        }
    }
}