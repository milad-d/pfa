using System;
using System.Windows.Forms;
using PathFinder.Algorithm_Test;
using java.util;

namespace PathFinder
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            //Application.Run(new TestForm());
            ///Application.Run(new SimulationForm());

            //new AlgorithmsTest();
            //System.Console.Read();

            // Test Binary Heap
            // Note that output type of project must be setup to Console appliacation
            /*Heap.BinaryHeapTest h = new PathFinder.Heap.BinaryHeapTest();
            h.Test();
            System.Console.Read();*/
        }
    }

    public class AlgorithmsTest
    {
        int DISTANCE_BEFORE_CHANGE = 10;// -> Step 25;
        int MAZE_W = 300;
        int MAZE_H = 300;
        int N_CHANGED_CELLS = 1; //->k  //(int)Math.round((MAZE_W * MAZE_H) * 0.025);
        int MAZE_CELL_MAX_COST = 1;
        float PROBABILITY_TO_BLOCK_A_CELL = 0.1f;
        static TieBreakingStrategy tie_breaking_strategy = TieBreakingStrategy.HIGHEST_G_VALUES;

        public AlgorithmsTest()
        {
            DateTime t0 = DateTime.Now;

            int MazeCount = 5;
            int TotalSearchs = 0;
            int SuccessCount = 0;

            for (int c = 0; c < MazeCount; c++)
            {
                int OneSearch = 0;

                HashSet blocked_cells = new HashSet(), unblocked_cells = new HashSet();
                long seed = DateTime.Now.Ticks;
                //seed = 27261468842294L;
                LightCell maze_cell;
                java.util.Random random = new java.util.Random(seed);
                System.Console.WriteLine("Seed: " + seed);

                Maze maze = new Maze(seed, MAZE_W, MAZE_H, PROBABILITY_TO_BLOCK_A_CELL, MAZE_CELL_MAX_COST);
                //GAAStarLazy gaa_star_lazy = new GAAStarLazy(maze , true , false , tie_breaking_strategy ,
                //        ManhattanDistanceHeuristic.GetManhattanDistanceHeuristic() , Maze.N_DIRECTIONS_WITHOUT_DIAGONALS);

                for (maze_cell = maze.GetStart(); maze_cell != maze.GetGoal(); )
                {
                    AStar a_star = new AStar(maze, true, false, tie_breaking_strategy,
                            ManhattanDistanceHeuristic.GetManhattanDistanceHeuristic(), Maze.N_DIRECTIONS_WITHOUT_DIAGONALS);

                    a_star.Solve();
                    OneSearch++;

                    //System.Console.WriteLine("A*:\n" + maze);
                    //System.Console.WriteLine("A*:\n");

                    //maze.CleanPath();
                    //gaa_star_lazy.Solve();

                    //System.Console.WriteLine("GAA*:\n");
                    //System.Console.WriteLine("GAA*:\n" + maze);

                    if (!a_star.HasSolution())
                    {
                        System.Console.WriteLine("No solution.");
                        //System.err.println("Fail: Some algorithms found the solution.");
                        //System.err.println("A*: " + a_star.HasSolution());

                        //System.exit(1);
                        Application.Restart();
                    }
                    else
                    {
                        SuccessCount++;
                        //System.Console.WriteLine(OneSearch + " The solution has the following cost: " + a_star.GetPathCost());
                    }

                    for (int distance = 0; maze_cell != maze.GetGoal(); distance += 1, maze_cell = maze_cell.GetNextMazeCell())
                    {
                        if (distance >= DISTANCE_BEFORE_CHANGE || !a_star.HasSolution())
                        {
                            LightCell new_goal;

                            maze.CleanPath();
                            maze.SetStart(maze_cell);
                            //gaa_star_lazy.InformNewStart(maze_cell);
                            blocked_cells.clear();
                            unblocked_cells.clear();

                            /* Block some cells. */
                            for (int i = 0; i < N_CHANGED_CELLS; i++)
                            {
                                LightCell blocked_maze_cell;
                                int x, y;
                                x = random.nextInt(maze.GetW());
                                y = random.nextInt(maze.GetH());
                                blocked_maze_cell = maze.GetMazeCell(x, y);
                                if (blocked_maze_cell != maze.GetStart() && blocked_maze_cell != maze.GetGoal() && !blocked_maze_cell.IsBlocked() &&
                                        !blocked_cells.contains(blocked_maze_cell))
                                {
                                    blocked_maze_cell.Block();
                                    blocked_cells.add(blocked_maze_cell);
                                }
                            }

                            /* Unblock or change the cost of some cells. */
                            for (int i = 0; i < N_CHANGED_CELLS; i++)
                            {
                                LightCell unblocked_maze_cell;
                                int x, y;
                                x = random.nextInt(maze.GetW());
                                y = random.nextInt(maze.GetH());
                                unblocked_maze_cell = maze.GetMazeCell(x, y);
                                if (!blocked_cells.contains(unblocked_maze_cell) && !unblocked_cells.contains(unblocked_maze_cell))
                                {
                                    int new_cost = random.nextInt(MAZE_CELL_MAX_COST) + 1;
                                    if (unblocked_maze_cell.IsBlocked() || unblocked_maze_cell.GetCost() > new_cost)
                                    {
                                        unblocked_cells.add(unblocked_maze_cell);
                                    }
                                    unblocked_maze_cell.SetCost(new_cost);
                                }
                            }

                            /* Change the goal. */
                            do
                            {
                                int x, y;
                                x = random.nextInt(maze.GetW());
                                y = random.nextInt(maze.GetH());
                                new_goal = maze.GetMazeCell(x, y);
                            } while (blocked_cells.contains(new_goal) || unblocked_cells.contains(new_goal) ||
                                    new_goal == maze.GetGoal() || new_goal == maze.GetStart());

                            if (new_goal.IsBlocked())
                            {
                                unblocked_cells.add(new_goal);
                                maze.SetGoal(new_goal);
                            }
                            else
                            {
                                int old_cost = maze.GetGoal().GetCost();
                                maze.SetGoal(new_goal);
                                if (old_cost > maze.GetGoal().GetCost()) unblocked_cells.add(maze.GetGoal());
                            }

                            //gaa_star_lazy.InformNewGoal(new_goal);

                            //if(unblocked_cells.size() > 0) gaa_star_lazy.InformUnblockedCells(unblocked_cells);
                            break;
                        }
                    }
                }
                TotalSearchs += OneSearch;
                System.Console.WriteLine(c +
                " Goal is : [" + maze.GetGoal().X + ", " + maze.GetGoal().Y +
                "] Current is : [" + maze_cell.X + ", " + maze_cell.Y + "]"
                );
            }//End of produce 100 maze
            TimeSpan diff = (DateTime.Now - t0);
            System.Console.WriteLine("Total RunTime  : " + diff.ToString());
            System.Console.WriteLine("RunTime per search : " + (diff.TotalMilliseconds * 1000 / TotalSearchs) + " micro second");
            System.Console.WriteLine("Average count of search is : " + (TotalSearchs / MazeCount));

        }
    }


}
