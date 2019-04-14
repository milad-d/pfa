using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder.Heuristic_Algorithms
{
    public interface Heuristic
    {
        int DistanceToGoal(Cell maze_cell, Cell goal);
    }
}
