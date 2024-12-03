using System.Text.RegularExpressions;

namespace Day02
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            DoWork(true);
        }

        [GeneratedRegex(@"(-?\d+)\s*")]
        private static partial Regex Regex();
        
        private static bool IsValidList(List<int> levels, int allowedErrors)
        {
            for (int i = 0; i < levels.Count - 1; i++)
            {
                int diff = levels[i + 1] - levels[i];

                if (diff < 1 || diff > 3)
                {
                    if(allowedErrors > 0)
                    {
                        List<int> newList = levels
                            .Where((item, index) => index != i)
                            .ToList();

                        if(IsValidList(newList, allowedErrors -1))
                        {
                            return true;
                        }
                        else
                        {
                            List<int> newList2 = levels
                                .Where((item, index) => index != i+1)
                                .ToList();

                            return IsValidList(newList2, allowedErrors - 1);
                        }
                    }

                    return false;
                }
            }

            return true;
        }

        
        static public Int64 DoWork(bool fullData, int allowedErrors = 0)
        {
            string[] lines;

            if(fullData)
                lines = System.IO.File.ReadAllLines("Day02FullInput.txt");
            else
                lines = System.IO.File.ReadAllLines("Day02TestInput.txt");

            Int64 result = 0;

            foreach (var line in lines)
            {
                MatchCollection matches = Regex().Matches(line);

                List<int> numbers = [];

                foreach(var match in matches)
                {
                    var x = int.Parse(match.ToString()!);

                    numbers.Add(x);
                }

                bool valid = IsValidList(numbers, allowedErrors);

                if(!valid)
                {
                    numbers.Reverse();
                    valid = IsValidList(numbers, allowedErrors);
                }

                if(valid)
                {
                    ++result;
                }
            }


            return result;
        }
    }
}
