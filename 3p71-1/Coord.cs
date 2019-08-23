namespace NQueens
{
    /// <summary>
    /// Exists so that coordinates can be passed as a single argument
    /// </summary>
    class Coord
    {
        public int row;
        public int col;
        /// <summary>
        /// Takes two integers and saves them as a coordinate. 
        /// </summary>
        /// <param name="x">The row in which you wish to mark</param>
        /// <param name="y">The colemn in which you wish to mark</param>
        public Coord(int x, int y)
        {
            row = x;
            col = y;
        }
    }
}
