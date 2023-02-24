using System;
using System.Collections.Generic;
using System.Text;

namespace text_maze_solver
{
    class Point
    {
        public int row;
        public int col;

        public Point(int x, int y)
        {
            this.row = x;
            this.col = y;
        }

        public bool Equals(Point other)
        {
            if (other == null)
                return false;

            return this.row == other.row && this.col == other.col;
        }

        override public string ToString()
        {
            return "(" + row + ", " + col + ")";
        }
    }
}
