using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder.Algorithm_Test
{
    public class ManhattanDistanceHeuristic : Heuristic
    {
        /* Public: */
        public static ManhattanDistanceHeuristic GetManhattanDistanceHeuristic()
        {
            if (manhattan_distance_heuristic == null)
            {
                manhattan_distance_heuristic = new ManhattanDistanceHeuristic();
            }
            return manhattan_distance_heuristic;
        }
        public int DistanceToGoal(LightCell maze_cell, LightCell goal)
        {
            return Math.Abs(maze_cell.X - goal.X) + Math.Abs(maze_cell.Y - goal.Y);
        }

        /* Private: */
        private static ManhattanDistanceHeuristic manhattan_distance_heuristic = null;

        private ManhattanDistanceHeuristic()
        {
        }
    }
}
