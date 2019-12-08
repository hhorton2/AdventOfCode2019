using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AdventOfCode2019.Intcode;

namespace AdventOfCode2019.Day07
{
    public class DaySevenSolver : ISolver
    {
        public string PartOneSolve(string input)
        {
            var rom = new ReadOnlyCollection<int>(input.Split(",").Select(int.Parse).ToArray());
            var computer = new IntcodeComputer();
            var inputs = new Queue<int>();
            var maxThrust = 0;
            for (var a = 0; a < 5; a++)
            {
                for (var b = 0; b < 5; b++)
                {
                    for (var c = 0; c < 5; c++)
                    {
                        for (var d = 0; d < 5; d++)
                        {
                            for (var e = 0; e < 5; e++)
                            {
                                var nums = new[]
                                {
                                    a, b, c, d, e
                                };
                                var numSet = new HashSet<int>
                                {
                                    a, b, c, d, e
                                };
                                if (nums.Length > numSet.Count)
                                {
                                    continue;
                                }

                                inputs.Enqueue(a);
                                inputs.Enqueue(0);
                                var aOutput = computer.Compute(rom.ToArray(), inputs);
                                inputs.Enqueue(b);
                                inputs.Enqueue(int.Parse(aOutput.First()));
                                var bOutput = computer.Compute(rom.ToArray(), inputs);
                                inputs.Enqueue(c);
                                inputs.Enqueue(int.Parse(bOutput.First()));
                                var cOutput = computer.Compute(rom.ToArray(), inputs);
                                inputs.Enqueue(d);
                                inputs.Enqueue(int.Parse(cOutput.First()));
                                var dOutput = computer.Compute(rom.ToArray(), inputs);
                                inputs.Enqueue(e);
                                inputs.Enqueue(int.Parse(dOutput.First()));
                                var eOutput = computer.Compute(rom.ToArray(), inputs);
                                var currentThrust = int.Parse(eOutput.First());
                                if (currentThrust > maxThrust)
                                {
                                    maxThrust = currentThrust;
                                }
                            }
                        }
                    }
                }
            }

            return maxThrust.ToString();
        }

        public string PartTwoSolve(string input)
        {
            throw new NotImplementedException();
        }
    }
}