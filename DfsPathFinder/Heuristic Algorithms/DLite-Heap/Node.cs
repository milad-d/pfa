using System;
using System.Collections.Generic;
using System.Text;

using PathFinder.Heap;

namespace PathFinder.Heuristic_Algorithms.DLite_Heap
{
    class Node : BinaryHeapElement
    {
        /* Public: */
        public Node parent;
        public int f, g, h;
        public bool closed;

        private Cell maze_cell;
        private TieBreakingStrategy tie_breaking_strategy;

        //public int g;
        public int rhs;
        //public int h;
        //public Node parent;
        //public int X;
        //public int Y;
        public byte real_type;
        public byte type_robot_vision;
        public int iteration;
        public bool used;
        public static byte FREE = 0;
        public static byte WALL = 1;
        public static byte PATH = 2;

        public Node(Cell maze_cell , TieBreakingStrategy tie_breaking_strategy)
        {
			closed = false;
			this.maze_cell = maze_cell;
            this.tie_breaking_strategy = tie_breaking_strategy;

            this.parent = null;
            this.maze_cell.X = maze_cell.X;
            this.maze_cell.Y = maze_cell.Y;
            this.h = 0;
            this.used = false;
            this.type_robot_vision = (this.real_type = 0);
            this.g = (this.rhs = 2147483647);
            this.iteration = 0;
        }

        public override bool LessThanForHeap(BinaryHeapElement e)
        {
            if (f == ((Node)e).f)
            {
                switch (tie_breaking_strategy)
                {
                    case TieBreakingStrategy.NONE:
                        return false;
                    case TieBreakingStrategy.HIGHEST_G_VALUES:
                        return g > ((Node)e).g;
                    case TieBreakingStrategy.SMALLEST_G_VALUES:
                        return g < ((Node)e).g;
                }
            }
            return f < ((AStarNode)e).f;
        }

        public Cell GetMazeCell()
        {
            return maze_cell;
        }

        public override String ToString()
        {
            return maze_cell.ToString() + " : [" + f + "," + g + "," + h + "]";
        }

        //-------------------------------------------------------------

        private Node()
        {
        }

        public Node(int paramInt1, int paramInt2)
        {
            this.parent = null;
            maze_cell.X = paramInt1;
            maze_cell.Y = paramInt2;
            this.h = 0;
            this.used = false;
            this.type_robot_vision = (this.real_type = 0);
            this.g = (this.rhs = 2147483647);
            this.iteration = 0;
        }

        public String toString()
        {
            return (maze_cell.X + 1) + Convert.ToString((char)(maze_cell.Y + 65));
        }


    }
}
