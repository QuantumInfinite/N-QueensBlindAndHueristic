using System;
using System.Collections.Generic;
using System.Text;

namespace NQueens
{
    /// <summary>
    /// This object represents a chessboard for the purposes of this program. squares on the board are stored in a 2D Square array
    /// </summary>
    class ChessBoard
    {
        public Square[][] board;
        int numQueens = 0;
        public int queenHits = 0;

        public ChessBoard(int size)
        {
            //Create structure
            board = new Square[size][];
            for (int i = 0; i < size; i++)
            {
                board[i] = new Square[size];
            }

            //Initilize
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i][j] = new Square();
                }
            }
            
        }

        /// <summary>
        /// Marks the square at the given coordinate as a queen, 
        /// Marks the hits on each of the squares she can attack,
        /// Increments the number of queens
        /// </summary>
        /// <param name="c">Coordinate for queen to be placed</param>
        public void AddQueen(Coord c)
        {
            board[c.row][c.col].isQueen = true;
            numQueens++;
            manipulateBoard(c);
            CalcQueenHits();
        }

        /// <summary>
        /// Marks the square at the given coordinate as not a queen, 
        /// Removes the former queens marks,
        /// decriment the number of queens
        /// </summary>
        /// <param name="c">Coordinate for queen to be removed</param>
        public void RemoveQueen(Coord c)
        {
            board[c.row][c.col].isQueen = false;
            numQueens--;
            manipulateBoard(c);
            CalcQueenHits();
        }

        /// <summary>
        /// Manipulates the board by adding or removing marks from squares based on whether the given location is a queen
        /// </summary>
        /// <param name="c">Coordinate of starting square</param>
        private void manipulateBoard(Coord c)
        {
            //Determins whether to increment or decreminet marks based on if coord is queen
            int val = (board[c.row][c.col].isQueen) ? 1 : -1;

            int length = board.Length;

            for (int i = 1; i < length; i++)
            {
                //Left
                if (c.col - i >= 0)
                {
                    board[c.row][c.col - i].MarkHit(val);
                }

                //Right
                if (c.col + i < length)
                {
                    board[c.row][c.col + i].MarkHit(val);
                }

                //Down
                if (c.row + i < length)
                {
                    //Down
                    board[c.row + i][c.col].MarkHit(val);

                    //Down Right
                    if (c.col + i < length)
                    {
                        board[c.row + i][c.col + i].MarkHit(val);
                    }

                    //Down Left
                    if (c.col - i >= 0)
                    {
                        board[c.row + i][c.col - i].MarkHit(val);
                    }
                }
                
                //Up
                if(c.row - i >= 0)
                {
                    //Up
                    board[c.row - i][c.col].MarkHit(val);

                    //Up Left
                    if (c.col - i >= 0)
                    {
                        board[c.row - i][c.col - i].MarkHit(val);
                    }

                    //Up Right
                    if (c.col + i < length)
                    {
                        board[c.row - i][c.col + i].MarkHit(val);
                    }
                }
                

            }
        }
        /// <summary>
        /// Iteratively sums the total number of hits of every queen on the board
        /// 
        /// </summary>
        private void CalcQueenHits()
        {
            queenHits = 0;
            for (int k = 0; k < board.Length; k++)
            {
                for (int j = 0; j < board.Length; j++)
                {
                    if (board[k][j].isQueen)
                    {
                        queenHits += board[k][j].GetNumHits();
                    }
                }
            }
        }

        public int GetNumQueens() { return numQueens; }

        /// <summary>
        /// Allows the ChessBoard to be cloned. See documentation for ICloneable
        /// </summary>
        /// <returns>Clone of this object</returns>
        public ChessBoard Clone()
        {
            int length = board.Length;
            ChessBoard newChessboard = new ChessBoard(length);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    newChessboard.board[i][j] = board[i][j].Clone();
                }
            }

            newChessboard.numQueens = numQueens;
            return newChessboard;
        }


    }
}
