using System;
using System.Collections.Generic;
using System.Text;

namespace text_maze_solver
{
    class MazeSolver
    {
        private Maze maze;
        private Stack<Point> pathStack = new Stack<Point>();
        
        public MazeSolver(Maze maze)
        {
            this.maze = maze;
        }

        public bool Solve()
        {
            // start with starting point, no previous point, and empty path stack.
            bool solved = DoSolve(maze.GetStartingPoint(), null);

            if (solved)
            {
                int[,] solvedMazeArray = maze.GetMazeArray();
                while(pathStack.Count > 0)
                {
                    Point point = pathStack.Pop();
                    solvedMazeArray[point.row, point.col] = 2;
                }

                maze.SetMazeArray(solvedMazeArray);
                return true;
            }

            return false;
        }

        public bool DoSolve(Point currentPoint, Point previousPoint)
        {
            // If point is equal to ending one, push it and return true.
            if (currentPoint.Equals(maze.GetEndingPoint()))
            {
                pathStack.Push(currentPoint);
                return true;
            }

            List<Point> availablePoints;

            // Get the available points for the current point, excluding the previous one.
            if (previousPoint != null)
                availablePoints = maze.GetOpenPathPoints(currentPoint, previousPoint);
            else
                availablePoints = maze.GetOpenPathPoints(currentPoint, null);

            // Print progress
            Console.Write(currentPoint.ToString() + "--> ");
            foreach (Point openPoint in availablePoints)
            {
                Console.Write(openPoint.ToString() + " ");
            }
            Console.WriteLine();

            if (availablePoints.Count == 0)
                return false;

            foreach (Point openPoint in availablePoints)
            {
                // Push the current point
                pathStack.Push(currentPoint);

                // Check if current point leads to a solution
                if (DoSolve(openPoint, currentPoint))
                    return true;

                // Current point doesn't lead to a solution, remove it (backtrack).
                Console.WriteLine("Backtracking to: " + pathStack.Pop());
            }

            // No available point of the current path leads to a solution.
            return false;
        }

    }
}
