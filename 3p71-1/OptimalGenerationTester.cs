using System;
using System.Collections.Generic;
using System.Text;

namespace NQueens
{
    class OptimalGenerationTester
    {
        public OptimalGenerationTester(int ranSeed)
        {
            int testSize = 1000;
            for (int n = 4; n < 12; n++)//Size of board
            {
                Console.WriteLine("For a board size of " + n);
                Console.WriteLine("Seed: " + ranSeed);
                Console.WriteLine("Number of tests: " + testSize);
                for (int i = 1; i <= n; i++)//Boards to generate
                {
                    int avg = 0;
                    for (int k = 0; k < testSize; k++)//Tests
                    {
                        InformedSearch s = new InformedSearch(n, i, ranSeed);
                        avg += s.GetNumSwaps();
                    }
                    Console.WriteLine(string.Format("For {0} initial board generations, {1} average swaps", i, (avg / testSize)));
                }
                Console.ReadLine();

            }
        }
    }
}

/* Output:
 * Verdict: 3 swaps seems to give enough variety to improve it in most cases but not so much that it gets confused.
 * 
For a board size of 4
Seed: -919347057
Number of tests: 1000
For 1 initial board generations, 3 average swaps
For 2 initial board generations, 18 average swaps
For 3 initial board generations, 20 average swaps
For 4 initial board generations, 12 average swaps

For a board size of 5
Seed: -919347057
Number of tests: 1000
For 1 initial board generations, 2 average swaps
For 2 initial board generations, 4 average swaps
For 3 initial board generations, 2 average swaps
For 4 initial board generations, 2 average swaps
For 5 initial board generations, 2 average swaps

For a board size of 6
Seed: -919347057
Number of tests: 1000
For 1 initial board generations, 6 average swaps
For 2 initial board generations, 2 average swaps
For 3 initial board generations, 4 average swaps
For 4 initial board generations, 6 average swaps
For 5 initial board generations, 4 average swaps
For 6 initial board generations, 2 average swaps

For a board size of 7
Seed: -919347057
Number of tests: 1000
For 1 initial board generations, 96 average swaps
For 2 initial board generations, 150 average swaps
For 3 initial board generations, 8 average swaps
For 4 initial board generations, 94 average swaps
For 5 initial board generations, 47 average swaps
For 6 initial board generations, 18 average swaps
For 7 initial board generations, 13 average swaps

For a board size of 8
Seed: -919347057
Number of tests: 1000
For 1 initial board generations, 77 average swaps
For 2 initial board generations, 121 average swaps
For 3 initial board generations, 77 average swaps
For 4 initial board generations, 72 average swaps
For 5 initial board generations, 66 average swaps
For 6 initial board generations, 73 average swaps
For 7 initial board generations, 83 average swaps
For 8 initial board generations, 70 average swaps

For a board size of 9
Seed: -919347057
Number of tests: 1000
For 1 initial board generations, 88 average swaps
For 2 initial board generations, 3 average swaps
For 3 initial board generations, 3 average swaps
For 4 initial board generations, 10 average swaps
For 5 initial board generations, 5 average swaps
For 6 initial board generations, 5 average swaps
For 7 initial board generations, 6 average swaps
For 8 initial board generations, 3 average swaps
For 9 initial board generations, 5 average swaps

For a board size of 10
Seed: -919347057
Number of tests: 1000
For 1 initial board generations, 154 average swaps
For 2 initial board generations, 190 average swaps
For 3 initial board generations, 20 average swaps
For 4 initial board generations, 99 average swaps
For 5 initial board generations, 206 average swaps
For 6 initial board generations, 46 average swaps
For 7 initial board generations, 6 average swaps
For 8 initial board generations, 69 average swaps
For 9 initial board generations, 31 average swaps
For 10 initial board generations, 14 average swaps

For a board size of 11
Seed: -919347057
Number of tests: 1000
For 1 initial board generations, 4 average swaps
For 2 initial board generations, 24 average swaps
For 3 initial board generations, 19 average swaps
For 4 initial board generations, 35 average swaps
For 5 initial board generations, 118 average swaps
For 6 initial board generations, 193 average swaps
For 7 initial board generations, 56 average swaps
For 8 initial board generations, 25 average swaps
For 9 initial board generations, 83 average swaps
For 10 initial board generations, 24 average swaps
For 11 initial board generations, 97 average swaps
*/
