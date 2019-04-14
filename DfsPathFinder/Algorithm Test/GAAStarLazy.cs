using System;
using System.Collections.Generic;
using System.Text;

using PathFinder.Heap;
using System.Collections;
using System.Diagnostics;

namespace PathFinder.Algorithm_Test
{
    class GAAStarLazy
    {
    	public enum PieceOfPseudoCode
        {
		    PIECE_12_21, PIECE_3A_9A, PIECE_10A_17A, Null
            //"12 - 21" , "3' - 9'" , "10' - 17'"
	    }

        private string PieceOfPseudoCodeToString(PieceOfPseudoCode piece)
        {
            if (piece == PieceOfPseudoCode.PIECE_10A_17A)
                return "12 - 21";
            else if (piece == PieceOfPseudoCode.PIECE_12_21)
                return "3' - 9'";
            else if (piece == PieceOfPseudoCode.PIECE_3A_9A)
                return "10' - 17'";
            else
                return "";
        }

        private static int INFINITY_INT = 0x7FFFFFFF;
        private static String INFINITY_STR = Convert.ToString((char)0x221E);

        private int w , h , path_cost_last_execution , neighborhood , n_searches;
        private bool has_solution, mark_path , step_by_step , has_execution_finished;
        private GAAStarNode [,]graph ;
        private GAAStarNode goal , start , new_goal;
        private BinaryHeap open_list;
        //private HashSet<MazeLightCell> unblocked_cells;
        private List<LightCell> unblocked_cells;
        private Heuristic heuristic;
        private List<int> path_cost , delta_h;
        private PieceOfPseudoCode piece_of_pseudo_code_being_executed;

        private void InitializeNode(GAAStarNode node)
        {
            Debug.Assert(node.search <= n_searches);
            if (node.search != n_searches && node.search != 0)
            {
                Debug.Assert(path_cost.Count > node.search && node.h != INFINITY_INT);
                if (node.g != INFINITY_INT && node.g + node.h < path_cost[node.search])
                {
                    node.h = path_cost[node.search] - node.g;
                }

                Debug.Assert(delta_h.Count > n_searches && delta_h.Count > node.search);
                int aux = node.h - (delta_h[n_searches] - delta_h[node.search]);

                node.h = Math.Max(heuristic.DistanceToGoal(node.GetMazeLightCell(), goal.GetMazeLightCell()), (aux < 0 ? 0 : aux));
                Debug.Assert(node.h >= 0);

                node.g = INFINITY_INT;
            }
            else if (node.search == 0)
            {
                node.g = INFINITY_INT;
                node.h = heuristic.DistanceToGoal(node.GetMazeLightCell(), goal.GetMazeLightCell());
            }
            node.search = n_searches;
        }

        public GAAStarLazy(Maze maze , bool mark_path , bool step_by_step  , 
            TieBreakingStrategy tie_breaking_strategy ,Heuristic heuristic , int neighborhood)
        {

		    h = maze.GetH();
		    w = maze.GetW();
		    open_list = new BinaryHeap(w * h);

		    graph = new GAAStarNode[h, w];
		    for(int y = 0 ; y < h ; y++)
            {
			    for(int x = 0 ; x < w ; x++)
                {
				    graph[y, x] = new GAAStarNode(maze.GetMazeCell(x , y) , tie_breaking_strategy);
			        //مقدار کشف کننده فعلاً تعیین نمیشود
                }
		    }

		    this.heuristic = heuristic;
		    this.mark_path = mark_path;
		    this.step_by_step = step_by_step;
		    this.neighborhood = neighborhood;
		    goal = graph[maze.GetGoal().Y, maze.GetGoal().X];
		    start = graph[maze.GetStart().Y, maze.GetStart().X];
		    new_goal = null;
		    path_cost_last_execution = n_searches = 0;
		    unblocked_cells = null;
            piece_of_pseudo_code_being_executed = PieceOfPseudoCode.Null;// null;

		    delta_h = new List<int>();
		    path_cost = new List<int>();
		    delta_h.Add(0); /* delta_h(0) := 0 */
		    path_cost.Add(0); /* path_cost(0) := 0 */
	    }

