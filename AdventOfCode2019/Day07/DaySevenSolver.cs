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

                                var state = GetFreshState(rom);
                                state.Input.Enqueue(a);
                                state.Input.Enqueue(0);
                                computer.Compute(state);
                                var output = state.Output.First();
                                
                                state = GetFreshState(rom);
                                state.Input.Enqueue(b);
                                state.Input.Enqueue(int.Parse(output));
                                computer.Compute(state);
                                output = state.Output.First();
                                
                                state = GetFreshState(rom);
                                state.Input.Enqueue(c);
                                state.Input.Enqueue(int.Parse(output));
                                computer.Compute(state);
                                output = state.Output.First();
                                
                                state = GetFreshState(rom);
                                state.Input.Enqueue(d);
                                state.Input.Enqueue(int.Parse(output));
                                computer.Compute(state);
                                output = state.Output.First();
                                
                                state = GetFreshState(rom);
                                state.Input.Enqueue(e);
                                state.Input.Enqueue(int.Parse(output));
                                computer.Compute(state);
                                
                                var currentThrust = int.Parse(state.Output.First());
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

        private static IntcodeState GetFreshState(ReadOnlyCollection<int> rom)
        {
            var state = new IntcodeState
            {
                Output = new List<string>(),
                Input = new Queue<int>(),
                Memory = rom.ToArray(),
                InstructionPointer = 0,
                BreakOnOutput = false,
                Halted = false
            };
            return state;
        }

        public string PartTwoSolve(string input)
        {
            var rom = new ReadOnlyCollection<int>(input.Split(",").Select(int.Parse).ToArray());
            var computer = new IntcodeComputer();
            var maxThrust = 0;
            for (var a = 5; a < 10; a++)
            {
                for (var b = 5; b < 10; b++)
                {
                    for (var c = 5; c < 10; c++)
                    {
                        for (var d = 5; d < 10; d++)
                        {
                            for (var e = 5; e < 10; e++)
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
                                var programStates = new List<IntcodeState>
                                {
                                    new IntcodeState
                                    {
                                        Input = new Queue<int>(),
                                        Memory = rom.ToArray(),
                                        Output = new List<string>(),
                                        InstructionPointer = 0,
                                        BreakOnOutput = true
                                    },
                                    new IntcodeState
                                    {
                                        Input = new Queue<int>(),
                                        Memory = rom.ToArray(),
                                        Output = new List<string>(),
                                        InstructionPointer = 0,
                                        BreakOnOutput = true
                                    },
                                    new IntcodeState
                                    {
                                        Input = new Queue<int>(),
                                        Memory = rom.ToArray(),
                                        Output = new List<string>(),
                                        InstructionPointer = 0,
                                        BreakOnOutput = true
                                    },
                                    new IntcodeState
                                    {
                                        Input = new Queue<int>(),
                                        Memory = rom.ToArray(),
                                        Output = new List<string>(),
                                        InstructionPointer = 0,
                                        BreakOnOutput = true
                                    },
                                    new IntcodeState
                                    {
                                        Input = new Queue<int>(),
                                        Memory = rom.ToArray(),
                                        Output = new List<string>(),
                                        InstructionPointer = 0,
                                        BreakOnOutput = true
                                    }
                                };
                                programStates[0].Input.Enqueue(a);
                                programStates[0].Input.Enqueue(0);
                                programStates[1].Input.Enqueue(b);
                                programStates[2].Input.Enqueue(c);
                                programStates[3].Input.Enqueue(d);
                                programStates[4].Input.Enqueue(e);
                                while (programStates.Any(s => !s.Halted))
                                {
                                    for (var i = 0; i < programStates.Count; i++)
                                    {
                                        computer.Compute(programStates[i]);
                                        if (i == programStates.Count - 1)
                                        {
                                            programStates[0].Input.Enqueue(int.Parse(programStates[i].Output.Last()));
                                        }
                                        else
                                        {
                                            programStates[i + 1].Input
                                                .Enqueue(int.Parse(programStates[i].Output.Last()));
                                        }
                                    }
                                }

                                var currentThrust = int.Parse(programStates.Last().Output.Last());
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
    }
}