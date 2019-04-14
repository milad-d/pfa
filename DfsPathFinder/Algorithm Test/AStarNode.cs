using System;
using System.Collections.Generic;
using System.Text;

using PathFinder.Heap;

namespace PathFinder.Algorithm_Test
{
    class AStarNode : BinaryHeapElement
    {
        /* Private: */
		private LightCell maze_cell;
		private TieBreakingStrategy tie_breaking_strategy;

		private AStarNode()
        {
		}
                
		/* Public: */
		public AStarNode parent;
		public int f , g , h;
		public bool closed;

		public AStarNode(LightCell maze_cell , TieBreakingStrategy tie_breaking_strategy)
        {
			closed = false;
			this.maze_cell = maze_cell;
			this.tie_breaking_strategy = tie_breaking_strategy;
		}
                
		public override bool LessThanForHeap(BinaryHeapElement e)
        {
			if(f == ((AStarNode)e).f)
            {
				switch(tie_breaking_strategy)
                {
					case TieBreakingStrategy.NONE:
						return false;
                    case TieBreakingStrategy.HIGHEST_G_VALUES:
						return g > ((AStarNode)e).g ; //*(f/g)
                        //return g * g > ((AStarNode)e).g * f / g;
                        //return g * g > ((AStarNode)e).g * f;
                    case TieBreakingStrategy.SMALLEST_G_VALUES:
						return g < ((AStarNode)e).g;
				}
			}
			return f < ((AStarNode)e).f;
		}

        public LightCell GetMazeLightCell()
        {
            return maze_cell;
        }
        public override String ToString()
        {
            return maze_cell.ToString() + " : [" + f + "," + g + "," + h + "]";
        }
    }
}
