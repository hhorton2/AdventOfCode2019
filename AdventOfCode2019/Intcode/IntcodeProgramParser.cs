using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Intcode
{
    public class IntcodeProgramParser
    {
        public Dictionary<long, long> ParseProgram(string input)
        {
            var volatileInstructions = input.Split(",")
                .Select(long.Parse)
                .Select((value, index) => new KeyValuePair<long, long>(index, value));
            return new Dictionary<long, long>(volatileInstructions);
        }
    }
}