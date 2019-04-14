using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder.Algorithm_Test
{
    public class NullHeuristic : Heuristic
    {
        /* Public: */
        public static NullHeuristic GetNullHeuristic()
        {
            if (null_heuristic == null)
            {
                null_heuristic = new NullHeuristic();
            }
            return null_heuristic;
        }
        public int DistanceToGoal(LightCell maze_cell, LightCell goal)
        {
            return 0;
        }

        /* Private: */
        private static NullHeuristic null_heuristic = null;

        private NullHeuristic()
        {
        }
    }
}
