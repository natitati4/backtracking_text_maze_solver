using System;
using System.Collections.Generic;
using System.Text;

namespace text_maze_solver
{
    class Maze
    {
        public const int MAZE_WALL = 0;
        public const int MAZE_PATH = 1;
        public const int MAZE_ENTRY = 2;

        private int[,] mazeArray;
        private Point startingPoint;
        private Point endingPoint;

        public Maze(string mazeText)
        {
            int rows = mazeText.Split('\n').Length; // get num of rows by spliting by '\n'.
            int columns = mazeText.IndexOf('\n'); // index of first '\n' is number of char in row, i.e. num of cols.
            mazeArray = new int[rows, columns];

            // Fill in the array.
            for (int i = 0; i < mazeArray.GetLength(0); i++)
            {
                for (int j = 0; j < mazeArray.GetLength(1); j++)
                {
                    // parse the number to int and store in the multidimensional array.
                    Int32.TryParse(mazeText[i * mazeArray.GetLength(0) + j].ToString(), out mazeArray[i, j]);
                }
            }

            // find starting point.
            for (int i = 0; i < mazeArray.GetLength(0); i++)
            {
                for (int j = 0; j < mazeArray.GetLength(1); j++)
                {
                    if (mazeArray[i, j] == MAZE_ENTRY)
                    {
                        startingPoint = new Point(i, j);
                        break;
                    }
                }
            }

            // find ending point.
            for (int i = 0; i < mazeArray.GetLength(0); i++)
            {
                for (int j = 0; j < mazeArray.GetLength(1); j++)
                {
                    Point currentCheckingPoint = new Point(i, j);
                    if ((mazeArray[i, j] == MAZE_PATH) && IsEdge(currentCheckingPoint))
                    {
                        endingPoint = currentCheckingPoint;
                        break;
                    }
                }
            }
        }

        new public string ToString()
        {
            StringBuilder mazeStringBuilder = new StringBuilder();
            for (int i = 0; i < mazeArray.GetLength(0); i++)
            {
                for (int j = 0; j < mazeArray.GetLength(1); j++)
                {
                    mazeStringBuilder.Append(mazeArray[i, j]);
                }

                if (i != mazeArray.GetLength(0) - 1)
                    mazeStringBuilder.Append("\n");
            }

            return mazeStringBuilder.ToString();
        }

        public Point GetStartingPoint()
        {
            return startingPoint;
        }

        public Point GetEndingPoint()
        {
            return endingPoint;
        }

        public int[,] GetMazeArray()
        {
            return mazeArray;
        }

        public void SetMazeArray(int [,] mazeArray)
        {
            this.mazeArray = mazeArray;
        }

        public bool IsEdge(Point point)
        {
            return point.row == 0 ||
                point.row == mazeArray.GetLength(0) - 1 ||
                point.col == 0 ||
                point.col == mazeArray.GetLength(1) - 1;
        }

        public List<Point> GetOpenPathPoints(Point point, Point pointCameFrom)
        {
            List<Point> pointOpenPaths = new List<Point>();

            // has up
            if (point.row > 0)
            {
                if (mazeArray[point.row - 1, point.col] == MAZE_PATH && !new Point(point.row - 1, point.col).Equals(pointCameFrom))
                    pointOpenPaths.Add(new Point(point.row - 1, point.col));
            }

            // has down
            if (point.row < mazeArray.GetLength(0) - 1)
            {
                if (mazeArray[point.row + 1, point.col] == MAZE_PATH && !new Point(point.row + 1, point.col).Equals(pointCameFrom))
                    pointOpenPaths.Add(new Point(point.row + 1, point.col));
            }

            // has left
            if (point.col > 0)
            {
                if (mazeArray[point.row, point.col - 1] == MAZE_PATH && !new Point(point.row, point.col - 1).Equals(pointCameFrom))
                    pointOpenPaths.Add(new Point(point.row, point.col - 1));
            }

            // has right
            if (point.col < mazeArray.GetLength(1) - 1)
            {
                if (mazeArray[point.row, point.col + 1] == MAZE_PATH && !new Point(point.row, point.col + 1).Equals(pointCameFrom))
                    pointOpenPaths.Add(new Point(point.row, point.col + 1));
            }

            return pointOpenPaths;

        }
    }
}
