using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder.Algorithm_Test
{
    public class LightCell
    {
        
        public double ManhatanDistance(LightCell goal)
        {
            return (Math.Abs(this.X - goal.X) + Math.Abs(this.Y - goal.Y));
        }

        public double RealDistance(LightCell goal)
        {
            return Math.Sqrt((this.X - goal.X) * (this.X - goal.X) +
                (this.Y - goal.Y) * (this.Y - goal.Y));
        }

        //مختص طول در فضای جستجو
        public int X;
        //مختص عرض در فضای جستجو
        public int Y;      
        //اگر سلول مسدود باشد مقدار این فیلد صحیح خواهد بود
        public bool blocked;
        //مشخص کننده شماره سلول والد در لیست سلول های گسترش داده شده
        public int parentCount;

        #region For Heuristic Algorithms
    	private static byte BLOCKED = 0x7F;
        private byte cost;
        private LightCell next_maze_cell;

        public void SetCost(int cost)
        {
            if (cost > 0x7F) throw new ArgumentException();
            this.cost = (byte)cost;
        }

        public int GetCost()
        {
            return (cost & 0x7F);
        }

        public void Block()
        {
            SetCost(BLOCKED);
            blocked = true;
        }

        public LightCell clone()
        {
            LightCell maze_cell = new LightCell();
            maze_cell.X = X;
            maze_cell.Y = Y;
            maze_cell.cost = cost;
            maze_cell.next_maze_cell = next_maze_cell;
            return maze_cell;
        }

        public void ClearPathFlag()
        {
            cost &= 0x7F;
        }

        public bool IsBlocked()
        {
            return (GetCost() == BLOCKED);
        }

        public bool IsPathFlagOn()
        {
            return ((cost & 0x80) != 0);
        }

        public void SetPathFlag()
        {
            cost |= 0x80;
        }

        public void SetNextMazeCell(LightCell maze_cell)
        {
            next_maze_cell = maze_cell;
        }

        public LightCell GetNextMazeCell()
        {
            return next_maze_cell;
        }

        #endregion

        //سازنده پیشفرض
        public LightCell()
        {
            blocked = false;
        }

        //سازنده برای تعیین موقعیت در فضای جستجو و سلول والد
        public LightCell(int XX, int YY, int ParentCount)
        {
            blocked = false;
            parentCount = ParentCount;
            X = XX;
            Y = YY;
        }

        public LightCell(LightCell cell, int ParentCount)
        {
            blocked = false;
            parentCount = ParentCount;
            X = cell.X;
            Y = cell.Y;
        }

        public LightCell(int x, int y, byte cost)
        {
            if (x < 0 || y < 0 || cost < 1 || cost >= BLOCKED)
                throw new ArgumentException();
            this.X = x;
            this.Y = y;
            this.cost = (byte)cost;
            next_maze_cell = null;
        }

        //سازنده برای تعیین موقعیت در فضای جستجو و سلول والد
        public LightCell(int XX, int YY, LightCell Parent)
        {

            blocked = false;
            //this.parent = Parent;
            X = XX;
            Y = YY;
        }

        public override String ToString()
        {
            int aux = X + 1;
            return (aux < 10 ? " " : "") + aux + Convert.ToString((char)(Y + 'A'));
        }

    }
}
