using System.Collections.Generic;

namespace AdventOfCode2019.Intcode
{
    public class IntcodeState
    {
        public int InstructionPointer { get; set; }
        public int[] Memory { get; set; }
        public Queue<int> Input { get; set; }
        public IList<string> Output { get; set; }
        public bool Halted { get; set; }
        public bool BreakOnOutput { get; set; }
    }
}