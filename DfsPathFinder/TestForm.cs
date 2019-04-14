using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using PathFinder.Heap;
using PathFinder.Algorithm_Test;

using System.Threading;

namespace PathFinder
{
    public partial class TestForm : Form
    {
        int DISTANCE_BEFORE_CHANGE = 10; //10;// -> Step 25;
        int MAZE_W = 300;
        int MAZE_H = 300;
        int N_CHANGED_CELLS = 1; //->k  //(int)Math.round((MAZE_W * MAZE_H) * 0.025);
        int MAZE_CELL_MAX_COST = 1;
        float PROBABILITY_TO_BLOCK_A_CELL = 0.1f;
        TieBreakingStrategy tie_breaking_strategy = TieBreakingStrategy.HIGHEST_G_VALUES;

        Thread PresenterThread;
        TimeSpan Diff;
        DateTime t0 = new DateTime(0);
        int MazeCount = 100;
        int c = 0;
        double TotalTime = 0;

        void SetParameter()
        {
            DISTANCE_BEFORE_CHANGE = (int)numericStep.Value;
            MAZE_W = (int)numericMazeW.Value;
            MAZE_H = (int)numericMazeH.Value; 
            N_CHANGED_CELLS = (int)numericNChange.Value;
            MAZE_CELL_MAX_COST = 1;
            PROBABILITY_TO_BLOCK_A_CELL = 0.1f;
        }

        public TestForm()
        {
            InitializeComponent();
            PresenterThread = new Thread((new ThreadStart(() => { })));
            timer1.Stop();
        }

