using System.IO;
using AdventOfCode2019.Day08;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2019Test.Day08
{
    public class DayEightSolver_should_
    {
        private readonly ITestOutputHelper _outputHelper;

        public DayEightSolver_should_(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
        [Fact]
        public void solve_part_one_for_unknown_input()
        {
            var inputs = File.ReadAllLines("./Day08/input_part_one.txt");
            var solver = new DayEightSolver();

            var actual = solver.PartOneSolve(inputs[0]);

            _outputHelper.WriteLine($"{actual}");
        }
        
        [Fact]
        public void solve_part_two_for_unknown_input()
        {
            var inputs = File.ReadAllLines("./Day08/input_part_one.txt");
            var solver = new DayEightSolver();

            var actual = solver.PartTwoSolve(inputs[0]);

            _outputHelper.WriteLine($"{actual}");
        }
    }
}