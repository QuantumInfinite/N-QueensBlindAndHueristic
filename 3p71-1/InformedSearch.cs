using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NQueens
{
    /// <summary>
    /// This class performs a hueristic search for the N Queens problem
    /// </summary>
    class InformedSearch
    {
        int gridSize;
        int maxSteps;
        int swapCounter = 0;
        int boardCounter = 0;
        int initialBoardCount = 3; //The generation of 3 initial boards was chosen after the testing shown in OptimalGenerationTester.cs. 3 came out better on average than any other
        ChessBoard solution;
        Printer printer;
        Random rnd;
        
        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="size">size of board to make</param>
        /// <param name="ranSeed">seed for randomization</param>
        public InformedSearch(int size, int ranSeed)
        {
            solution = HuristicSearch(size, ranSeed);
        }
        /// <summary>
        /// Alternate constructor for testing, or when the number of initial boards needs to be changed
        /// </summary>
        /// <param name="size">size of board to make</param>
        /// <param name="boardCount">number of boards to initialy push into the queue</param>
        /// <param name="ranSeed">seed for randomization</param>
        public InformedSearch(int size, int boardCount, int seed)
        {
            initialBoardCount = boardCount;
            solution = HuristicSearch(size, seed);
        }

        /// <summary>
        /// Main function. sets the base variables and calls the needed functions to find a solution
        /// </summary>
        /// <param name="size">size of board to make</param>
        /// <param name="ranSeed">seed for random</param>
        /// <returns>Either a solution or null</returns>
        public ChessBoard HuristicSearch(int size, int ranSeed)
        {
            printer = new Printer();
            gridSize = size;
            maxSteps = size * size; // As explained in chapter 10, this is the theoretical max it could take to find an answer
            rnd = new Random(ranSeed);
            
            for (int i = 0; i < maxSteps; i++)//Limits the number of generated boards to maxSteps. Requiring more boards than this is statistically impossible
            {
                solution = MinConSearch();
                if (solution != null)
                {
                    printer.Print(solution.board);
                    Console.WriteLine(String.Format("\nsolution found! \nTotal swaps: {0}\nBoards Generated: {1}", swapCounter, boardCounter));
                    Console.WriteLine("Seed: " + ranSeed);
                    return solution;
                }
            }

            //On a board > 3, this should never be reached
            Console.WriteLine("\nNo solution found. maxSteps insufficient");
            return null;
        }


        /// <summary>
        /// Main algorithm.
        /// Generates a number of boards with randomly placed queens, default 3 and queues them.
        /// Chooses fittest board (based off the number of total hits all the queens on that board have)
        /// Applies a switch based off the algorithm defined in Section 6.4 of Artifical Intelligence: A modern approach Third edition pg 221
        /// </summary>
        /// <returns>Null if failed. Proper chessboard if success</returns>
        private ChessBoard MinConSearch()
        {
            Queue<ChessBoard> queue = new Queue<ChessBoard>();
            
            for (int k = 0; k < initialBoardCount; k++)//Generate initial boards. Default 3
            {
                ChessBoard baseBoard = new ChessBoard(gridSize);

                //Randomly fill baseboard
                int[] ranOrder = RanOrder(gridSize);
                for (int i = 0; i < baseBoard.board.Length; i++)
                {
                    Coord nextQueen = new Coord(i, ranOrder[i]);
                    baseBoard.AddQueen(nextQueen);
                }
                queue.Enqueue(baseBoard);

                boardCounter++;
            }

            
            while(queue.Count < maxSteps)
            {
                queue = Prioritize(queue);//Sort
                ChessBoard currentBoard = queue.ElementAt(0); //Get best board without removing from queue

                if (CheckSolved(currentBoard))//Check if solved
                {
                    return currentBoard;
                }

                Coord worstQueen = FindWorstQueen(currentBoard);
                Coord bestSquare = FindBestSquare(currentBoard, worstQueen.row);

                queue.Enqueue(MoveQueen(currentBoard, worstQueen, bestSquare));
                swapCounter++;
            }
            return null;
        }

        /// <summary>
        /// Checks if the chessboard is solved. 
        /// This is done by iterating over every square checking it for fail conditions
        /// </summary>
        /// <param name="currentBoard">Board to check</param>
        /// <returns>Whether the board is solved</returns>
        private bool CheckSolved(ChessBoard currentBoard)
        {
            if(currentBoard.GetNumQueens() != gridSize)
            {
                return false;
            }
            for (int k = 0; k < currentBoard.board.Length; k++)
            {
                for (int j = 0; j < currentBoard.board.Length; j++)
                {
                    if (currentBoard.board[k][j].isQueen && currentBoard.board[k][j].GetNumHits() > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// Sorts a  queue of chessboards from best to worst in terms of each boards queenHits 
        /// </summary>
        /// <param name="queue">Queue to be sorted</param>
        /// <returns>Sorted queue</returns>
        private Queue<ChessBoard> Prioritize(Queue<ChessBoard> queue)
        {
            return new Queue<ChessBoard>(queue.OrderBy(board => board.queenHits));
        }

        /// <summary>
        /// Finds the coordinate of the 'best' square in the row of the given coord. The best square has the least hits
        /// </summary>
        /// <param name="currentBoard">Board on which to perform the search</param>
        /// <param name="row">Row on which to search</param>
        /// <returns>Coordinate where it would be best to place the next queen</returns>
        private Coord FindBestSquare(ChessBoard currentBoard, int row)
        {
            List<Coord> bestSquares = new List<Coord>();
            int benchMark = int.MaxValue;
            for (int j = 0; j < currentBoard.board.Length; j++)
            {
                if (!currentBoard.board[row][j].isQueen)
                {
                    if (currentBoard.board[row][j].GetNumHits() < benchMark) //Square is Better than best square
                    {
                        benchMark = currentBoard.board[row][j].GetNumHits();
                        bestSquares.Clear();
                        bestSquares.Add(new Coord(row,j));
                    }
                    else if (currentBoard.board[row][j].GetNumHits() == benchMark) //Square is as good as best square
                    {
                        bestSquares.Add(new Coord(row, j));
                    }
                }

            }
            
            //return random element in list
            return bestSquares[rnd.Next(bestSquares.Count)];
        }

        /// <summary>
        /// Finds the coordinate of the 'worst' queen on the board, meaning the queen with the most hits.
        /// If there is a tie, choose a random queen with the same number of hits
        /// </summary>
        /// <param name="currentBoard">Board on which to perform the search</param>
        /// <returns>Coord of the worst queen</returns>
        public Coord FindWorstQueen(ChessBoard currentBoard)
        {
            List<Coord> worstQueens = new List<Coord>();
            int benchMark = 0;
            for (int k = 0; k < currentBoard.board.Length; k++)
            {
                for (int j = 0; j < currentBoard.board.Length; j++)
                {
                    if (currentBoard.board[k][j].isQueen)
                    {
                        if (currentBoard.board[k][j].GetNumHits() > benchMark) //Square is WORSE than worst queen
                        {
                            benchMark = currentBoard.board[k][j].GetNumHits();
                            worstQueens.Clear();
                            worstQueens.Add(new Coord(k,j));
                        }
                        else if (currentBoard.board[k][j].GetNumHits() == benchMark) //Square is as bad as worst queen
                        {
                            worstQueens.Add(new Coord(k, j));
                        }
                    }
                    
                }
            }
            //return random element in list
            return worstQueens[rnd.Next(worstQueens.Count)];
        }

        /// <summary>
        /// Moves queen from the worstQueen square to the bestSquare square
        /// </summary>
        /// <param name="currentBoard">Chessboard onwhich to apply the move</param>
        /// <param name="worstQueen">Square containing the worst placed queen</param>
        /// <param name="bestSquare">Square with the least hits in the same row</param>
        /// <returns></returns>
        public ChessBoard MoveQueen(ChessBoard currentBoard, Coord worstQueen, Coord bestSquare)
        {
            currentBoard.RemoveQueen(worstQueen);

            currentBoard.AddQueen(bestSquare);

            return currentBoard;
        }

        /// <summary>
        /// Returns an array of random integers wherein no integer repeats. 
        /// Shuffle alogorthm is bassed of Knuth shuffle
        /// </summary>
        /// <param name="size">Desired size of list</param>
        /// <returns>Ranomized array of </returns>
        public int[] RanOrder(int size)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < size; i++) //Initialize
            {
                list.Add(i);
            }
            for (int i = 0; i < list.Count; i++) //Randomize
            {
                int rnd = this.rnd.Next(i, list.Count);
                int t = list[i];
                list[i] = list[rnd];
                list[rnd] = t;
            }
            return list.ToArray();
        }


        public ChessBoard GetSolution() { return solution; }

        public int GetNumSwaps() { return swapCounter; }
    }
}
