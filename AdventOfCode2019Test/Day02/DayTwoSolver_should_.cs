using System.IO;
using System.Linq;
using AdventOfCode2019.Day02;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2019Test.Day02
{
    public class DayTwoSolver_should_
    {
        private readonly ITestOutputHelper _outputHelper;

        public DayTwoSolver_should_(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Theory]
        [InlineData("1,9,10,3,2,3,11,0,99,30,40,50", "3500,9,10,70,2,3,11,0,99,30,40,50")]
        [InlineData("1,0,0,0,99", "2,0,0,0,99")]
        [InlineData("2,3,0,3,99", "2,3,0,6,99")]
        [InlineData("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        [InlineData("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
        public void solve_for_expected_answer_part_one(string input, string expected)
        {
            var solver = new DayTwoSolver();

            var actual = solver.PartOneSolve(input);

            actual.Should().Be(expected);
        }

        [Fact]
        public void solve_for_unknown_answer_part_one()
        {
            var inputs = File.ReadAllLines("./Day02/input_part_one.txt");
            var solver = new DayTwoSolver();

            var actual = solver.PartOneSolve(inputs[0]);

            _outputHelper.WriteLine($"{actual}");
        }

        [Theory]
        [InlineData("12", "2")]
        [InlineData("14", "2")]
        [InlineData("1969", "966")]
        [InlineData("100756", "50346")]
        public void solve_for_expected_answer_part_two(string input, string expected)
        {
            var solver = new DayTwoSolver();

            var actual = solver.PartTwoSolve(input);

            actual.Should().Be(expected);
        }

        [Fact]
        public void solve_for_unknown_answer_part_two()
        {
            var inputs = File.ReadAllLines("./Day02/input_part_one.txt");
            var solver = new DayTwoSolver();

            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    var program = inputs[0].Split(",").Select(int.Parse).ToArray();
                    program[1] = i;
                    program[2] = j;
                    var input = program.Select(inputInt => inputInt.ToString()).Aggregate((output, next) => $"{output},{next}");
                    var actual = solver.PartTwoSolve(input);
                    var resultProgram = actual.Split(",").Select(int.Parse).ToArray();
                    if (resultProgram[0] != 19690720)
                    {
                        continue;
                    }

                    _outputHelper.WriteLine($"{i} : {j}");
                    break;
                }
            }
        }
    }
}