using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Day3
{
    public partial class Program
    {
        public static string CopySkippingDontToDo(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            ReadOnlySpan<char> span = input.AsSpan();
            StringBuilder result = new StringBuilder();
            int i = 0;
            bool skip = false;

            string dont = "don't()";
            var dontLength = dont.Length;
            string doString = "do()";
            var doLength = doString.Length;

            while (i < span.Length)
            {
                // Check for the start of a "don't()" block
                if (!skip && i + dontLength <= span.Length && span.Slice(i, dontLength).SequenceEqual(dont))
                {
                    skip = true;
                    i += dontLength; // Skip the "don't()" part
                    continue;
                }

                // Check for the end of a "do()" block
                if (skip && i + doLength <= span.Length && span.Slice(i, doLength).SequenceEqual(doString))
                {
                    skip = false;
                    i += doLength; // Skip the "do()" part
                    continue;
                }

                // Append characters to the result if not skipping
                if (!skip)
                {
                    result.Append(span[i]);
                }

                i++;
            }

            return result.ToString();
        }

        [GeneratedRegex(@"mul\((-?\d+),(-?\d+)\)")]
        private static partial Regex regex();

        static public void Main(string[] args)
        {
            DoWork(true, true);
        }


        static public Int64 DoWork(bool skipDonts, bool fullData)
        {
            Console.WriteLine("Hello, World!");

            string input;

            if (fullData)
            {
                input = File.ReadAllText("Day3FullInput.txt");
            }
            else
            {
                if(skipDonts)
                    input = File.ReadAllText("Day3TestInput2.txt");
                else
                    input = File.ReadAllText("Day3TestInput1.txt");
            }
             
            if(skipDonts)
                input = CopySkippingDontToDo(input);

            MatchCollection matches = regex().Matches(input);

            Int64 total = 0;

            foreach (Match match in matches)
            {
                Int64 x = Convert.ToInt64(match.Groups[1].Value);
                Int64 y = Convert.ToInt64(match.Groups[2].Value);
                Console.WriteLine($"Found match: {match.Value} with X = {x} and Y = {y}");

                total += x * y;
            }

            Console.WriteLine($"Total is {total}");

            return total;
        }
    }
}
