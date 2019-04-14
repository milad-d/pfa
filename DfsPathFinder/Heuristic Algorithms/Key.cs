using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder.Heuristic_Algorithms
{
    class Key
    {
        private int x;
        private int y;
        public DStatLiteNode cell;

        private Key()
        {
        }

        public Key(DStatLiteNode paramCell)
        {
            this.x = Math.Min(paramCell.g, paramCell.rhs);
            this.y = this.x;
            this.x = (this.x != 2147483647 ? this.x + paramCell.h : this.x);

            this.cell = paramCell;
        }

        public String toString() 
        {
            return ("[" + this.x + "," + this.y + "] => " + this.cell);
        }

        public class Key_comparer : Comparer<Key> 
        {

            public override int Compare(Key paramKey1, Key paramKey2) 
            {
                if (paramKey1.x == paramKey2.x) 
                {
                    if (paramKey1.y == paramKey2.y) 
                    {
                        if (paramKey1.cell.GetMazeCell().X == paramKey2.cell.GetMazeCell().X) 
                        {
                            if (paramKey1.cell.GetMazeCell().Y == paramKey2.cell.GetMazeCell().Y) 
                                return 0;
                            return paramKey1.cell.GetMazeCell().Y - paramKey2.cell.GetMazeCell().Y;
                        }
                        return paramKey1.cell.GetMazeCell().X - paramKey2.cell.GetMazeCell().X;
                    }
                    return paramKey1.y - paramKey2.y;
                }
                return paramKey1.x - paramKey2.x;
            }

            public override bool Equals(Object paramObject)
            {
                return false;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        public class Key_comparator : java.util.Comparator
        {
            public int compare(Object param1, Object param2)
            {
                Key paramKey1 = (Key)param1;
                Key paramKey2 = (Key)param2;

                if (paramKey1.x == paramKey2.x)
                {
                    if (paramKey1.y == paramKey2.y)
                    {
                        if (paramKey1.cell.GetMazeCell().X == paramKey2.cell.GetMazeCell().X)
                        {
                            if (paramKey1.cell.GetMazeCell().Y == paramKey2.cell.GetMazeCell().Y)
                                return 0;
                            return paramKey1.cell.GetMazeCell().Y - paramKey2.cell.GetMazeCell().Y;
                        }
                        return paramKey1.cell.GetMazeCell().X - paramKey2.cell.GetMazeCell().X;
                    }
                    return paramKey1.y - paramKey2.y;
                }
                return paramKey1.x - paramKey2.x;
            }

            public override bool Equals(Object paramObject)
            {
                return false;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }
    }
}
