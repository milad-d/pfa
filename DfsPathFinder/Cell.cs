using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PathFinder
{
    public partial class Cell : Label//PictureBox
    {          
        //AStar 
        //public double g;
        //public double h;
        //public double f;
        //public Cell parent;
        //public Cell CameFrom;

        public double ManhatanDistance(Cell goal)
        {
            return (Math.Abs(this.X - goal.X) + Math.Abs(this.Y - goal.Y));
        }

        public double RealDistance(Cell goal)
        {
            return Math.Sqrt((this.X - goal.X) * (this.X - goal.X) +
                (this.Y - goal.Y) * (this.Y - goal.Y));
        }
        //


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
        private Cell next_maze_cell;

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

        public Cell clone()
        {
            Cell maze_cell = new Cell();
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

        public void SetNextMazeCell(Cell maze_cell)
        {
            next_maze_cell = maze_cell;
        }

        public Cell GetNextMazeCell()
        {
            return next_maze_cell;
        }

        #endregion

        //سازنده پیشفرض
        public Cell()
        {
            //این تابع به صورت پیش فرض برای کنترل های دات نت وجود دارد
            InitializeComponent();

            blocked = false;
            this.BackColor = Color.White;
        }

        //سازنده برای تعیین موقعیت در فضای جستجو و سلول والد
        public Cell(int XX, int YY, int ParentCount)
        {
            //این تابع به صورت پیش فرض برای کنترل های دات نت وجود دارد
            InitializeComponent();

            blocked = false;
            this.BackColor = Color.White;
            parentCount = ParentCount;
            X = XX;
            Y = YY;
        }

        public Cell(Cell cell, int ParentCount)
        {
            //این تابع به صورت پیش فرض برای کنترل های دات نت وجود دارد
            InitializeComponent();

            blocked = false;
            this.BackColor = Color.White;
            parentCount = ParentCount;
            X = cell.X;
            Y = cell.Y;
        }


        public Cell(int x, int y, byte cost)
        {
            if (x < 0 || y < 0 || cost < 1 || cost >= BLOCKED)
                throw new ArgumentException();
            this.X = x;
            this.Y = y;
            this.cost = (byte)cost;
            next_maze_cell = null;
        }

        //سازنده برای تعیین موقعیت در فضای جستجو و سلول والد
        public Cell(int XX, int YY, Cell Parent)
        {
            //این تابع به صورت پیش فرض برای کنترل های دات نت وجود دارد
            InitializeComponent();

            blocked = false;
            this.BackColor = Color.White;
            //this.parent = Parent;
            X = XX;
            Y = YY;
        }

        // مسدود یا باز کردن سلول
        // سلول مسود سیاهرنگ و سلول آزاد سفید رنگ است
        private void Cell_MouseClick(object sender, MouseEventArgs e)
        {

        }

        //تعیین این سلول به عنوان سلول هدف
        private void SetEnd_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Gold;
        }

        //تعیین این سلول به عنوان سلول شروع
        private void SetStart_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Blue;
        }

        public override String ToString()
        {
            int aux = X + 1;
            return (aux < 10 ? " " : "") + aux + Convert.ToString((char)(Y + 'A'));
        }

        private void Cell_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                blocked = !blocked;
                if (blocked)
                {
                    this.BackColor = Color.Black;
                }
                else
                {
                    this.BackColor = Color.White;
                }
            }
        }


    }
}
