using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Day01
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            DoWork();
        }

        [GeneratedRegex(@"(-?\d+)\s+(-?\d+)")]
        private static partial Regex regex();

        public static Int64 DoWork(bool testData = true, bool frequencyCheck = true)
        {
            string[] input;
           
            if(testData)
                input = System.IO.File.ReadAllLines("Day01TestInput1.txt");
            else
                input = System.IO.File.ReadAllLines("Day01FullInput1.txt");

            List<int> leftList = [];
            List<int> rightList = [];

            Dictionary<int, int> rightListFrequency = [];

            foreach (var line in input)
            {
                var match = regex().Match(line);
                if (!match.Success)
                {
                    System.Console.WriteLine($"Failed to match line: {line}");
                    continue;
                }
                var x = int.Parse(match.Groups[1].Value);
                var y = int.Parse(match.Groups[2].Value);
                leftList.Add(x);
                if (!frequencyCheck)
                {
                    rightList.Add(y);
                }
                else
                {
                    if (rightListFrequency.ContainsKey(y))
                    {
                        rightListFrequency[y]++;
                    }
                    else
                    {
                        rightListFrequency[y] = 1;
                    }
                }
            }

            leftList.Sort();

            if(!frequencyCheck)
                rightList.Sort();

            Int64 result = 0;

            for (int i = 0; i < leftList.Count; i++)
            {
                if (frequencyCheck)
                {
                    if (rightListFrequency.TryGetValue(leftList[i], out var frequency))
                    {
                        result += leftList[i] * frequency;
                    }
                }
                else
                {
                    result += Math.Abs(leftList[i] - rightList[i]);
                }
            }


            System.Console.WriteLine(result);

            return result;
        }
    }
}
