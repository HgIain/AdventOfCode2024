using System.Data;

namespace Day09
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            DoWork(false, true);
        }

        private record Block(int startIndex, int size, int id);
        private record Gap(int startIndex, int size) : IComparable<Gap>
        {
            public int CompareTo(Gap? other)
            {
                if (other is null) return 1;
                return startIndex.CompareTo(other.startIndex);
            }
        }

        public static long DoWork(bool isFull = false, bool dontFragment = false)
        {
            string input;

            if (isFull)
            {
                input = System.IO.File.ReadAllText("Day09FullInput.txt");
            }
            else
            {
                input = System.IO.File.ReadAllText("Day09TestInput.txt");
            }






            if (dontFragment)
            {
                SortedSet<Gap> gaps = [];
                Stack<Block> blocks = [];
                Dictionary<int,int> filesystem = [];

                bool bIsGap = false;
                int blockId = 0;
                int totalFilesize = 0;

                foreach (char c in input)
                {
                    int digit = c - '0'; // Convert char to int directly

                    if (bIsGap)
                    {
                        gaps.Add(new Gap(totalFilesize, digit));
                    }
                    else
                    {
                        blocks.Push(new Block(totalFilesize, digit, blockId));
                        ++blockId;
                    }
                    totalFilesize += digit;
                    bIsGap = !bIsGap;
                }

                foreach (Block block in blocks)
                {
                    var gap = gaps.FirstOrDefault(g => g.size >= block.size && g.startIndex < block.startIndex);

                    if (gap is null)
                    {
                        for (int i = 0; i < block.size; i++)
                        {
                            filesystem[block.startIndex + i] = block.id;
                        }
                        continue;
                    }

                    for (int i = 0; i < block.size; i++)
                    {
                        filesystem[gap.startIndex + i] = block.id;
                    }

                    gaps.Remove(gap);
                    if(gap.size > block.size)
                    {
                        gaps.Add(new Gap(gap.startIndex + block.size, gap.size - block.size));
                    }
                }

                long result = 0;

                foreach (var block in filesystem)
                {
                    result += block.Key * block.Value;
                }

                return result;
            }
            else
            {
                List<int> filesystem = [];

                bool bIsGap = false;
                int blockId = 0;

                foreach (char c in input)
                {
                    int digit = c - '0'; // Convert char to int directly

                    if (bIsGap)
                    {
                        filesystem.AddRange(Enumerable.Repeat(-1, digit));
                    }
                    else
                    {
                        filesystem.AddRange(Enumerable.Repeat(blockId, digit));
                        ++blockId;
                    }

                    bIsGap = !bIsGap;
                }

                int start = 0;
                int end = filesystem.Count - 1;

                while (start < end)
                {
                    if (filesystem[start] == -1 && filesystem[end] != -1)
                    {
                        filesystem[start] = filesystem[end];
                        filesystem[end] = -1;
                        ++start;
                        --end;
                    }
                    else
                    {
                        if (filesystem[end] == -1)
                        {
                            --end;
                        }
                        if (filesystem[start] != -1)
                        {
                            ++start;
                        }
                    }
                }

                long result = 0;

                for (int i = 0; i < filesystem.Count; i++)
                {
                    if (filesystem[i] != -1)
                    {
                        result += filesystem[i] * i;
                    }
                }


                return result;
            }


        }
    }
}