        public int GetMazeLightCellG(LightCell maze_cell)
        {
            return graph[maze_cell.Y, maze_cell.X].g;
        }

        public int GetMazeLightCellH(LightCell maze_cell)
        {
            return graph[maze_cell.Y, maze_cell.X].h;
        }

        public String GetOpenListText()
        {
            String s = "";

            for (int i = 0; i < open_list.Size(); i++)
            {
                s += ((GAAStarNode)open_list.GetElement(i)).ToString() + "\n";
            }

            return s;
        }

        public String GetPieceOfPseudoCodeBeingExecuted()
        {
            Debug.Assert(piece_of_pseudo_code_being_executed != PieceOfPseudoCode.Null);
            return PieceOfPseudoCodeToString(piece_of_pseudo_code_being_executed);
        }

        public int GetPathCost()
        {
            return path_cost_last_execution;
        }

        public bool HasBeenVisitedinLastExecution(LightCell maze_cell)
        {
            return graph[maze_cell.Y, maze_cell.X].search == n_searches;
        }

        public bool HasExecutionFinished()
        {
            return has_execution_finished;
        }

        public bool HasSolution()
        {
            return has_solution;
        }

        public void SetStepByStep(bool step_by_step)
        {
            this.step_by_step = step_by_step;
        }

        public void InformNewGoal(LightCell new_goal)
        {
            this.new_goal = graph[new_goal.Y, new_goal.X];
        }

        public void InformNewStart(LightCell new_start)
        {
            start = graph[new_start.Y, new_start.X];
        }

        public void InformUnblockedCells(List<LightCell> unblocked_cells)//(HashSet<LightCell> unblocked_cells)
        {
            this.unblocked_cells = unblocked_cells;
        }

        public bool IsInOpenList(LightCell maze_cell)
        {
            return open_list.Has(graph[maze_cell.Y, maze_cell.X]);
        }

