#define TIEBREAKING

using System;
using System.Collections.Generic;
using System.Text;

using PathFinder.Heap;
using java.util;

namespace PathFinder.Algorithm_Test
{
    class DStarLite
    {
        int mazeiteration;
        public int keymodifier;
        const int LARGE = 1000000;
        public int keylength = 3;
        DStatLiteNode goaltmpcell, oldtmpcell;
        static int []reverse= {2, 3, 0, 1};

        private int w , h , path_cost , neighborhood;
	    private bool has_solution, mark_path , step_by_step;
	    private DStatLiteNode [,]graph;
        public DStatLiteNode goal , start;
	    private BinaryHeap open_list;

        private TreeSet u;
        private Key.Key_comparator cde;

	    private DStarLite()
        {
	    }

        	/* Public: */
        public DStarLite(Maze maze, bool mark_path, bool step_by_step,
            TieBreakingStrategy tie_breaking_strategy,
            Heuristic heuristic, int neighborhood)
        {
            h = maze.GetH();
            w = maze.GetW();
            open_list = new BinaryHeap(w * h);

            graph = new DStatLiteNode[h, w];
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    graph[y, x] = new DStatLiteNode(maze.GetMazeCell(x, y), tie_breaking_strategy);
                    graph[y, x].h = heuristic.DistanceToGoal(graph[y, x].GetMazeLightCell(), maze.GetGoal());

                    if (maze.cells[y, x].GetCost() == 0x7F)
                    {
                        graph[y, x].type_robot_vision = 1;
                        //graph[y, x].real_type = 1;
                    }
                    else
                    {
                        graph[y, x].type_robot_vision = 0;
                        //graph[y, x].real_type = 0;
                    }

                    graph[y, x].g = (graph[y, x].rhs = 2147483647);
                    graph[y, x].iteration = mazeiteration;
                    graph[y, x].parent = null;
                }
            }

            has_solution = false;
            this.mark_path = mark_path;
            this.step_by_step = step_by_step;
            this.neighborhood = neighborhood;
            goal = graph[maze.GetGoal().Y, maze.GetGoal().X];
            start = graph[maze.GetStart().Y, maze.GetStart().X];

            start.parent = null;
            start.g = start.GetMazeLightCell().GetCost();
            start.f = start.g + start.h;
            open_list.Insert(start);

