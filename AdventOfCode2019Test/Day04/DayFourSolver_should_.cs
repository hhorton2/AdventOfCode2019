using AdventOfCode2019.Day04;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2019Test.Day04
{
    public class DayFourSolver_should_
    {
        private readonly ITestOutputHelper _outputHelper;

        public DayFourSolver_should_(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
        [Fact]
        public void solve_for_unknown_input_part_one()
        {
            var solver = new DayFourSolver();
            
            _outputHelper.WriteLine(solver.PartOneSolve("271973-785961"));
        }
        [Fact]
        public void solve_for_unknown_input_part_two()
        {
            var solver = new DayFourSolver();
            
            _outputHelper.WriteLine(solver.PartTwoSolve("271973-785961"));
        }
    }
}