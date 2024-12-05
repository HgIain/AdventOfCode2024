using System.Text.RegularExpressions;

namespace Day05v2
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            DoWork(true, false);
        }

        [GeneratedRegex(@"(\d+)\|(\d+)")]
        private static partial Regex pageOrderRegex();

        [GeneratedRegex(@"(\d+)+")]
        private static partial Regex updatePagesRegex();

        public static int DoWork(bool fullData, bool onlyInOrder)
        {
            string[] input;

            if (fullData)
                input = File.ReadAllLines("Day05FullInput.txt");
            else
                input = File.ReadAllLines("Day05TestInput.txt");

            int i = 0;

            var pageOrders = new Dictionary<int, SortedSet<int>>();

            for (; i < input.Length; i++)
            {
                if (String.IsNullOrWhiteSpace(input[i]))
                {
                    break;
                }

                var match = pageOrderRegex().Match(input[i]);

                int x = Convert.ToInt32(match.Groups[1].Value);
                int y = Convert.ToInt32(match.Groups[2].Value);

                if (!pageOrders.TryGetValue(x, out var set))
                {
                    set = new SortedSet<int>();
                    pageOrders[x] = set;
                }
                set.Add(y);
            }

            i++;

            int result = 0;

            for (; i < input.Length; i++)
            {
                var matches = updatePagesRegex().Matches(input[i]);

                var listInts = new List<int>();
                bool success = true;

                foreach (Match match in matches)
                {
                    int newPage = int.Parse(match.Value);

                    if (pageOrders.TryGetValue(newPage, out var set) && listInts.Any(x => set.Contains(x)))
                    {
                        success = false;
                        if (onlyInOrder)
                        {
                            break;
                        }
                    }

                    listInts.Add(newPage);
                }

                if (!success && onlyInOrder)
                {
                    continue;
                }

                if (success && !onlyInOrder)
                {
                    continue;
                }

                if (!onlyInOrder)
                {
                    // Reorder the list using a custom comparator
                    listInts.Sort((a, b) =>
                    {
                        if (pageOrders.TryGetValue(a, out var setA) && setA.Contains(b))
                        {
                            return -1;
                        }
                        if (pageOrders.TryGetValue(b, out var setB) && setB.Contains(a))
                        {
                            return 1;
                        }
                        return 0;
                    });
                }

                int half = listInts.Count / 2;
                result += listInts[half];
            }

            return result;
        }
    }
}
