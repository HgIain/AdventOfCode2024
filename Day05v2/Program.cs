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

            Dictionary<int, SortedSet<int>> pageOrders = [];

            for(;i<input.Length; i++)
            {
                if(String.IsNullOrWhiteSpace(input[i]))
                {
                    break;
                }

                var match = pageOrderRegex().Match(input[i]);

                int x = Convert.ToInt32(match.Groups[1].Value);
                int y = Convert.ToInt32(match.Groups[2].Value);

                if (pageOrders.TryGetValue(x, out var set))
                {
                    set.Add(y);
                }
                else
                {
                    pageOrders[x] = new SortedSet<int> { y };
                }
            }

            i++;

            int result = 0;

            for (;i<input.Length; i++)
            {
                var matches = updatePagesRegex().Matches(input[i]);

                var listInts = new List<int>();

                bool success = true;

                foreach (var match in matches)
                {
                    int newPage = int.Parse(match.ToString()!);

                    bool inOrder = true;

                    if (pageOrders.TryGetValue(newPage, out var set))
                    {
                        inOrder = listInts.All(x => !set.Contains(x));
                    }

                    if (!inOrder)
                    {
                        success = false;

                        if (onlyInOrder)
                        {
                            break;
                        }
                    }

                    listInts.Add(newPage);
                }

                if(!success && onlyInOrder)
                {
                    continue;
                }

                if(success && !onlyInOrder)
                {
                    continue;
                }

                if (!onlyInOrder)
                {
                    // ok, let's reorder these lists
                    listInts.Sort((a, b) =>
                    {
                        if(pageOrders.TryGetValue(a, out var set))
                        {
                            if (set.Contains(b))
                            {
                                return -1;
                            }
                        }
                        if (pageOrders.TryGetValue(b, out var set2))
                        {
                            if (set2.Contains(a))
                            {
                                return 1;
                            }
                        }
                        return 0;
                    });
                }

                int half = (listInts.Count / 2);

                result += listInts[half];
            }
            return result;
        }
    }
}
