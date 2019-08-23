using System;

namespace NQueens
{
    class Printer
    {
        /// <summary>
        /// Iterates over a board and prints it to console
        /// </summary>
        /// <param name="board">Board to print</param>
        public void Print(Square[][] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j].isQueen)
                    {
                        Console.Write('Q');
                    }
                    else if (board[i][j].GetNumHits() > 0)
                    {
                        Console.Write('-');
                    }
                    else
                    {
                        Console.Write('X');
                    }
                    Console.Write("   ");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Used for debugging, prints the same as above except it lists how many hits each square has
        /// </summary>
        /// <param name="board">board to print</param>
        public void PrintHits(Square[][] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j].isQueen)
                    {
                        Console.Write('Q');
                    }
                    Console.Write(board[i][j].GetNumHits());
                    
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
        }
    }
}
