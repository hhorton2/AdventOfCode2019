using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Day08
{
    public class SpaceImageFormatDecoder
    {
        public IEnumerable<SpaceImageLayer> Decode(string input, int width, int height)
        {
            var rows = Enumerable.Range(0, input.Length / width)
                .Select(i => input.Substring(i * width, width).ToCharArray().Select(s => int.Parse(s.ToString())))
                .ToArray();
            var layers = new List<SpaceImageLayer>();
            for (var i = 0; i < rows.Length; i += height)
            {
                var rowsToAdd = new List<IEnumerable<int>>();
                for (var j = i; j < i + height; j++)
                {
                    rowsToAdd.Add(rows[j]);
                }
                layers.Add(new SpaceImageLayer{Rows = rowsToAdd});
            }

            return layers;
        }
    }
}