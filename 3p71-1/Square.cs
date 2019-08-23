using System;
using System.Collections.Generic;
using System.Text;

namespace NQueens
{
    /// <summary>
    /// This object represents a square on a chessboard.
    /// For the purposes of this object, a 'hit' is when a queen is able to attack this square.
    /// </summary>
    class Square
    {
        public bool isQueen = false;
        int numHits = 0;
                
        public int GetNumHits() { return numHits; }

        public void MarkHit(int val )
        {
            numHits += val;
        }

        /// <summary>
        /// Inherited to enable a deep cloning of a ChessBoard. See documentation for ICloneable
        /// </summary>
        /// <returns>A by value clone of this object</returns>
        public Square Clone()
        {
            Square newSquare = new Square();
            newSquare.isQueen = isQueen;
            newSquare.numHits = numHits;
            return newSquare;
        }
    }
}
