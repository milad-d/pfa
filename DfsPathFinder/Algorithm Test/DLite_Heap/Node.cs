using System;
using System.Collections.Generic;
using System.Text;

using PathFinder.Heap;

namespace PathFinder.Algorithm_Test.DLite_Heap
{
    class Node : BinaryHeapElement
    {
        public int g;
        public int rhs;
        public int h;
        public Node parent;
            public int X;
            public int Y;
        public byte real_type;
        public byte type_robot_vision;
        public int iteration;
        public bool used;
        public static byte FREE = 0;
        public static byte WALL = 1;
        public static byte PATH = 2;

        private TieBreakingStrategy tie_breaking_strategy;
        public int f;
        

        private Node()
        {
        }

        public Node(int x, int y)
        {
            this.parent = null;
            this.X = x;
            this.Y = y;
            this.h = 0;
            this.used = false;
            this.type_robot_vision = (this.real_type = 0);
            this.g = (this.rhs = 2147483647);
            this.iteration = 0;

            tie_breaking_strategy = TieBreakingStrategy.HIGHEST_G_VALUES;
        }

        public String toString()
        {
            return (this.X + 1) + Convert.ToString((char)(this.Y + 65));
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
            return f < ((Node)e).f;
        }

    }
}
