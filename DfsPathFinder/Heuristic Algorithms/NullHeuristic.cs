using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder.Heuristic_Algorithms
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
        public int DistanceToGoal(Cell maze_cell, Cell goal)
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
