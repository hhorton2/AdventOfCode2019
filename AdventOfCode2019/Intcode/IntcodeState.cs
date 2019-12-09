using System.Collections.Generic;

namespace AdventOfCode2019.Intcode
{
    public class IntcodeState
    {
        public long InstructionPointer { get; set; }
        public long RelativeBase { get; set; }
        public Dictionary<long, long> Memory { get; set; }
        public Queue<int> Input { get; set; } = new Queue<int>();
        public IList<string> Output { get; set; } = new List<string>();
        public bool Halted { get; set; }
        public bool BreakOnOutput { get; set; }
    }
}