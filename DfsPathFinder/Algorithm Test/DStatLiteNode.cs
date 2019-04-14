using System;
using System.Collections.Generic;
using System.Text;

using PathFinder.Heap;

namespace PathFinder.Algorithm_Test
{
    class DStatLiteNode : BinaryHeapElement
    {
        //New
        public int[] key = new int[3];
        //public List<DStatLiteNode> SearchTree;
        public DStatLiteNode SearchTree;
        public DStatLiteNode trace;
        public int generated;
        public DStatLiteNode []move=new DStatLiteNode[Maze.N_DIRECTIONS_WITHOUT_DIAGONALS];
        public DStatLiteNode []succ=new DStatLiteNode[Maze.N_DIRECTIONS_WITHOUT_DIAGONALS];


        public int rhs;
        public byte real_type;
        public byte type_robot_vision;
        public int iteration;
        public bool used;
        public static byte FREE = 0;
        public static byte WALL = 1;
        public static byte PATH = 2;


        /* Private: */
		private LightCell maze_cell;
		private TieBreakingStrategy tie_breaking_strategy;

        public DStatLiteNode()
        {
            this.parent = null;
            this.h = 0;
            this.used = false;
            this.type_robot_vision = (this.real_type = 0);
            this.g = (this.rhs = 2147483647);
            this.iteration = 0;
		}
                
		/* Public: */
		public DStatLiteNode parent;
		public int f , g , h;
		public bool closed;

		public DStatLiteNode(LightCell maze_cell , TieBreakingStrategy tie_breaking_strategy)
        {
			closed = false;
			this.maze_cell = maze_cell;
			this.tie_breaking_strategy = tie_breaking_strategy;

            //----------------------------------------------------
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
			if(f == ((DStatLiteNode)e).f)
            {
				switch(tie_breaking_strategy)
                {
					case TieBreakingStrategy.NONE:
						return false;
                    case TieBreakingStrategy.HIGHEST_G_VALUES:
						return g > ((DStatLiteNode)e).g;
                    case TieBreakingStrategy.SMALLEST_G_VALUES:
						return g < ((DStatLiteNode)e).g;
				}
			}
			return f < ((DStatLiteNode)e).f;
		}

        public LightCell GetMazeLightCell()
        {
            return maze_cell;
        }

        public override String ToString()
        {
            return maze_cell.ToString() + " : [" + f + "," + g + "," + h + "]";
        }

        public String toString()
        {
            return (Convert.ToString(this.maze_cell.X + 1) + 
                    Convert.ToString((char)(this.maze_cell.Y + 65)));
        }

    }
}
