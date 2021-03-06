using System;
using System.Threading.Tasks;

namespace collatz
{
    class Program
    {
        static int maxsteps = 0;
        static long maxval = 0;

        static void Main(string[] args)
        {
            var start = DateTime.Now;
            go(1000_000);
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}  ms. number: {maxval}. steps: {maxsteps -1}");
            Console.ReadKey();
        }

        private static void go(long range)
        {
            ushort[] result = new ushort[range];
            Parallel.ForEach(result, (r, psl, index) =>
            {
                long i = index;
                ushort steps = 1;
                while (i > 1)
                {
                    if (i < range && result[i] > 0)
                    {
                        steps += (ushort)(result[i] - 1);
                        break;
                    }
                    i = i % 2 == 0 ? i / 2 : i * 3 + 1;
                    steps++;
                }
                result[index] = steps;
                if (steps > maxsteps)
                {
                    maxsteps = steps;
                    maxval = index;
                }
            });
        }
    }
}
