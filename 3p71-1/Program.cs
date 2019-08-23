using System;

namespace NQueens
{
    class Program
    {
        static void Main(string[] args)
        {
            //variables
            int size;
            int seed;

            //Set up
            Console.WriteLine("Input board size >= 4. Default = 8");
            size = (Int32.TryParse(Console.ReadLine(), out size) && size > 3) ? size : 8;
            Console.WriteLine("Board size of " + size + " selected");

            Console.WriteLine("Input seed. Default = Current Time");
            seed = Int32.TryParse(Console.ReadLine(), out seed) ? seed : (int)Math.Abs((int)DateTime.Now.ToBinary());
            Console.WriteLine("Seed of " + seed + " selected");

            Console.WriteLine();
            //Actual searches

            Console.WriteLine("Press Enter to run blindsearch");
            Console.ReadLine();
            var timer = System.Diagnostics.Stopwatch.StartNew();
            BlindSearch blind = new BlindSearch(size);
            Console.WriteLine("Blind search compleated in: " + timer.ElapsedMilliseconds + "ms");

            timer.Reset();

            Console.WriteLine("\n\nPress Enter to run Hueristic search");
            Console.ReadLine();

            timer.Start();
            InformedSearch huristic = new InformedSearch(size, seed);
            Console.WriteLine("Hueristic search compleated in: " + timer.ElapsedMilliseconds + "ms");
            timer.Stop();

            Console.ReadLine();
        }
    }
}