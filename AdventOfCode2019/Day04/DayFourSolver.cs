namespace AdventOfCode2019.Day04
{
    public class DayFourSolver : ISolver
    {
        public string PartOneSolve(string input)
        {
            var rangeNums = input.Split("-");
            var min = int.Parse(rangeNums[0]);
            var max = int.Parse(rangeNums[1]);
            var passwordMatchCount = 0;
            for (var i = min; i <= max; i++)
            {
                var currentNum = i.ToString().ToCharArray();
                var hasMatch = false;
                var allNumbersGreater = true;
                for (var j = 1; j < currentNum.Length; j++)
                {
                    if (int.Parse(currentNum[j].ToString()) < int.Parse(currentNum[j - 1].ToString()))
                    {
                        allNumbersGreater = false;
                        break;
                    }

                    if (currentNum[j] == currentNum[j - 1])
                    {
                        hasMatch = true;
                    }
                }

                if (hasMatch && allNumbersGreater)
                {
                    passwordMatchCount++;
                }
            }

            return $"{passwordMatchCount}";
        }

        public string PartTwoSolve(string input)
        {
            var rangeNums = input.Split("-");
            var min = int.Parse(rangeNums[0]);
            var max = int.Parse(rangeNums[1]);
            var passwordMatchCount = 0;
            for (var i = min; i <= max; i++)
            {
                var currentNum = i.ToString().ToCharArray();
                var hasMatch = false;
                var allNumbersGreater = true;
                var matchCount = 0;
                for (var j = 1; j < currentNum.Length; j++)
                {
                    if (int.Parse(currentNum[j].ToString()) < int.Parse(currentNum[j - 1].ToString()))
                    {
                        allNumbersGreater = false;
                        break;
                    }

                    if (currentNum[j] == currentNum[j - 1])
                    {
                        matchCount++;
                        if (j == currentNum.Length - 1)
                        {
                            matchCount = CheckMatchCountAndReset(matchCount, ref hasMatch);
                        }
                    }
                    else
                    {
                        matchCount = CheckMatchCountAndReset(matchCount, ref hasMatch);
                    }
                }

                if (hasMatch && allNumbersGreater)
                {
                    passwordMatchCount++;
                }
            }

            return $"{passwordMatchCount}";
        }

        private static int CheckMatchCountAndReset(int matchCount, ref bool hasMatch)
        {
            if (matchCount == 1)
            {
                hasMatch = true;
            }

            return 0;
        }
    }
}