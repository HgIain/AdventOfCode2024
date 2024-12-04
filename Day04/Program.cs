namespace Day04
{
    public class Program
    {
        static void Main(string[] args)
        {
            DoWork(true, true);
        }

        public static bool WordPresent(string word, int x, int y, char[,] grid, int xdiff, int ydiff, int width, int height)
        {
            int wordLength = word.Length;

            int xEnd = x + (wordLength - 1) * xdiff;
            int yEnd = y + (wordLength - 1) * ydiff;

            if(xEnd < 0 || xEnd >= width || yEnd < 0 || yEnd >= height)
            {
                // early return if the word would go off the grid
                return false;
            }

            for (int i = 0; i < word.Length; i++)
            {
                if (grid[x, y] != word[i])
                {
                    return false;
                }

                x += xdiff;
                y += ydiff;
            }
            return true;
        }

        public static bool MiddleOfCross(int x, int y, char[,] grid, int width, int height)
        {
            if (x < 1 || x >= width - 1 || y < 1 || y >= height - 1)
            {
                // early return if the word would go off the grid
                return false;
            }

            char mors = grid[x+1, y+1];
            char sorm = grid[x - 1, y - 1];

            if((mors != 'M' || sorm != 'S') 
                && (mors != 'S' || sorm != 'M'))
            {
                return false;
            }

            mors = grid[x + 1, y - 1];
            sorm = grid[x - 1, y + 1];

            if ((mors != 'M' || sorm != 'S')
                && (mors != 'S' || sorm != 'M'))
            {
                return false;
            }


            return true;
        }


        public static Int64 DoWork(bool fullData = false, bool crossCheck = false)
        {
            string[] input;
            
            if(fullData)
                input = File.ReadAllLines("Day04FullInput.txt");
            else
                input = File.ReadAllLines("Day04TestInput.txt");


            int height = input.Length;
            int width = input[0].Length;

            var grid = new char[width, height];

            for (int y = 0; y < height; y++) 
            {
                for (int x = 0; x < width; x++)
                {
                    grid[x, y] = input[y][x];
                }
            }

            if (crossCheck)
            {
                Int64 result = 0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (grid[x, y] != 'A')
                        {
                            continue;
                        }

                        if (MiddleOfCross(x, y, grid, width, height))
                        {
                            result++;
                        }
                    }
                }

                return result;
            }
            else
            {
                Int64 result = 0;

                List<(int xdiff, int diff)> directions =
                [
                    (1, 0),
                    (1, 1),
                    (1, -1),
                    (0, 1),
                    (0, -1),
                    (-1, 1),
                    (-1, 0),
                    (-1, -1),
                ];

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (grid[x, y] != 'X')
                        {
                            continue;
                        }

                        foreach (var (xdiff, ydiff) in directions)
                        {
                            if (WordPresent("XMAS", x, y, grid, xdiff, ydiff, width, height))
                            {
                                result++;
                            }
                        }
                    }
                }


                return result;
            }
        }
    }
}
