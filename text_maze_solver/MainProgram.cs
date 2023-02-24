using System;

namespace text_maze_solver
{
    class MainProgram
    {

        static void Main(string[] args)
        {
            // Get maze txt file path.
            string mazeFilePath = @"D:\other_projects\text_maze_solver\mazes\maze1.txt";

            // Get maze text.
            string mazeText = System.IO.File.ReadAllText(mazeFilePath);

            // Turn to maze.
            Maze maze = new Maze(mazeText);
            Console.WriteLine(maze.ToString());

            // Pass to MazeSolver and solve.
            MazeSolver mazeSolver = new MazeSolver(maze);
            if (mazeSolver.Solve())
            {
                Console.WriteLine(maze.ToString());

                // Finally, write result to a file.
                System.IO.File.WriteAllText(mazeFilePath + "_solved.txt", maze.ToString());
            }
            else
                Console.WriteLine("Maze cannot be solved.");
        }
    }
}