            //------------------------------
            this.cde = new Key.Key_comparator();
            this.u = new TreeSet(this.cde);
            this.goal.rhs = 0;
            //this.iteration += 1;
            this.u.clear();
            this.u.add(new Key(this.goal));
            this.goal.real_type = (this.goal.type_robot_vision = 0);
            this.start.real_type = (this.start.type_robot_vision = 0);

        }

	    public String GetOpenListText()
        {
            String s = "";

		    for(int i = 0 ; i < open_list.Size() ; i++)
            {
			    s += ((DStatLiteNode)open_list.GetElement(i)).ToString() + "\n";
		    }

		    return s;
	    }

	    public int GetPathCost()
        {
		    return path_cost;
	    }

	    public int GetMazeLightCellG(LightCell maze_cell)
        {
		    return graph[maze_cell.Y, maze_cell.X].g;
	    }

	    public int GetMazeLightCellH(LightCell maze_cell)
        {
		    return graph[maze_cell.Y, maze_cell.X].h;
	    }

	    public bool HasExecutionFinished()
        {
		    return open_list.Size() == 0 || goal.closed;
	    }

	    public bool HasSolution()
        {
		    return has_solution;
	    }

	    public bool IsInOpenList(LightCell maze_cell)
        {
		    return open_list.Has(graph[maze_cell.Y, maze_cell.X]);
	    }

	    public bool IsInClosedList(LightCell maze_cell)
        {
		    return graph[maze_cell.Y, maze_cell.X].closed;
	    }

	    public void SetStepByStep(bool step_by_step)
        {
		    this.step_by_step = step_by_step;
	    }

	    public void Solve()
        {
		    DStatLiteNode node;
		    while(!HasExecutionFinished())
            {
			    node = (DStatLiteNode)open_list.Pop();
			    node.closed = true;

			    if(node == goal)
                {
				    DStatLiteNode node_child = null;
				    path_cost = node.g;
				    has_solution = true;

				    if(mark_path)
                    {
					    node.GetMazeLightCell().SetPathFlag();
					    node_child = node;
					    node = node.parent;

					    do
                        {
						    node.GetMazeLightCell().SetNextMazeCell(node_child.GetMazeLightCell());
						    node.GetMazeLightCell().SetPathFlag();
						    node_child = node;
						    node = node.parent;
					    }while(node != null);
				    }
				    break;
			    }

			    for(int i = 0 ; i < neighborhood ; i++)
                {
				    int x , y;
				    x = node.GetMazeLightCell().X + Maze.delta_x[i];
				    y = node.GetMazeLightCell().Y + Maze.delta_y[i];
				    if(0 <= x && x < w && 0 <= y && y < h)
                    {
					    DStatLiteNode child = graph[y, x];
					    if(child.GetMazeLightCell().IsBlocked() || child.closed) continue;
					    int cost = child.GetMazeLightCell().GetCost();

					    if(open_list.Has(child))
                        {
						    if(child.g > node.g + cost)
                            {
							    child.parent = node;
							    child.g = node.g + cost;
							    child.f = child.g + child.h;
							    open_list.Insert(child);
						    }
					    }
                        else
                        {
						    child.parent = node;
						    child.g = node.g + cost;
						    child.f = child.g + child.h;
						    open_list.Insert(child);
					    }
				    }
			    }
			    if(step_by_step) break;
		    }
        }

        #region old

        private void Update_cell(DStatLiteNode paramLightCell, int Iteration, Maze maze)
        {
            DStatLiteNode localLightCell = null;
            System.Console.WriteLine("Update_cell: " + paramLightCell);

            Key localKey = new Key(paramLightCell);
            paramLightCell.iteration = Iteration;

            if (paramLightCell != this.goal) 
            {
                paramLightCell.rhs = 2147483647;

                for (int i = 0; i < Maze.N_DIRECTIONS_WITHOUT_DIAGONALS; i++)
                {
                    int j = paramLightCell.GetMazeLightCell().X + Maze.delta_x[i];
                    int k = paramLightCell.GetMazeLightCell().Y + Maze.delta_y[i];

                    if ((0 > j) || (j >= SimulationForm.MAZE_W) || (0 > k) || (k >= SimulationForm.MAZE_H) || (maze.cells[j, k].blocked))
                      continue;
                    int m;
                    if (this.graph[j, k].g == 2147483647) 
                        m = 2147483647; 
                    else 
                    {
                        m = this.graph[j, k].g + 1;
                    }
                    if (paramLightCell.rhs > m)
                    {
                        paramLightCell.rhs = m;
                        localLightCell = this.graph[j, k];
                    }
                }
            }

            //open_list.Delete(localLightCell);
            this.u.remove(localKey);
            paramLightCell.parent = localLightCell;

            System.Console.WriteLine("New Parente: " + localLightCell);

            if (paramLightCell.g != paramLightCell.rhs)
            {
                open_list.Insert(paramLightCell);
                this.u.add(new Key(paramLightCell));
            }
        }

        private bool Mark_path(Maze maze)
        {
            if (this.start.g == 2147483647) return false;

            Object localObject = this.start;
            while (localObject != this.goal)
            {
                int i = ((DStatLiteNode)localObject).g;
                DStatLiteNode localLightCell = null;

                for (int j = 0; j < Maze.N_DIRECTIONS_WITHOUT_DIAGONALS; j++)
                {
                    int k = ((DStatLiteNode)localObject).GetMazeLightCell().X + Maze.delta_x[j];
                    int m = ((DStatLiteNode)localObject).GetMazeLightCell().Y + Maze.delta_y[j];

                    //if ((0 > k) || (k >= this.size_x) || (0 > m) || (m >= this.size_y) || (this.cells[k][m].type_robot_vision == 1))
                    if ((0 > k) || (k >= SimulationForm.MAZE_W) || (0 > m) || (m >= SimulationForm.MAZE_H) || (graph[k, m].type_robot_vision == 1))//(maze.cells[k, m].blocked))
                    {
                        continue;
                    }
                    if (this.graph[k, m].g < i)
                    {
                        i = this.graph[k, m].g;
                        localLightCell = this.graph[k, m];
                    }
                }

                ((DStatLiteNode)localObject).type_robot_vision = 2;
                ((DStatLiteNode)localObject).GetMazeLightCell().SetPathFlag();
                localObject = localLightCell;
            }
            return true;
        }

        public bool Execution_end()
        {
            return (this.u.isEmpty()) || ((this.cde.compare(new Key(this.start), (Key)this.u.first()) <= 0) && (this.start.g == this.start.rhs));
        }

        private void Clear_path()
        {
            for (int i = 0; i < SimulationForm.MAZE_W; i++)
                for (int j = 0; j < SimulationForm.MAZE_W; j++)
                    if (this.graph[i, j].type_robot_vision == 2)
                        this.graph[i, j].type_robot_vision = 0;
        }

        public void Calculate_path(bool paramBoolean, int Iteration, Maze maze)
        {
            //Clear_path();
            //with heap : DStatLiteNode node;
            while (!Execution_end())
            {
                //with heap : node = (DStatLiteNode)open_list.Pop();
                //with heap :  node.closed = true;
                Key localKey = (Key)this.u.first();
                this.u.remove(localKey);
                DStatLiteNode node = localKey.cell;

                node.iteration = Iteration;
                int i;
                int j;
                int k;
                if (node.g > node.rhs)
                {
                    node.g = node.rhs;

                    if (node != this.start)
                    {
                        for (i = 0; i < Maze.N_DIRECTIONS_WITHOUT_DIAGONALS; i++)
                        {
                            j = node.GetMazeLightCell().X + Maze.delta_x[i];
                            k = node.GetMazeLightCell().Y + Maze.delta_y[i];

                            //if ((0 > j) || (j >= this.size_x) || (0 > k) || (k >= this.size_y) || (this.cells[j][k].type_robot_vision == 1) || (node.g + 1 >= this.cells[j][k].rhs))
                            if ((0 > j) || (j >= SimulationForm.MAZE_W) || (0 > k) || (k >= SimulationForm.MAZE_H) || (maze.cells[j,k].blocked) || (node.g + 1 >= this.graph[j,k].rhs))
                            {
                                continue;
                            }
                            Update_cell(this.graph[j, k], Iteration, maze);
                        }
                    }
                }
                else
                {
                    node.g = 2147483647;

                    Update_cell(node, Iteration, maze);

                    for (i = 0; i < Maze.N_DIRECTIONS_WITHOUT_DIAGONALS; i++)
                    {
                        j = node.GetMazeLightCell().X + Maze.delta_x[i];
                        k = node.GetMazeLightCell().Y + Maze.delta_y[i];
                        
                        //if ((0 > j) || (j >= this.size_x) || (0 > k) || (k >= this.size_y) || (this.cells[j][k].type_robot_vision == 1))
                        if ((0 > j) || (j >= SimulationForm.MAZE_W) || (0 > k) || (k >= SimulationForm.MAZE_H) || (maze.cells[j, k].blocked))
                        {
                            continue;
                        }
                        Update_cell(this.graph[j,k], Iteration, maze);
                    }

                }

                if (paramBoolean)
                    break;
            }
            if (Execution_end()) 
                Mark_path(maze);
        }

        #endregion

        public DStatLiteNode[,] GetGraph()
        {
            return graph;
        }

        #region sven
        //------------------------------------------------------------------------------
        public void initialize()
        {
            ++mazeiteration;
            keymodifier = 0;
            start.g = LARGE;
            start.rhs = 0;
            #if TIEBREAKING
            open_list.Clear();
                start.key[0] = start.h;
                start.key[1] = start.h + 1;
                start.key[2] = start.h;
            #else
                open_list.Clear();
                keylength = 2;
                start.key[0] = start.h;
                start.key[1] = 0;
            #endif
            start.SearchTree = new DStatLiteNode();//new List<DStatLiteNode>();
            start.generated = mazeiteration;
            open_list.Insert(start);

            goal.g = LARGE;
            goal.rhs = LARGE;
            goal.SearchTree = new DStatLiteNode();//new List<DStatLiteNode>(); 
            goal.generated = mazeiteration;
        }

        public void initializecell(DStatLiteNode thiscell)
        {
            if (thiscell.generated != mazeiteration)
            {
                thiscell.g = LARGE;
                thiscell.rhs = LARGE;
                thiscell.SearchTree = new DStatLiteNode();//new List<DStatLiteNode>();
                thiscell.generated = mazeiteration;
            }
        }

        public void updatecell(DStatLiteNode thiscell)
        {
            if (thiscell.g < thiscell.rhs)
            {
                #if TIEBREAKING
	                thiscell.key[0] = thiscell.g + thiscell.h + keymodifier;
	                thiscell.key[1] = thiscell.g + thiscell.h + keymodifier;
	                thiscell.key[2] = thiscell.g;
                #else
	                thiscell.key[0] = thiscell.g + thiscell.h + keymodifier;
	                thiscell.key[1] = thiscell.g;
                #endif
	                open_list.Insert(thiscell);
            }
            else if (thiscell.g > thiscell.rhs)
            {
                #if TIEBREAKING
                    thiscell.key[0] = thiscell.rhs + thiscell.h + keymodifier;
                    thiscell.key[1] = thiscell.rhs + thiscell.h + keymodifier + 1;
                    thiscell.key[2] = thiscell.h + keymodifier;
                #else
	                thiscell.key[0] = thiscell.rhs + H(thiscell) + keymodifier;
	                thiscell.key[1] = thiscell.rhs;
                #endif
	                open_list.Insert(thiscell);
            }
            else
	            open_list.Delete(thiscell);
        }

        public void updatekey(DStatLiteNode thiscell)
        {
            if (thiscell.g < thiscell.rhs)
            {
                #if TIEBREAKING
                    thiscell.key[0] = thiscell.g + thiscell.h + keymodifier;
                    thiscell.key[1] = thiscell.g + thiscell.h + keymodifier;
                    thiscell.key[2] = thiscell.g;
                #else
                        thiscell.key[0] = thiscell.g + thiscell.h + keymodifier;
                    thiscell.key[1] = thiscell.g;
                #endif
            }
            else
            {
                #if TIEBREAKING
                thiscell.key[0] = thiscell.rhs + thiscell.h + keymodifier;
                thiscell.key[1] = thiscell.rhs + thiscell.h + keymodifier + 1;
                thiscell.key[2] = thiscell.h + keymodifier;
                #else
                        thiscell.key[0] = thiscell.rhs + thiscell.h + keymodifier;
                        thiscell.key[1] = thiscell.rhs;
                #endif
            }
        }

        public void updaterhs(DStatLiteNode thiscell)
        {
            int d;

            thiscell.rhs = LARGE;
            thiscell.SearchTree = new DStatLiteNode();//new List<DStatLiteNode>();
            for (d = 0; d < Maze.N_DIRECTIONS_WITHOUT_DIAGONALS; ++d)
            {
	            //if (thiscell.move[d] && thiscell.move[d].generated == mazeiteration && thiscell.rhs > thiscell.move[d].g + 1)
                if (thiscell.move[d].generated == mazeiteration && thiscell.rhs > thiscell.move[d].g + 1)
                {
	                thiscell.rhs = thiscell.move[d].g + 1;
	                thiscell.SearchTree = thiscell.move[d];
	            }
            }
            updatecell(thiscell);
        }

        public bool computeshortestpath()
        {
            DStatLiteNode tmpcell1, tmpcell2;
            goaltmpcell = new DStatLiteNode();

            int x, d;
            if (goal.g < goal.rhs)
            {
                goaltmpcell.key[0] = goal.g + keymodifier;
                goaltmpcell.key[1] = goal.g + keymodifier;
                goaltmpcell.key[2] = goal.g;
            }
            else
            {
                goaltmpcell.key[0] = goal.rhs + keymodifier;
                goaltmpcell.key[1] = goal.rhs + keymodifier + 1;
                goaltmpcell.key[2] = keymodifier;
            }
            while ((goal.rhs > goal.g || open_list.Pop().LessThanForHeap(goaltmpcell)))
            {
                tmpcell1 = (DStatLiteNode)open_list.Pop();
                oldtmpcell.key[0] = tmpcell1.key[0];
                oldtmpcell.key[1] = tmpcell1.key[1];
                oldtmpcell.key[2] = tmpcell1.key[2];
                updatekey(tmpcell1);
                if (oldtmpcell.LessThanForHeap(tmpcell1))
                    updatecell(tmpcell1);
                else if (tmpcell1.g > tmpcell1.rhs)
                {
                    tmpcell1.g = tmpcell1.rhs;
                    open_list.Delete(tmpcell1);
                    for (d = 0; d < Maze.N_DIRECTIONS_WITHOUT_DIAGONALS; ++d)
                    {
                        if (!tmpcell1.move[d].Equals(null))
                        {
                            tmpcell2 = tmpcell1.move[d];
                            initializecell(tmpcell2);
                            if (tmpcell2 != start && tmpcell2.rhs > tmpcell1.g + 1)
                            {
                                tmpcell2.rhs = tmpcell1.g + 1;
                                tmpcell2.SearchTree = tmpcell1;
                                updatecell(tmpcell2);
                            }
                        }
                    }
                }
                else
                {
                    tmpcell1.g = LARGE;
                    updatecell(tmpcell1);
                    for (d = 0; d < Maze.N_DIRECTIONS_WITHOUT_DIAGONALS; ++d)
                    {
                        if (!tmpcell1.move[d].Equals(null))
                        {
                            tmpcell2 = tmpcell1.move[d];
                            initializecell(tmpcell2);
                            if (tmpcell2 != start && tmpcell2.SearchTree == tmpcell1)
                                updaterhs(tmpcell2);
                        }
                    }
                }
                if (goal.g < goal.rhs)
                {
                    goaltmpcell.key[0] = goal.g + keymodifier;
                    goaltmpcell.key[1] = goal.g + keymodifier;
                    goaltmpcell.key[2] = goal.g;
                }
                else
                {
                    goaltmpcell.key[0] = goal.rhs + keymodifier;
                    goaltmpcell.key[1] = goal.rhs + keymodifier + 1;
                    goaltmpcell.key[2] = keymodifier;
                }
            }
            return (goal.rhs == LARGE);
        }

        public void updatemaze(DStatLiteNode robot)
        {
            int d1, d2;
            DStatLiteNode tmpcell;

            for (d1 = 0; d1 < Maze.N_DIRECTIONS_WITHOUT_DIAGONALS; ++d1)
            {
	            if (!robot.move[d1].GetMazeLightCell().blocked)
	            {
	                tmpcell = robot.move[d1];
	                initializecell(tmpcell);
	                for (d2 = 0; d2 < Maze.N_DIRECTIONS_WITHOUT_DIAGONALS; ++d2)
		            if (!tmpcell.move[d2].Equals(null))
		            {
		                tmpcell.move[d2] = null;
		                tmpcell.succ[d2].move[reverse[d2]] = null;
		                initializecell(tmpcell.succ[d2]);
		                if (tmpcell.succ[d2] != start && tmpcell.succ[d2].SearchTree == tmpcell)
			            updaterhs(tmpcell.succ[d2]);
		            }
	                if (tmpcell != start)
	                {
		                tmpcell.rhs = LARGE;
		                updatecell(tmpcell);
	                }
	            }
            }
        }
        /*
        public void printactualmaze(Maze maze)
        {
            int x, y;

            for (y = 0; y < SimulationForm.MAZE_H; ++y)
            {
                for (x = 0; x < SimulationForm.MAZE_W; ++x)
                {
                    if (goal.GetMazeLightCell() == maze.cells[y,x])
                        maze.cells[y,x].BackColor=System.Drawing.Color.Blue;
                    else if (start.GetMazeLightCell() == maze.cells[y, x])
                        maze.cells[y, x].BackColor = System.Drawing.Color.Gold;
                    else if (maze.cells[y,x].blocked)
                        maze.cells[y, x].BackColor = System.Drawing.Color.Orange;
                }

            }

        }

        public void printknownmaze(Maze maze)
        {
            DStatLiteNode tmpcell;
            int y, x,d;

            for (y = 0; y < SimulationForm.MAZE_H; ++y)
                for (x = 0; x < SimulationForm.MAZE_W; ++x)
                {
                    maze.cells[y, x].BackColor = System.Drawing.Color.Black;
                    for (d = 0; d < Maze.N_DIRECTIONS_WITHOUT_DIAGONALS; ++d)
                        if (!graph[y, x].move[d].Equals(null))
                            maze.cells[y, x].BackColor = System.Drawing.Color.White;
                }
            for (tmpcell = goal; tmpcell != start; tmpcell = tmpcell.SearchTree)
                graph[tmpcell.GetMazeLightCell().X, tmpcell.GetMazeLightCell().Y].GetMazeLightCell().SetPathFlag();
            maze.cells[start.GetMazeLightCell().Y, start.GetMazeLightCell().X].BackColor = System.Drawing.Color.Gold;
            maze.cells[goal.GetMazeLightCell().Y, goal.GetMazeLightCell().X].BackColor = System.Drawing.Color.Blue;
        }
         * 
         */

        #endregion
    }
}
