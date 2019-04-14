using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder.Heuristic_Algorithms
{
    public class DiagonalDistanceHeuristic : Heuristic
    {
        /* Public: */
        public static DiagonalDistanceHeuristic GetDiagonalDistanceHeuristic()
        {
            if (diagonal_distance_heuristic == null)
            {
                diagonal_distance_heuristic = new DiagonalDistanceHeuristic();
            }
            return diagonal_distance_heuristic;
        }
        public int DistanceToGoal(Cell maze_cell, Cell goal)
        {
            return Math.Max(Math.Abs(maze_cell.X - goal.X), Math.Abs(maze_cell.Y - goal.Y));
        }

        /* Private: */
        private static DiagonalDistanceHeuristic diagonal_distance_heuristic = null;

        private DiagonalDistanceHeuristic()
        {
        }
    }
}
