using System;
using System.Collections.Generic;
using System.Text;

using PathFinder.Heap;

namespace PathFinder.Algorithm_Test
{
    class GAAStarNode : BinaryHeapElement
    {
        public GAAStarNode parent;
        public int f, g, h, search;
        public bool closed;

        private static int INFINITY_INT = 0x7FFFFFFF;
        private static String INFINITY_STR = Convert.ToString((char)0x221E);

        public GAAStarNode(LightCell maze_cell, TieBreakingStrategy tie_breaking_strategy)
        {
            this.maze_cell = maze_cell;
            search = 0;
            f = g = h = INFINITY_INT;
            this.tie_breaking_strategy = tie_breaking_strategy;
        }

        public override bool LessThanForHeap(BinaryHeapElement e)
        {
            if (f == ((GAAStarNode)e).f)
            {
                switch (tie_breaking_strategy)
                {
                    case TieBreakingStrategy.NONE:
                        return false;
                    case TieBreakingStrategy.HIGHEST_G_VALUES:
                        return g > ((GAAStarNode)e).g;
                    case TieBreakingStrategy.SMALLEST_G_VALUES:
                        return g < ((GAAStarNode)e).g;
                }
            }
            return f < ((GAAStarNode)e).f;
        }

        public LightCell GetMazeLightCell()
        {
            return maze_cell;
        }

        public override String ToString()
        {
            return maze_cell.ToString() + " : [" + f + "," + (g.Equals(INFINITY_INT.ToString()) ? INFINITY_STR : g.ToString()) + "," + h + "]";
        }

        LightCell maze_cell;
        TieBreakingStrategy tie_breaking_strategy;

        private GAAStarNode()
        {

        }
    }
}
