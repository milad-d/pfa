using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder.Algorithm_Test
{
    class Key
    {
        private int x;
        private int y;
        public DStatLiteNode cell;

        private Key()
        {
        }

        public Key(DStatLiteNode paramLightCell)
        {
            this.x = Math.Min(paramLightCell.g, paramLightCell.rhs);
            this.y = this.x;
            this.x = (this.x != 2147483647 ? this.x + paramLightCell.h : this.x);

            this.cell = paramLightCell;
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
                        if (paramKey1.cell.GetMazeLightCell().X == paramKey2.cell.GetMazeLightCell().X) 
                        {
                            if (paramKey1.cell.GetMazeLightCell().Y == paramKey2.cell.GetMazeLightCell().Y) 
                                return 0;
                            return paramKey1.cell.GetMazeLightCell().Y - paramKey2.cell.GetMazeLightCell().Y;
                        }
                        return paramKey1.cell.GetMazeLightCell().X - paramKey2.cell.GetMazeLightCell().X;
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
                        if (paramKey1.cell.GetMazeLightCell().X == paramKey2.cell.GetMazeLightCell().X)
                        {
                            if (paramKey1.cell.GetMazeLightCell().Y == paramKey2.cell.GetMazeLightCell().Y)
                                return 0;
                            return paramKey1.cell.GetMazeLightCell().Y - paramKey2.cell.GetMazeLightCell().Y;
                        }
                        return paramKey1.cell.GetMazeLightCell().X - paramKey2.cell.GetMazeLightCell().X;
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
