using System.IO;
using AdventOfCode2019.Day05;
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
    }
}