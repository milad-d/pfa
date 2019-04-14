using System;
using System.Collections.Generic;
using System.Text;

using PathFinder.Heap;

namespace PathFinder.Algorithm_Test
{
    class AStar
    {
        private int w , h , path_cost , neighborhood;
	    private bool has_solution, mark_path , step_by_step;
	    private AStarNode [,]graph;
        private AStarNode goal , start;
	    private BinaryHeap open_list;

	    private AStar()
        {
	    }

        	/* Public: */
        public AStar(Maze maze, bool mark_path, bool step_by_step,
            TieBreakingStrategy tie_breaking_strategy,
            Heuristic heuristic, int neighborhood)
        {

            h = maze.GetH();
            w = maze.GetW();
            open_list = new BinaryHeap(w * h);

            graph = new AStarNode[h, w];
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    graph[y, x] = new AStarNode(maze.GetMazeCell(x, y), tie_breaking_strategy);
                    graph[y, x].h = heuristic.DistanceToGoal(graph[y, x].GetMazeLightCell(), maze.GetGoal());
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
        }

	    public String GetOpenListText()
        {
            String s = "";

		    for(int i = 0 ; i < open_list.Size() ; i++)
            {
			    s += ((AStarNode)open_list.GetElement(i)).ToString() + "\n";
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
		    AStarNode node;
		    while(!HasExecutionFinished())
            {
			    node = (AStarNode)open_list.Pop();
			    node.closed = true;

			    if(node == goal)
                {
				    AStarNode node_child = null;
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
					    AStarNode child = graph[y, x];
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

        public AStarNode[,] GetGraph()
        {
            return graph;
        }
    }
}
