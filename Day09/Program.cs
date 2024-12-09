using System.Data;

namespace Day09
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            DoWork(true, true);
        }

        private record Block(int startIndex, int size, int id);
        private record Gap(int startIndex, int size);

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

            List<int> filesystem = [];
            List<Gap> gaps = [];
            Stack<Block> blocks = [];

            bool bIsGap = false;
            int blockId = 0;

            foreach(char c in input)
            {
                int digit = c - '0'; // Convert char to int directly

                if (bIsGap)
                {
                    gaps.Add(new Gap(filesystem.Count, digit));

                    filesystem.AddRange(Enumerable.Repeat(-1, digit));

                }
                else
                {
                    blocks.Push(new Block(filesystem.Count, digit, blockId));

                    filesystem.AddRange(Enumerable.Repeat(blockId, digit));
                    ++blockId;
                }

                bIsGap = !bIsGap;
            }



            if (dontFragment)
            {
                foreach(Block block in blocks)
                {
                    var gap = gaps.Find(g => g.size >= block.size && g.startIndex < block.startIndex);

                    if(gap is null)
                    {
                        continue;
                    }

                    for (int i = 0; i < block.size; i++)
                    {
                        filesystem[gap.startIndex + i] = block.id;
                        filesystem[block.startIndex + i] = -1;
                    }

                    gaps.Remove(gap);
                    if(gap.size > block.size)
                    {
                        gaps.Add(new Gap(gap.startIndex + block.size, gap.size - block.size));
                        gaps.Sort((a, b) => a.startIndex.CompareTo(b.startIndex));
                    }
                }
            }
            else
            {
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
            }

            long result = 0;

            for(int i=0;i<filesystem.Count;i++)
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