        private void buttonStartTest_Click(object sender, EventArgs e)
        {
            timer1.Start();
            PresenterThread = new Thread(new ThreadStart(() =>
            {
                SetParameter();
                t0 = DateTime.Now;

                MazeCount = 10;
                int TotalSearchs = 0;
                int SuccessCount = 0;

                for (c = 0; c < MazeCount; c++)
                {
                    int OneSearch = 0;

                    //Hashtable blocked_cells = new Hashtable() , unblocked_cells = new Hashtable();
                    //HashSet blocked_cells = new HashSet(), unblocked_cells = new HashSet();
                    List<LightCell> blocked_cells = new List<LightCell>(), unblocked_cells = new List<LightCell>();
                    
                        //long seed = DateTime.Now.Ticks;//.Millisecond;
                    int seed = DateTime.Now.Millisecond;
                    LightCell maze_cell;
                    //java.util.Random random = new java.util.Random(seed);
                    System.Random random = new System.Random(seed);

                    //richTextBox1.Text += ("Seed: " + seed) + "\n";
                    //richTextBox1.Refresh();
                    SetText(("Seed: " + seed) + "\n", richTextBox1);

                    Maze maze = new Maze(seed, MAZE_W, MAZE_H, PROBABILITY_TO_BLOCK_A_CELL, MAZE_CELL_MAX_COST);
                    //GAAStarLazy gaa_star_lazy = new GAAStarLazy(maze , true , false , tie_breaking_strategy ,
                    //        ManhattanDistanceHeuristic.GetManhattanDistanceHeuristic() , Maze.N_DIRECTIONS_WITHOUT_DIAGONALS);

                    for (maze_cell = maze.GetStart(); maze_cell != maze.GetGoal(); )
                    {
                        AStar a_star = new AStar(maze, true, false, tie_breaking_strategy,
                                ManhattanDistanceHeuristic.GetManhattanDistanceHeuristic(), Maze.N_DIRECTIONS_WITHOUT_DIAGONALS);

                        a_star.Solve();
                        OneSearch++;

                        //System.out.println("A*:\n" + maze);
                        //System.out.println("A*:\n");

                        //maze.CleanPath();
                        //gaa_star_lazy.Solve();

                        //System.out.println("GAA*:\n");
                        //System.out.println("GAA*:\n" + maze);

                        if (!a_star.HasSolution())
                        {
                            //richTextBox1.Text += ("No solution.") + "\n";
                            SetText(("No solution.") + "\n", richTextBox1);

                            //System.err.println("Fail: Some algorithms found the solution.");
                            //System.err.println("A*: " + a_star.HasSolution());

                            //throw new Exception("Error");\                            
                            timer1.Stop();
                            PresenterThread.Abort();
                            //break;
                        }
                        else
                        {
                            SuccessCount++;
                            //System.out.println(OneSearch + " The solution has the following cost: " + a_star.GetPathCost());
                        }

                        for (int distance = 0; maze_cell != maze.GetGoal(); distance += 1, maze_cell = maze_cell.GetNextMazeCell())
                        {
                            if (distance >= DISTANCE_BEFORE_CHANGE || !a_star.HasSolution())
                            {
                                LightCell new_goal;

                                maze.CleanPath();
                                maze.SetStart(maze_cell);
                                //gaa_star_lazy.InformNewStart(maze_cell);
                                blocked_cells.Clear(); //blocked_cells.clear();
                                unblocked_cells.Clear(); //unblocked_cells.clear();

                                if (checkBoxBlockSomeCell.Checked)
                                {
                                    /* Block some cells. */
                                    for (int i = 0; i < N_CHANGED_CELLS; i++)
                                    {
                                        LightCell blocked_maze_cell;
                                        int x, y;
                                        x = random.Next(maze.GetW());//x = random.nextInt(maze.GetW());
                                        y = random.Next(maze.GetH());// y = random.nextInt(maze.GetH());
                                        blocked_maze_cell = maze.GetMazeCell(x, y);
                                        if (blocked_maze_cell != maze.GetStart() && blocked_maze_cell != maze.GetGoal() && !blocked_maze_cell.IsBlocked() &&
                                                !blocked_cells.Contains(blocked_maze_cell))
                                        {
                                            blocked_maze_cell.Block();
                                            blocked_cells.Add(blocked_maze_cell);
                                        }
                                    }
                                }

                                if (checkBoxUnBlock.Checked)
                                {
                                    /* Unblock or change the cost of some cells. */
                                    for (int i = 0; i < N_CHANGED_CELLS; i++)
                                    {
                                        LightCell unblocked_maze_cell;
                                        int x, y;
                                        x = random.Next(maze.GetW());//x = random.nextInt(maze.GetW());
                                        y = random.Next(maze.GetH());// y = random.nextInt(maze.GetH());
                                        unblocked_maze_cell = maze.GetMazeCell(x, y);
                                        if (!blocked_cells.Contains(unblocked_maze_cell) && !unblocked_cells.Contains(unblocked_maze_cell))
                                        {
                                            int new_cost = random.Next(MAZE_CELL_MAX_COST) + 1; //int new_cost = random.nextInt(MAZE_CELL_MAX_COST) + 1;
                                            if (unblocked_maze_cell.IsBlocked() || unblocked_maze_cell.GetCost() > new_cost)
                                            {
                                                unblocked_cells.Add(unblocked_maze_cell);
                                            }
                                            unblocked_maze_cell.SetCost(new_cost);
                                        }
                                    }
                                }

                                if (checkBoxMovingTarget.Checked)
                                {
                                    /* Change the goal. */
                                    do
                                    {
                                        int x, y;
                                        x = random.Next(maze.GetW());//x = random.nextInt(maze.GetW());
                                        y = random.Next(maze.GetH());// y = random.nextInt(maze.GetH());
                                        new_goal = maze.GetMazeCell(x, y);
                                    } while (blocked_cells.Contains(new_goal) || unblocked_cells.Contains(new_goal) ||
                                            new_goal == maze.GetGoal() || new_goal == maze.GetStart());

                                    if (new_goal.IsBlocked())
                                    {
                                        unblocked_cells.Add(new_goal);
                                        maze.SetGoal(new_goal);
                                    }
                                    else
                                    {
                                        int old_cost = maze.GetGoal().GetCost();
                                        maze.SetGoal(new_goal);
                                        if (old_cost > maze.GetGoal().GetCost()) 
                                            unblocked_cells.Add(maze.GetGoal());
                                    }
                                }
                                //gaa_star_lazy.InformNewGoal(new_goal);

                                //if(unblocked_cells.size() > 0) gaa_star_lazy.InformUnblockedCells(unblocked_cells);
                                break;
                            }
                        }
                    }
                    TotalSearchs += OneSearch;
                    /*richTextBox1.Text += c +
                    " Goal is : [" + maze.GetGoal().X + ", " + maze.GetGoal().Y +
                    "] Current is : [" + maze_cell.X + ", " + maze_cell.Y + "] \n";
                    richTextBox1.Refresh();*/

                    SetText("Maze " + (c+1) + " Goal is : [" + maze.GetGoal().X + ", " + maze.GetGoal().Y +
                    "] Current is : [" + maze_cell.X + ", " + maze_cell.Y + "] \n", richTextBox1);

                }//End of produce 100 maze
                //richTextBox1.Text += ("Average count of search is : " + (TotalSearchs / MazeCount));
                //richTextBox1.Refresh();
                SetText(("Average count of search is : " + TotalSearchs + "/" + MazeCount + 
                         " = " + (TotalSearchs / MazeCount)), richTextBox1);

                Diff = (DateTime.Now - t0);
                SetText("\n" + Diff.ToString() + "\n", richTextBox1);
                SetText("RunTime per each search : " + (Diff.TotalMilliseconds * 1000 / TotalSearchs) + " micro second \n", richTextBox1);

                timer1.Stop();
            }));
            PresenterThread.Start();

            //richTextBox1.Text = (DateTime.Now - t0).ToString();
            //PresenterThread.Abort();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (PresenterThread.ThreadState == ThreadState.Running)
                toolStripTime.Text = "" + (DateTime.Now - t0).TotalSeconds;
            progressBar1.Maximum = MazeCount;
            //if (progressBar1.Value <= progressBar1.Maximum)
            if (MazeCount > c)
                progressBar1.Value = c;
            if (MazeCount == c)
                progressBar1.Value = c + 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*PresenterThread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    Thread.Sleep(1);
                    SetText(DateTime.Now.ToString(), this);
                }
            }));
            PresenterThread.Start();*/
            progressBar1.Value = 0;
            richTextBox1.Clear();
        }

        #region HandleCrossThread

        delegate void SetTextCallback(string text, Control c);

        void SetText(string text, Control c)
        {
            if (c.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text, c});
            }
            else
            {
                //Diff += (EndTime - StartTime);
                c.Text += text;// Diff.ToString();//.AppendText(text);
            }
        }

        #endregion

        private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PresenterThread.Abort();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            comboTieBreaking.SelectedIndex = 0;
        }

        private void comboTieBreaking_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboTieBreaking.SelectedIndex)
            {
                case 0:
                    tie_breaking_strategy = TieBreakingStrategy.HIGHEST_G_VALUES;
                    break;
                case 1:
                    tie_breaking_strategy = TieBreakingStrategy.SMALLEST_G_VALUES;
                    break;
                case 2:
                    tie_breaking_strategy = TieBreakingStrategy.NONE;
                    break;
            }
        }

        private void buttonGAATest_Click(object sender, EventArgs e)
        {
            timer1.Start();
            PresenterThread = new Thread(new ThreadStart(() =>
            {
                SetParameter();
                t0 = DateTime.Now;

                MazeCount = 100;
                int TotalSearchs = 0;
                int SuccessCount = 0;

                for (c = 0; c < MazeCount; c++)
                {
                    int OneSearch = 0;

                    List<LightCell> blocked_cells = new List<LightCell>(), 
                                    unblocked_cells = new List<LightCell>();

                    int seed = DateTime.Now.Millisecond;
                    LightCell maze_cell;
                    System.Random random = new System.Random(seed);

                    SetText(("Seed: " + seed) + "\n", richTextBox1);

                    Maze maze = new Maze(seed, MAZE_W, MAZE_H, PROBABILITY_TO_BLOCK_A_CELL, MAZE_CELL_MAX_COST);
                    GAAStarLazy gaa_star_lazy = new GAAStarLazy(maze , true , false , tie_breaking_strategy ,
                            ManhattanDistanceHeuristic.GetManhattanDistanceHeuristic() , Maze.N_DIRECTIONS_WITHOUT_DIAGONALS);

                    for (maze_cell = maze.GetStart(); maze_cell != maze.GetGoal(); )
                    {
                        //AStar a_star = new AStar(maze, true, false, tie_breaking_strategy,
                        //        ManhattanDistanceHeuristic.GetManhattanDistanceHeuristic(), Maze.N_DIRECTIONS_WITHOUT_DIAGONALS);

                        //a_star.Solve();
                        
                        OneSearch++;

                        //System.out.println("A*:\n" + maze);
                        //System.out.println("A*:\n");

                        //maze.CleanPath();
                        gaa_star_lazy.Solve();

                        //System.out.println("GAA*:\n");
                        //System.out.println("GAA*:\n" + maze);

                        if (!gaa_star_lazy.HasSolution())
                        {
                            //richTextBox1.Text += ("No solution.") + "\n";
                            SetText(("No solution.") + "\n", richTextBox1);

                            //System.err.println("Fail: Some algorithms found the solution.");
                            //System.err.println("A*: " + a_star.HasSolution());

                            //throw new Exception("Error");\                            
                            timer1.Stop();
                            PresenterThread.Abort();
                            //break;
                        }
                        else
                        {
                            SuccessCount++;
                            //System.out.println(OneSearch + " The solution has the following cost: " + a_star.GetPathCost());
                        }

                        for (int distance = 0; maze_cell != maze.GetGoal(); distance += 1, maze_cell = maze_cell.GetNextMazeCell())
                        {
                            if (distance >= DISTANCE_BEFORE_CHANGE || !gaa_star_lazy.HasSolution())
                            {
                                //LightCell new_goal = new LightCell();

                                maze.CleanPath();
                                maze.SetStart(maze_cell);
                                gaa_star_lazy.InformNewStart(maze_cell);
                                blocked_cells.Clear(); //blocked_cells.clear();
                                unblocked_cells.Clear(); //unblocked_cells.clear();

                                if (checkBoxBlockSomeCell.Checked)
                                {
                                    /* Block some cells. */
                                    for (int i = 0; i < N_CHANGED_CELLS; i++)
                                    {
                                        LightCell blocked_maze_cell;
                                        int x, y;
                                        x = random.Next(maze.GetW());//x = random.nextInt(maze.GetW());
                                        y = random.Next(maze.GetH());// y = random.nextInt(maze.GetH());
                                        blocked_maze_cell = maze.GetMazeCell(x, y);
                                        if (blocked_maze_cell != maze.GetStart() && blocked_maze_cell != maze.GetGoal() && !blocked_maze_cell.IsBlocked() &&
                                                !blocked_cells.Contains(blocked_maze_cell))
                                        {
                                            blocked_maze_cell.Block();
                                            blocked_cells.Add(blocked_maze_cell);
                                        }
                                    }
                                }

                                if (checkBoxUnBlock.Checked)
                                {
                                    /* Unblock or change the cost of some cells. */
                                    for (int i = 0; i < N_CHANGED_CELLS; i++)
                                    {
                                        LightCell unblocked_maze_cell;
                                        int x, y;
                                        x = random.Next(maze.GetW());//x = random.nextInt(maze.GetW());
                                        y = random.Next(maze.GetH());// y = random.nextInt(maze.GetH());
                                        unblocked_maze_cell = maze.GetMazeCell(x, y);
                                        if (!blocked_cells.Contains(unblocked_maze_cell) && !unblocked_cells.Contains(unblocked_maze_cell))
                                        {
                                            int new_cost = random.Next(MAZE_CELL_MAX_COST) + 1; //int new_cost = random.nextInt(MAZE_CELL_MAX_COST) + 1;
                                            if (unblocked_maze_cell.IsBlocked() || unblocked_maze_cell.GetCost() > new_cost)
                                            {
                                                unblocked_cells.Add(unblocked_maze_cell);
                                            }
                                            unblocked_maze_cell.SetCost(new_cost);
                                        }
                                    }
                                }

                                if (checkBoxMovingTarget.Checked)
                                {
                                    /* //Change the goal
                                    do
                                    {
                                        int x, y;
                                        x = random.Next(maze.GetW());//x = random.nextInt(maze.GetW());
                                        y = random.Next(maze.GetH());// y = random.nextInt(maze.GetH());
                                        new_goal = maze.GetMazeCell(x, y);
                                    } while (blocked_cells.Contains(new_goal) || unblocked_cells.Contains(new_goal) ||
                                            new_goal == maze.GetGoal() || new_goal == maze.GetStart());

                                    if (new_goal.IsBlocked())
                                    {
                                        unblocked_cells.Add(new_goal);
                                        maze.SetGoal(new_goal);
                                    }
                                    else
                                    {
                                        int old_cost = maze.GetGoal().GetCost();
                                        maze.SetGoal(new_goal);
                                        if (old_cost > maze.GetGoal().GetCost())
                                            unblocked_cells.Add(maze.GetGoal());
                                    }

                                    gaa_star_lazy.InformNewGoal(new_goal);
                                    */
                                }
                                if(unblocked_cells.Count > 0) 
                                    gaa_star_lazy.InformUnblockedCells(unblocked_cells);
                                break;
                            }
                        }
                    }
                    TotalSearchs += OneSearch;
                    /*richTextBox1.Text += c +
                    " Goal is : [" + maze.GetGoal().X + ", " + maze.GetGoal().Y +
                    "] Current is : [" + maze_cell.X + ", " + maze_cell.Y + "] \n";
                    richTextBox1.Refresh();*/

                    SetText("Maze " + (c + 1) + " Goal is : [" + maze.GetGoal().X + ", " + maze.GetGoal().Y +
                    "] Current is : [" + maze_cell.X + ", " + maze_cell.Y + "] \n", richTextBox1);

                }//End of produce 100 maze
                //richTextBox1.Text += ("Average count of search is : " + (TotalSearchs / MazeCount));
                //richTextBox1.Refresh();
                SetText(("Average count of search is : " + TotalSearchs + "/" + MazeCount +
                         " = " + (TotalSearchs / MazeCount)), richTextBox1);

                Diff = (DateTime.Now - t0);
                SetText("\n" + Diff.ToString() + "\n", richTextBox1);
                SetText("RunTime per each search : " + (Diff.TotalMilliseconds * 1000 / TotalSearchs) + " micro second \n", richTextBox1);

                timer1.Stop();
            }));
            PresenterThread.Start();
        }

        private void buttonBfsTest_Click(object sender, EventArgs e)
        {
            timer1.Start();
            PresenterThread = new Thread(new ThreadStart(() =>
            {
                SetParameter();
                t0 = DateTime.Now;

                MazeCount = 1;
                int TotalSearchs = 0;
                int SuccessCount = 0;

                for (c = 0; c < MazeCount; c++)
                {
                    int OneSearch = 0;

                    List<LightCell> blocked_cells = new List<LightCell>(), unblocked_cells = new List<LightCell>();
                    int seed = DateTime.Now.Millisecond;
                    LightCell maze_cell;

                    System.Random random = new System.Random(seed);
                    SetText(("Seed: " + seed) + "\n", richTextBox1);

                    Maze maze = new Maze(seed, MAZE_W, MAZE_H, PROBABILITY_TO_BLOCK_A_CELL, MAZE_CELL_MAX_COST);
                    BFS bfs = new BFS(maze, false, false);

                    for (maze_cell = maze.GetStart(); maze_cell != maze.GetGoal(); )
                    //for (maze_cell = bfs.path[0]; maze_cell != bfs.path[bfs.path.Count - 1]; )
                    {
                        bfs = new BFS(maze, false, false);
                        bfs.Solve2();

                        OneSearch++;

                        //System.out.println("A*:\n" + maze);
                        //System.out.println("A*:\n");

                        //maze.CleanPath();
                        //gaa_star_lazy.Solve();

                        //System.out.println("GAA*:\n");
                        //System.out.println("GAA*:\n" + maze);

                        if (!bfs.HasSolution())
                        {
                            //richTextBox1.Text += ("No solution.") + "\n";
                            SetText(("No solution.") + "\n", richTextBox1);

                            //System.err.println("Fail: Some algorithms found the solution.");
                            //System.err.println("A*: " + a_star.HasSolution());

                            //throw new Exception("Error");\                            
                            timer1.Stop();
                            PresenterThread.Abort();
                            //break;
                        }
                        else
                        {
                            SuccessCount++;
                            //System.out.println(OneSearch + " The solution has the following cost: " + a_star.GetPathCost());
                        }

                        for (int distance = 0; maze_cell != maze.GetGoal(); distance += 1, maze_cell = maze_cell.GetNextMazeCell())
                        //for (int distance = 0; maze_cell != bfs.path[bfs.path.Count - 1]; distance += 1, maze_cell = maze_cell.GetNextMazeCell())
                        {
                            if (distance >= DISTANCE_BEFORE_CHANGE || !bfs.HasSolution())
                            {
                                LightCell new_goal;

                                maze.CleanPath();
                                bfs.path.Clear();

                                maze.SetStart(maze_cell);
                                //gaa_star_lazy.InformNewStart(maze_cell);
                                blocked_cells.Clear(); //blocked_cells.clear();
                                unblocked_cells.Clear(); //unblocked_cells.clear();

                                if (checkBoxBlockSomeCell.Checked)
                                {
                                    /* Block some cells. */
                                    for (int i = 0; i < N_CHANGED_CELLS; i++)
                                    {
                                        LightCell blocked_maze_cell;
                                        int x, y;
                                        x = random.Next(maze.GetW());//x = random.nextInt(maze.GetW());
                                        y = random.Next(maze.GetH());// y = random.nextInt(maze.GetH());
                                        blocked_maze_cell = maze.GetMazeCell(x, y);
                                        if (blocked_maze_cell != maze.GetStart() && blocked_maze_cell != maze.GetGoal() && !blocked_maze_cell.IsBlocked() &&
                                                !blocked_cells.Contains(blocked_maze_cell))
                                        {
                                            blocked_maze_cell.Block();
                                            blocked_cells.Add(blocked_maze_cell);
                                        }
                                    }
                                }

                                if (checkBoxUnBlock.Checked)
                                {
                                    /* Unblock or change the cost of some cells. */
                                    for (int i = 0; i < N_CHANGED_CELLS; i++)
                                    {
                                        LightCell unblocked_maze_cell;
                                        int x, y;
                                        x = random.Next(maze.GetW());//x = random.nextInt(maze.GetW());
                                        y = random.Next(maze.GetH());// y = random.nextInt(maze.GetH());
                                        unblocked_maze_cell = maze.GetMazeCell(x, y);
                                        if (!blocked_cells.Contains(unblocked_maze_cell) && !unblocked_cells.Contains(unblocked_maze_cell))
                                        {
                                            int new_cost = random.Next(MAZE_CELL_MAX_COST) + 1; //int new_cost = random.nextInt(MAZE_CELL_MAX_COST) + 1;
                                            if (unblocked_maze_cell.IsBlocked() || unblocked_maze_cell.GetCost() > new_cost)
                                            {
                                                unblocked_cells.Add(unblocked_maze_cell);
                                            }
                                            unblocked_maze_cell.SetCost(new_cost);
                                        }
                                    }
                                }

                                if (checkBoxMovingTarget.Checked)
                                {
                                    /* Change the goal. */
                                    do
                                    {
                                        int x, y;
                                        x = random.Next(maze.GetW());//x = random.nextInt(maze.GetW());
                                        y = random.Next(maze.GetH());// y = random.nextInt(maze.GetH());
                                        new_goal = maze.GetMazeCell(x, y);
                                    } while (blocked_cells.Contains(new_goal) || unblocked_cells.Contains(new_goal) ||
                                            new_goal == maze.GetGoal() || new_goal == maze.GetStart());

                                    if (new_goal.IsBlocked())
                                    {
                                        unblocked_cells.Add(new_goal);
                                        maze.SetGoal(new_goal);
                                    }
                                    else
                                    {
                                        int old_cost = maze.GetGoal().GetCost();
                                        maze.SetGoal(new_goal);
                                        if (old_cost > maze.GetGoal().GetCost())
                                            unblocked_cells.Add(maze.GetGoal());
                                    }
                                }
                                //gaa_star_lazy.InformNewGoal(new_goal);

                                //if(unblocked_cells.size() > 0) gaa_star_lazy.InformUnblockedCells(unblocked_cells);
                                break;
                            }
                        }
                    }
                    TotalSearchs += OneSearch;
                    /*richTextBox1.Text += c +
                    " Goal is : [" + maze.GetGoal().X + ", " + maze.GetGoal().Y +
                    "] Current is : [" + maze_cell.X + ", " + maze_cell.Y + "] \n";
                    richTextBox1.Refresh();*/

                    SetText("Maze " + (c + 1) + " Goal is : [" + maze.GetGoal().X + ", " + maze.GetGoal().Y +
                    "] Current is : [" + maze_cell.X + ", " + maze_cell.Y + "] \n", richTextBox1);

                }//End of produce 100 maze
                //richTextBox1.Text += ("Average count of search is : " + (TotalSearchs / MazeCount));
                //richTextBox1.Refresh();
                SetText(("Average count of search is : " + TotalSearchs + "/" + MazeCount +
                         " = " + (TotalSearchs / MazeCount)), richTextBox1);

                Diff = (DateTime.Now - t0);
                SetText("\n" + Diff.ToString() + "\n", richTextBox1);
                SetText("RunTime per each search : " + (Diff.TotalMilliseconds * 1000 / TotalSearchs) + " micro second \n", richTextBox1);

                timer1.Stop();
            }));
            PresenterThread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
            PresenterThread = new Thread(new ThreadStart(() =>
            {
                SetParameter();
                t0 = DateTime.Now;

                MazeCount = 10;
                int SuccessCount = 0;

                for (c = 0; c < MazeCount; c++)
                {
                    List<LightCell> blocked_cells = new List<LightCell>(), unblocked_cells = new List<LightCell>();
                    
                    int seed = DateTime.Now.Millisecond;
                    System.Random random = new System.Random(seed);
                    SetText(("Seed: " + seed) + "\n", richTextBox1);

                    Maze maze = new Maze(seed, MAZE_W, MAZE_H, PROBABILITY_TO_BLOCK_A_CELL, MAZE_CELL_MAX_COST);
                    BFS bfs = new BFS(maze, false, false);
                    bfs.Solve();

                    if (bfs.HasSolution())
                    {
                        SuccessCount++;
                    }
                    else
                    {
                        timer1.Stop();
                        PresenterThread.Abort();
                    }
                }
                timer1.Stop();
                /*SetText(("Average count of search is : " + TotalSearchs + "/" + MazeCount +
                         " = " + (TotalSearchs / MazeCount)), richTextBox1);*/

                Diff = (DateTime.Now - t0);
                SetText("\n Total Time : " + Diff.ToString() + "\n", richTextBox1);
                SetText("RunTime per each search : " + (Diff.TotalMilliseconds * 1000 / SuccessCount) + " micro second \n", richTextBox1);

            }));
            PresenterThread.Start();
        }

        private void buttonTestRuntimeAStar_Click(object sender, EventArgs e)
        {
            timer1.Start();
            PresenterThread = new Thread(new ThreadStart(() =>
            {
                SetParameter();
                t0 = DateTime.Now;

                MazeCount = 10;
                int SuccessCount = 0;

                for (c = 0; c < MazeCount; c++)
                {
                    List<LightCell> blocked_cells = new List<LightCell>(), unblocked_cells = new List<LightCell>();

                    int seed = DateTime.Now.Millisecond;
                    System.Random random = new System.Random(seed);
                    SetText(("Seed: " + seed) + "\n", richTextBox1);

                    Maze maze = new Maze(seed, MAZE_W, MAZE_H, PROBABILITY_TO_BLOCK_A_CELL, MAZE_CELL_MAX_COST);
                    AStar a_star = new AStar(maze, true, false, tie_breaking_strategy,
                            ManhattanDistanceHeuristic.GetManhattanDistanceHeuristic(), Maze.N_DIRECTIONS_WITHOUT_DIAGONALS);

                    a_star.Solve();

                    if (a_star.HasSolution())
                    {
                        SuccessCount++;
                    }
                    else
                    {
                        timer1.Stop();
                        PresenterThread.Abort();
                    }
                }
                timer1.Stop();
                /*SetText(("Average count of search is : " + TotalSearchs + "/" + MazeCount +
                         " = " + (TotalSearchs / MazeCount)), richTextBox1);*/

                Diff = (DateTime.Now - t0);
                SetText("\n Total Time : " + Diff.ToString() + "\n", richTextBox1);
                SetText("RunTime per each search : " + (Diff.TotalMilliseconds * 1000 / SuccessCount) + " micro second \n", richTextBox1);

            }));
            PresenterThread.Start();
        }

        private void buttonTestRuntimeGAA_Click(object sender, EventArgs e)
        {
            timer1.Start();
            PresenterThread = new Thread(new ThreadStart(() =>
            {
                SetParameter();
                t0 = DateTime.Now;

                MazeCount = 10;
                int SuccessCount = 0;

                for (c = 0; c < MazeCount; c++)
                {
                    List<LightCell> blocked_cells = new List<LightCell>(), unblocked_cells = new List<LightCell>();

                    int seed = DateTime.Now.Millisecond;
                    System.Random random = new System.Random(seed);
                    SetText(("Seed: " + seed) + "\n", richTextBox1);

                    Maze maze = new Maze(seed, MAZE_W, MAZE_H, PROBABILITY_TO_BLOCK_A_CELL, MAZE_CELL_MAX_COST);
                    GAAStarLazy gaa_star = new GAAStarLazy(maze, true, false, tie_breaking_strategy,
                            ManhattanDistanceHeuristic.GetManhattanDistanceHeuristic(), Maze.N_DIRECTIONS_WITHOUT_DIAGONALS);

                    gaa_star.Solve();

                    if (gaa_star.HasSolution())
                    {
                        SuccessCount++;
                    }
                    else
                    {
                        timer1.Stop();
                        PresenterThread.Abort();
                    }
                }
                timer1.Stop();
                /*SetText(("Average count of search is : " + TotalSearchs + "/" + MazeCount +
                         " = " + (TotalSearchs / MazeCount)), richTextBox1);*/

                Diff = (DateTime.Now - t0);
                SetText("\n Total Time : " + Diff.ToString() + "\n", richTextBox1);
                SetText("RunTime per each search : " + (Diff.TotalMilliseconds * 1000 / SuccessCount) + " micro second \n", richTextBox1);

            }));
            PresenterThread.Start();
        }

        private void buttonTestRuntimeDFS_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                "الگوریتم اول عمق زمان به زمان زیادی احتیاج دارد. آیا الگوریتم اجرا شود؟",
                "زمان بر بودن الگوریتم",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                timer1.Start();
                PresenterThread = new Thread(new ThreadStart(() =>
                {
                    SetParameter();
                    t0 = DateTime.Now;

                    MazeCount = 10;
                    int SuccessCount = 0;

                    for (c = 0; c < MazeCount; c++)
                    {
                        List<LightCell> blocked_cells = new List<LightCell>(), unblocked_cells = new List<LightCell>();

                        int seed = DateTime.Now.Millisecond;
                        System.Random random = new System.Random(seed);
                        SetText(("Seed: " + seed) + "\n", richTextBox1);

                        Maze maze = new Maze(seed, MAZE_W, MAZE_H, PROBABILITY_TO_BLOCK_A_CELL, MAZE_CELL_MAX_COST);
                        DFS dfs = new DFS(maze, false, false);
                        dfs.Solve();

                        if (dfs.HasSolution())
                        {
                            SuccessCount++;
                        }
                        else
                        {
                            timer1.Stop();
                            PresenterThread.Abort();
                        }
                    }
                    timer1.Stop();
                    /*SetText(("Average count of search is : " + TotalSearchs + "/" + MazeCount +
                             " = " + (TotalSearchs / MazeCount)), richTextBox1);*/

                    Diff = (DateTime.Now - t0);
                    SetText("\n Total Time : " + Diff.ToString() + "\n", richTextBox1);
                    SetText("RunTime per each search : " + (Diff.TotalMilliseconds * 1000 / SuccessCount) + " micro second \n", richTextBox1);

                }));
                PresenterThread.Start();
            }
        }

        private void buttonTestRuntimeDLite_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                "این پیاده سازی از این الگوریتم به زمان زیادی احتیاج دارد. آیا الگوریتم اجرا شود؟",
                "زمان بر بودن الگوریتم",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                SetParameter();
                timer1.Start();
                PresenterThread = new Thread(new ThreadStart(() =>
                {
                    t0 = DateTime.Now;

                    MazeCount = 5;
                    int SuccessCount = 0;

                    for (c = 0; c < MazeCount; c++)
                    {
                        int seed = DateTime.Now.Millisecond;
                        System.Random random = new System.Random(seed);
                        SetText(("Seed: " + seed) + "\n", richTextBox1);

                        Algorithm_Test.DLite.Maze maze =
                            new PathFinder.Algorithm_Test.DLite.Maze(DateTime.Now.Millisecond, MAZE_W, MAZE_H);

                        maze.Initialize();

                        //D*Lie solve
                        maze.Calculate_path(false);

                        if (maze.HasSolution())
                        {
                            SuccessCount++;
                        }
                        else
                        {
                            timer1.Stop();
                            SetText("\n No Solution", richTextBox1);
                            PresenterThread.Abort();
                        }
                    }
                    timer1.Stop();
                    /*SetText(("Average count of search is : " + TotalSearchs + "/" + MazeCount +
                             " = " + (TotalSearchs / MazeCount)), richTextBox1);*/

                    Diff = (DateTime.Now - t0);
                    SetText("\n Total Time : " + Diff.ToString() + "\n", richTextBox1);
                    SetText("RunTime per each search : " + (Diff.TotalMilliseconds * 1000 / SuccessCount) + " micro second \n", richTextBox1);

                }));
                PresenterThread.Start();
            }
        }

        private void buttonTestRuntimeDLite_Heap_Click(object sender, EventArgs e)
        {
            SetParameter();
            timer1.Start();
            PresenterThread = new Thread(new ThreadStart(() =>
            {
                t0 = DateTime.Now;

                MazeCount = 5;
                int SuccessCount = 0;

                for (c = 0; c < MazeCount; c++)
                {
                    int seed = DateTime.Now.Millisecond;
                    System.Random random = new System.Random(seed);
                    SetText(("Seed: " + seed) + "\n", richTextBox1);

                    Algorithm_Test.DLite_Heap.Maze maze =
                        new PathFinder.Algorithm_Test.DLite_Heap.Maze(DateTime.Now.Millisecond, MAZE_W, MAZE_H);

                    maze.Initialize();

                    //D*Lie solve
                    maze.Calculate_path(false);

                    if (maze.HasSolution())
                    {
                        SuccessCount++;
                    }
                    else
                    {
                        timer1.Stop();
                        PresenterThread.Abort();
                    }
                }
                timer1.Stop();
                /*SetText(("Average count of search is : " + TotalSearchs + "/" + MazeCount +
                         " = " + (TotalSearchs / MazeCount)), richTextBox1);*/

                Diff = (DateTime.Now - t0);
                SetText("\n Total Time : " + Diff.ToString() + "\n", richTextBox1);
                SetText("RunTime per each search : " + (Diff.TotalMilliseconds * 1000 / SuccessCount) + " micro second \n", richTextBox1);

            }));
            PresenterThread.Start();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (PresenterThread.ThreadState == ThreadState.Running)
            {
                PresenterThread.Abort();
                richTextBox1.Text += "\n" + "Aborted";
                timer1.Stop();
            }
            else if (PresenterThread.ThreadState == ThreadState.Unstarted)
            {
                MessageBox.Show("اجرای الگوریتم هنوز شروع نشده است");
            }
            else
            {
                MessageBox.Show("الگوریتم قبلاً متوقف شده است");
            }
        }

        /*private void buttonContinue_Click(object sender, EventArgs e)
        {
            if (PresenterThread.ThreadState == ThreadState.Running)
            {
                PresenterThread.Suspend();
                richTextBox1.Text += "\n" + "Stoped";
                timer1.Stop();
                buttonContinue.Text = "ادامه اجرا";
            }
            else if (PresenterThread.ThreadState == ThreadState.Suspended)
            {
                PresenterThread.Resume();
                timer1.Start();
                buttonContinue.Text = "توقف اجرا";
            }
            else if (PresenterThread.ThreadState == ThreadState.Unstarted)
            {
                MessageBox.Show("اجرای الگوریتم هنوز شروع نشده است");
            }
            else
            {
                MessageBox.Show("الگوریتم قبلاً متوقف شده است");
            }
        }*/


    }
}
