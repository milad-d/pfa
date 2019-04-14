using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder.Heuristic_Algorithms.DLite
{
    class Key
    {
        private int x;
        private int y;
        public Node cell;

        private Key()
        {
        }

        public Key(Node paramCell)
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
                        if (paramKey1.cell.X == paramKey2.cell.X)
                        {
                            if (paramKey1.cell.Y == paramKey2.cell.Y)
                                return 0;
                            return paramKey1.cell.Y - paramKey2.cell.Y;
                        }
                        return paramKey1.cell.X - paramKey2.cell.X;
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
                        if (paramKey1.cell.X == paramKey2.cell.X)
                        {
                            if (paramKey1.cell.Y == paramKey2.cell.Y)
                                return 0;
                            return paramKey1.cell.Y - paramKey2.cell.Y;
                        }
                        return paramKey1.cell.X - paramKey2.cell.X;
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
