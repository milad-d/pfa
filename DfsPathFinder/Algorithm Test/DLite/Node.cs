using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder.Algorithm_Test.DLite
{
    class Node
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
        }

        public String toString()
        {
            return (this.X + 1) + Convert.ToString((char)(this.Y + 65));
        }
    }
}
