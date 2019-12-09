using System;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Day08
{
    public class DayEightSolver : ISolver
    {
        public string PartOneSolve(string input)
        {
            var decoder = new SpaceImageFormatDecoder();
            var layers = decoder.Decode(input, 25, 6).ToList();
            var minLayer = layers.First();
            var minCount = int.MaxValue;
            foreach (var layer in layers)
            {
                var currentLayerCount = layer.Rows.SelectMany(r => r).Count(r => r == 0);
                if (currentLayerCount < minCount)
                {
                    minLayer = layer;
                    minCount = currentLayerCount;
                }
            }

            var oneCount = minLayer.Rows.SelectMany(r => r).Count(r => r == 1);
            var twoCount = minLayer.Rows.SelectMany(r => r).Count(r => r == 2);
            return (oneCount * twoCount).ToString();
        }

        public string PartTwoSolve(string input)
        {
            var decoder = new SpaceImageFormatDecoder();
            var layers = decoder.Decode(input, 25, 6).Reverse().ToList();
            var outputArray = new string[6, 25];
            foreach (var layer in layers)
            {
                for (var i = 0; i < layer.Rows.Count(); i++)
                {
                    var row = layer.Rows.ElementAt(i).ToArray();
                    for (var j = 0; j < row.Count(); j++)
                    {
                        if (row[j] != 2)
                        {
                            outputArray[i, j] = row[j] == 0 ? " " : "█";
                        }
                    }
                }
            }

            var sb = new StringBuilder();
            for (var i = 0; i < outputArray.GetLength(0); i++)
            {
                for (var j = 0; j < outputArray.GetLength(1); j++)
                {
                    sb.Append(outputArray[i, j]);
                }

                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}