using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder.Algorithm_Test
{
    public interface Heuristic
    {
        int DistanceToGoal(LightCell maze_cell, LightCell goal);
    }
}