        public void Solve()
        {
            has_solution = false;
            has_execution_finished = false;

            Debug.Assert(path_cost_last_execution != INFINITY_INT);

            {
                /* If the goal changed update the heuristic. */
                if (piece_of_pseudo_code_being_executed == PieceOfPseudoCode.Null)
                {
                    if (new_goal != null)
                    {
                        Debug.Assert(path_cost.Count > n_searches);
                        InitializeNode(new_goal);
                        if (new_goal.g != INFINITY_INT && new_goal.g + new_goal.h < path_cost[n_searches])
                        {
                            new_goal.h = path_cost[n_searches] - new_goal.g;
                        }
                        delta_h.Add(delta_h[n_searches] + new_goal.h);
                        goal = new_goal;
                        new_goal = null;
                    }
                    else
                    {
                        delta_h.Add(delta_h[n_searches]);
                    }
                    n_searches++;
                    open_list.Clear();
                }

                /* If some cell became unblocked. */
                if (unblocked_cells != null)
                {
                    Debug.Assert(piece_of_pseudo_code_being_executed == PieceOfPseudoCode.Null ||
                        piece_of_pseudo_code_being_executed == PieceOfPseudoCode.PIECE_3A_9A ||
                        piece_of_pseudo_code_being_executed == PieceOfPseudoCode.PIECE_10A_17A);
                    if (piece_of_pseudo_code_being_executed != PieceOfPseudoCode.PIECE_10A_17A)
                    {
                        piece_of_pseudo_code_being_executed = PieceOfPseudoCode.PIECE_3A_9A;

                        for (IEnumerator i = unblocked_cells.GetEnumerator(); i.MoveNext() != false; )
                        //for (Iterator<MazeLightCell> i = unblocked_cells.iterator(); i.hasNext(); )
                        {
                            LightCell maze_cell = (LightCell)i.Current;//i.next();
                                //i.remove();
                            GAAStarNode node = graph[maze_cell.Y, maze_cell.X];
                            InitializeNode(node);
                            int cost = node.GetMazeLightCell().GetCost();
                            Debug.Assert(!node.GetMazeLightCell().IsBlocked());

                            for (int j = 0; j < neighborhood; j++)
                            {
                                int x, y;
                                x = node.GetMazeLightCell().X + Maze.delta_x[j];
                                y = node.GetMazeLightCell().Y + Maze.delta_y[j];
                                if (0 <= x && x < w && 0 <= y && y < h)
                                {
                                    GAAStarNode predecessor = graph[y, x];
                                    InitializeNode(predecessor);

                                    if (predecessor.h > cost + node.h)
                                    {
                                        predecessor.h = cost + node.h;
                                        predecessor.f = predecessor.h;
                                        open_list.Insert(predecessor);
                                    }
                                }
                            }
                            if (step_by_step) 
                                return;
                        }
                    }

                    piece_of_pseudo_code_being_executed = PieceOfPseudoCode.PIECE_10A_17A;
                    while (open_list.Size() > 0)
                    {
                        GAAStarNode node = (GAAStarNode)open_list.Pop();
                        int cost = node.GetMazeLightCell().GetCost();

                        for (int i = 0; i < neighborhood; i++)
                        {
                            int x, y;
                            x = node.GetMazeLightCell().X + Maze.delta_x[i];
                            y = node.GetMazeLightCell().Y + Maze.delta_y[i];
                            if (0 <= x && x < w && 0 <= y && y < h)
                            {
                                GAAStarNode predecessor = graph[y, x];
                                if (predecessor == goal) 
                                    continue;
                                InitializeNode(predecessor);

                                if (predecessor.h > cost + node.h)
                                {
                                    predecessor.h = cost + node.h;
                                    predecessor.f = predecessor.h;
                                    open_list.Insert(predecessor);
                                }
                            }
                        }
                        if (step_by_step) return;
                    }
                    unblocked_cells = null;
                }
            }

            /* A*. */
            if (piece_of_pseudo_code_being_executed != PieceOfPseudoCode.PIECE_12_21)
            {
                InitializeNode(start);
                InitializeNode(goal);
                start.g = start.GetMazeLightCell().GetCost();
                start.parent = null;
                start.f = start.g + start.h;
                open_list.Insert(start);
                piece_of_pseudo_code_being_executed = PieceOfPseudoCode.PIECE_12_21;
                if (step_by_step) return;
            }

            while (open_list.Size() > 0 && goal.g > ((GAAStarNode)open_list.Peek()).f)
            {
                GAAStarNode node = (GAAStarNode)open_list.Pop();

                for (int i = 0; i < neighborhood; i++)
                {
                    int x, y;
                    x = node.GetMazeLightCell().X + Maze.delta_x[i];
                    y = node.GetMazeLightCell().Y + Maze.delta_y[i];
                    if (0 <= x && x < w && 0 <= y && y < h)
                    {
                        GAAStarNode child = graph[y, x];
                        if (child.GetMazeLightCell().IsBlocked()) continue;
                        InitializeNode(child);
                        int cost = child.GetMazeLightCell().GetCost();

                        if (node.g != INFINITY_INT && child.g > node.g + cost)
                        {
                            child.parent = node;
                            child.g = node.g + cost;
                            child.f = child.g + child.h;
                            open_list.Insert(child);
                        }
                    }
                }
                if (step_by_step) return;
            }

            if (open_list.Size() != 0)
            {
                GAAStarNode node = goal, node_child = null;
                path_cost_last_execution = node.g;
                has_solution = true;

                if (mark_path)
                {
                    node.GetMazeLightCell().SetNextMazeCell(null);
                    node.GetMazeLightCell().SetPathFlag();
                    node_child = node;
                    node = node.parent;

                    while (node != null)
                    {
                        node.GetMazeLightCell().SetNextMazeCell(node_child.GetMazeLightCell());
                        node.GetMazeLightCell().SetPathFlag();
                        node_child = node;
                        node = node.parent;
                    }
                }
            }

            if (has_solution)
            {
                path_cost.Add(path_cost_last_execution);
            }
            else
            {
                path_cost.Add(INFINITY_INT);
            }
            piece_of_pseudo_code_being_executed = PieceOfPseudoCode.Null;
            has_execution_finished = true;
        }

    }
}
