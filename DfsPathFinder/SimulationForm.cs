using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PathFinder.Heuristic_Algorithms;

namespace PathFinder
{
    public partial class SimulationForm : Form
    {
        public SimulationForm()
        {
            InitializeComponent();
        }

        //اندازه صفحه 15 در 15 است
        static int size = 20;
        static int size2 = 30;

        // لیستی از سلولهای مسدود
        List<Point> blocked = new List<Point>();
        // فضای جستجو متشکل از سلولهای مربعی شکل
        //Cell[,] Grid = new Cell[size, size];
        //نقطه شروع
        Point start = new Point();
        // نقطه پایان
        Point end = new Point();
        // لیستی از نقاط گسترش داده فعلی
        List<Cell> points = new List<Cell>();

        public static bool MouseIsDown = false;

        static AStar a_star;
        public static Maze maze;
        public static int MAZE_W = 20, MAZE_H = 20;
        bool must_execute_step_by_step = false;
        private static String infinit = Convert.ToString((char)0x221E);

        GAAStarLazy gaa_star;
        DStarLite d_star_lite;
        PathFinder.Heuristic_Algorithms.DLite.Maze maze1;

        bool mazeCreated = false;
        bool dLiteMazeCreated = false;

        private int GetMaxCost()
        {
            return (checkBoxUniformCost.Checked ? 1 : 3);
        }
        
        private void Cell_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Cell cell = sender as Cell;

                if (listView1.Items[0].Selected)
                {
                    cell.Image = imageList1.Images[0];
                    if (cell != maze.GetGoal())
                        maze.SetStart(cell);
                }
                else if (listView1.Items[1].Selected)
                {
                    cell.Image = imageList1.Images[1];
                    if (cell != maze.GetStart())
                        maze.SetGoal(cell);
                }
                else if (listView1.Items[2].Selected)
                {
                    cell.blocked = true;
                    cell.Image = imageList1.Images[2];
                    cell.Block();
                }
                else if (listView1.Items[3].Selected)
                {
                    cell.Image = imageList1.Images[3];
                    cell.SetCost(GetMaxCost() == 1 ? 1 : 3);
                }
                else if (listView1.Items[4].Selected)
                {
                    cell.Image = imageList1.Images[4];
                    cell.SetCost(GetMaxCost() == 1 ? 1 : 2);
                }
                else if (listView1.Items[5].Selected)
                {
                    cell.Image = imageList1.Images[5];
                    cell.SetCost(1);
                }
                Application.DoEvents();
            }
        }

        private void SimulationForm_Load(object sender, EventArgs e)
        {
            /*
            //maze = new Maze(DateTime.Now.Millisecond, MAZE_W, MAZE_H, GetProbabilityToBlockACell(), GetMaxCost());
            //رسم فضای جستجو
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Grid[i, j] = new Cell();
                    Grid[i, j].BorderStyle = BorderStyle.FixedSingle;
                    Grid[i, j].Width = 25;
                    Grid[i, j].Height = 22;
                    Grid[i, j].Left = i * Grid[i, j].Width + 250;
                    Grid[i, j].Top = j * Grid[i, j].Height + 160;
                    Grid[i, j].BackColor = Color.White;
                    //Grid[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
            //this.Controls.Add(Grid[i, j]);
                    //tabControl1.TabPages[0].Controls.Add(Grid[i, j]);
                    //tabControl1.TabPages[1].Controls.Add(Grid[i, j]);
                    
                    //Grid[i, j].MouseUp += new System.Windows.Forms.MouseEventHandler(this.Cell_MouseUp);
                    Grid[i, j].MouseClick += new MouseEventHandler(Cell_Click);
                    //Grid[i, j].MouseEnter += new System.EventHandler(this.Cell_MouseEnter);
                }
            }
            */
        }

        // پیدا کردن تمام سلول های مسدود ، شروع و پایان
        private void TakeAllBlokedCellsStartAndEnd()
        {
            blocked.Clear();
            bool haveStart = false
                , haveEnd = false;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (maze.cells[i, j].blocked == true)
                    {
                        blocked.Add(new Point(i,j));
                    }
                    if (maze.cells[i, j].BackColor == Color.Blue)
                    {
                        start = new Point(i, j);
                        haveStart = true;
                    }
                    if (maze.cells[i, j].BackColor == Color.Gold)
                    {
                        end = new Point(i, j);
                        haveEnd = true;
                    }
                }
            }
            //اگر نقاط شروع و پایان وجود نداشت آن ها را ایجاد کن
            if (!haveStart & !haveEnd)
            {
                start = new Point(0, 0);
                end = new Point(14, 4);
                maze.cells[0, 0].BackColor = Color.Blue;
                maze.cells[14, 14].BackColor = Color.Gold;
            }
            if (haveStart & !haveEnd)
            {
                end = new Point((16 - start.X) % size, (16 - start.Y) % size);
                maze.cells[(16 - start.X) % size, (16 - start.Y) % size].BackColor = Color.Gold;
            }
            if (!haveStart & haveEnd)
            {
                start = new Point((16 - start.X) % size, (16 - start.Y) % size);
                maze.cells[(16 - start.X) % size, (16 - start.Y) % size].BackColor = Color.Blue;
            }
        }

        #region Uninformed Search
       
        // راه اندازی مجدد برنامه
        private void toolRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        //توضیح اجرای برنامه
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "مسدود کردن : برای مسدود کردن یا آزاد کردن یک سلول از دکمه چپ ماوس استفاده کنید" +
                "\n" +
                "تعیین نقاط شروع و پایانی : روی سلول مورد نظر کلیک راست کرده گزینه دلخواه را انتخاب کنید"+
                "\n" +
                "تذکر : فقط از یک سلول شروع و فقط از یک سلول پایان استفاده کنید"
                ,
                "توضیح اجرای برنامه"
                );
        }

        private void بازکردنToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Text File|*.txt";
                ofd.Title = "باز کردن نقشه";
                if (ofd.ShowDialog() == DialogResult.OK)
                {

                    string[] list = System.IO.File.ReadAllLines(ofd.FileName);
                    blocked.Clear();

                    maze = new Maze(MAZE_W, MAZE_H);

                    for (int i = 0; i < MAZE_W; i++)
                    {
                        for (int j = 0; j < MAZE_H; j++)
                        {
                            maze.cells[i, j].SetCost(int.Parse(list[size * i + j]));

                            maze.cells[i, j].BorderStyle = BorderStyle.FixedSingle;
                            maze.cells[i, j].Width = ((this.Width - 220) / MAZE_W) - 1;// 57;
                            maze.cells[i, j].Height = ((this.Height - 90) / MAZE_H) - 1; //31;
                            maze.cells[i, j].Left = i * maze.cells[i, j].Width + 220;// 800;
                            maze.cells[i, j].Top = j * maze.cells[i, j].Height + 80;// 160;
                            maze.cells[i, j].BackColor = Color.White;
                            maze.cells[i, j].Font = new Font("Arial", 8);
                            maze.cells[i, j].TextAlign = ContentAlignment.MiddleCenter;
                            //maze.cells[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                            this.Controls.Add(maze.cells[i, j]);
                            maze.cells[i, j].MouseClick += new MouseEventHandler(Cell_Click);

                            switch (maze.cells[i, j].GetCost())
                            {
                                case 1:
                                    //maze.cells[i, j].Image = imageList1.Images[6];
                                    break;
                                case 2:
                                    maze.cells[i, j].Image = imageList1.Images[4];
                                    break;
                                case 3:
                                    maze.cells[i, j].Image = imageList1.Images[3];
                                    break;
                                case 0x7F:
                                    maze.cells[i, j].Image = imageList1.Images[2];
                                    maze.cells[i, j].blocked = true;
                                    blocked.Add(new Point(i, j));
                                    break;
                                default:
                                    break;
                            }
                            if (maze.GetGoal() == maze.cells[i, j])
                            {
                                maze.cells[i, j].Image = imageList1.Images[1];
                                maze.cells[i, j].BackColor = Color.Gold;
                            }
                            else if (maze.GetStart() == maze.cells[i, j])
                            {
                                maze.cells[i, j].Image = imageList1.Images[0];
                                maze.cells[i, j].BackColor = Color.Blue;
                            }

                            if (maze.cells[i, j].BackColor == Color.Blue)
                            {
                                start = new Point(i, j);
                            }
                            if (maze.cells[i, j].BackColor == Color.Gold)
                            {
                                end = new Point(i, j);
                            }
                        }
                    }
                }

                MessageBox.Show("نقشه باز شد");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //MessageBox.Show("مطمئن شوید نقشه وجود دارد و دوباره امتحان کنید",
                //    "نقشه باز نشد");
            }

        }

        private void ذخیرهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "";
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        str += maze.cells[i, j].GetCost() + "\n";
                    }
                }
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "ذخیره نقشه";
                sfd.Filter = "Text File|*.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(sfd.FileName, str);
                    MessageBox.Show("نقشه ذخیره شد");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("مطمئن شوید نقشه وجود دارد و دوباره امتحان کنید",
                    "نقشه ذخیره نشد");
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void howToRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                            "مسدود کردن : برای مسدود کردن یا آزاد کردن یک سلول از دکمه چپ ماوس استفاده کنید" +
                            "\n" +
                            "تعیین نقاط شروع و پایانی : روی سلول مورد نظر کلیک راست کرده گزینه دلخواه را انتخاب کنید" +
                            "\n" +
                            "تذکر : فقط از یک سلول شروع و فقط از یک سلول پایان استفاده کنید"
                            ,
                            "توضیح اجرای برنامه"
                            );
        }

        //اول سطح
        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dLiteMazeCreated)
                    button2_Click(sender, e);

                else if (mazeCreated)
                {
                    UnInformed_Algorithms.BFS bfs1 =
                        new PathFinder.UnInformed_Algorithms.BFS(maze, true, false);

                    bfs1.Solve();
                }
                else if (!mazeCreated)
                    MessageBox.Show("ابتدا دکمه نقشه جدید را کلیک کنید", "خطا",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("ابتدا دکمه نقشه جدید را کلیک کنید", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /*{
            //محاسبه تمام نقاط مسدود و تمام سلول های شروع و  پایان
            //TakeAllBlokedCellsStartAndEnd();
            //blocked.Clear();
            points.Clear();

            //شمارنده ای برای حرکت در مجموعه نقاط
            //نقطه فعلی را دنبال می کند
            int Current = 0;
            //Used for expanding the fringe set (keeps track of the end of the fringe set)
            int tempCount = 1;

            //اضافه کردن نقطه شروع به مجموعه نقاط برای اینکه می خواهیم از این نقطه
            // شروع به گسترش دادن و جستجو برای رسیدن به هدف را انجام دهیم
            points.Add(new Cell(start.X, start.Y, Current));

            //تا زمانی که نقطه فعلی به هدف نرسیده است دستورات این حلقه را اجرا کن
            while (!(points[Current].X == end.X && points[Current].Y == end.Y))
            {
                //for (int i = 0; i < 10000000; i++) ;

                //points.Add(new Cell());

                //رسم نقطه فعلی به رنگ نقطه آبی روشن
                maze.cells[points[Current].X, points[Current].Y].BackColor = Color.LightBlue;
                maze.cells[points[Current].X, points[Current].Y].Refresh();

                //برای ساختن 4 فرزند سلول (نقطه) فعلی
                //یعنی سلول های بالا ، پایین ، جپ و راست سلول فعلی
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        // برای جلوگیری از ساختن فرزندان به صورت مورب
                        // با این فرض که سلول فعلی فقط در یکی از جهات طول یا عرض حرکت می کند
                        if(!checkBoxDiagonalMove.Checked)
                        if (x * y == 0)
                        {
                            // شمارنده ای برای نقاط غیر قابل گسترش
                            // اگر نقطه فعلی خارج از صفحه جستجو باشد ، یا مسدود باشد و یا قبلاً گسترش داده شده باشد
                            // یک واحد به این شمارنده اضافه می شود
                            // اگر این مقدار برای نقطه فعلی غیر صفر باشد ، نشان دهنده این است که نباید آن را گسترش داد
                            int failsTest = 0;

                            //نقطه ای که می خواهد گسترش یابد
                            Cell tempNode = new Cell();
                            tempNode = new Cell(points[Current].X + x,
                                                points[Current].Y + y,
                                                Current);

                            // بررسی اینکه آیا نقطه از صفحه خارج شده است یا خیر
                            // اگر پاسخ مثبت است یک واحد به مقدار شمارنده آزمایش اضافه کن
                            if ((tempNode.X < 0) ||
                                (tempNode.Y < 0) ||
                                (tempNode.X > size - 1) ||
                                (tempNode.Y > size - 1))
                            {
                                failsTest++;
                            }

                            // بررسی اینکه آیا نقطه قبلاً گسترش داده شده است؟
                            // اگر پاسخ مثبت است یک واحد به مقدار شمارنده آزمایش اضافه کن
                            if (failsTest < 1)
                            {
                                for (int i = 0; i < points.Count; i++)
                                {
                                    if ((tempNode.X == points[i].X) &&
                                        (tempNode.Y == points[i].Y))
                                    {
                                        failsTest++;
                                    }
                                }
                            }

                            // بررسی اینکه آیا نقطه گسترش داده شده مسدود است یا خیر؟
                            // اگر پاسخ مثبت است یک واحد به مقدار شمارنده آزمایش اضافه کن
                            if (failsTest < 1)
                            {
                                for (int i = 0; i < blocked.Count; i++)
                                {
                                    if ((tempNode.X == blocked[i].X) &&
                                        (tempNode.Y == blocked[i].Y))
                                    {
                                        failsTest++;
                                    }
                                }
                            }

                            // اگر مقدار شمارنده آزمایش صفر باقی ماند نقطه فعلی را گسترش بده
                            if (failsTest < 1)
                            {
                                //points.Add(tempNode);
                                //points[Current + tempCount] = tempNode;
                                points.Insert(Current + tempCount, tempNode);

                                // رسم نقطه گسترش یافته به رنگ خاکستری
                                maze.cells[tempNode.X, tempNode.Y].BackColor = Color.Gray;//Gray is expanded Color
                                maze.cells[tempNode.X, tempNode.Y].Refresh();

                                tempCount++;
                            }

                        }
                    }
                }

                Current++;
                tempCount--;
            }
            // تا این جا با گسترش دادن نقاط و جستجو به هدف رسیده ایم
            // از این پس باید مسیر رسیدن به هدف را رسم کنیم
            // مسیری که پیدا میکنیم لزوماً بهینه نیست و فقط یک مسیر از شروع به هدف است


            // شمارنده ای برای حرکت در مسیر رسیدن به هدف
            int counter = 0;
            List<Point> path = new List<Point>();

            //تا زمانیکه والد سلول فعلی صفر نشده یعنی تا به سلول شروع نرسیده ای
            //سلول را به مسیر اضافه کن
            while (Current != 0)
            {
                path.Add(new Point());
                path[counter] = new Point(points[Current].X, points[Current].Y);
                Current = points[Current].parentCount;
                counter++;
            }

            // سلول شروع را به مسیر اضافه کن
            path.Add(start);
            path[counter] = start;

            // تمام سلول های مسیر را به رنگ قرمز رنگ کن
            for (int i = 0; i < path.Count; i++)
            {
                maze.cells[path[i].X, path[i].Y].BackColor = Color.Red;
                maze.cells[path[i].X, path[i].Y].Refresh();
            }
        }*/

        //اول عمق
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            //button1_Click(sender, null);
            try
            {
                if (dLiteMazeCreated)
                    button2_Click(sender, e);

                else if (mazeCreated)
                {
                    UnInformed_Algorithms.DFS dfs =
                        new PathFinder.UnInformed_Algorithms.DFS(maze, true, false);

                    dfs.Solve();
                }
                else if(!mazeCreated)
                    MessageBox.Show("ابتدا دکمه نقشه جدید را کلیک کنید", "خطا",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("ابتدا دکمه نقشه جدید را کلیک کنید", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /*{
            //محاسبه تمام نقاط مسدود و تمام سلول های شروع و  پایان
            //TakeAllBlokedCellsStartAndEnd();
            //blocked.Clear();
            points.Clear();


            //شمارنده ای برای حرکت در مجموعه نقاط
            //نقطه فعلی را دنبال می کند
            int Current = 0;

            //اضافه کردن نقطه شروع به مجموعه نقاط برای اینکه می خواهیم از این نقطه
            // شروع به گسترش دادن و جستجو برای رسیدن به هدف را انجام دهیم
            points.Add(new Cell(start.X, start.Y, Current));

            //تا زمانی که نقطه فعلی به هدف نرسیده است دستورات این حلقه را اجرا کن
            while (!(points[Current].X == end.X && points[Current].Y == end.Y))
            {
                //points.Add(new Cell());

                //رسم نقطه فعلی به رنگ نقطه آبی روشن
                maze.cells[points[Current].X, points[Current].Y].BackColor = Color.LightBlue;
                maze.cells[points[Current].X, points[Current].Y].Refresh();

                //برای ساختن 4 فرزند سلول (نقطه) فعلی
                //یعنی سلول های بالا ، پایین ، جپ و راست سلول فعلی
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        // برای جلوگیری از ساختن فرزندان به صورت مورب
                        // با این فرض که سلول فعلی فقط در یکی از جهات طول یا عرض حرکت می کند
                        if (x * y == 0)
                        {
                            // شمارنده ای برای نقاط غیر قابل گسترش
                            // اگر نقطه فعلی خارج از صفحه جستجو باشد ، یا مسدود باشد و یا قبلاً گسترش داده شده باشد
                            // یک واحد به این شمارنده اضافه می شود
                            // اگر این مقدار برای نقطه فعلی غیر صفر باشد ، نشان دهنده این است که نباید آن را گسترش داد
                            int failsTest = 0;

                            //نقطه ای که می خواهد گسترش یابد
                            Cell tempNode = new Cell();
                            tempNode = new Cell(points[Current].X + x,
                                                points[Current].Y + y,
                                                Current);

                            // بررسی اینکه آیا نقطه از صفحه خارج شده است یا خیر
                            // اگر پاسخ مثبت است یک واحد به مقدار شمارنده آزمایش اضافه کن
                            if ((tempNode.X < 0) ||
                                (tempNode.Y < 0) ||
                                (tempNode.X > size - 1) ||
                                (tempNode.Y > size - 1))
                            {
                                failsTest++;
                            }

                            // بررسی اینکه آیا نقطه قبلاً گسترش داده شده است؟
                            // اگر پاسخ مثبت است یک واحد به مقدار شمارنده آزمایش اضافه کن
                            if (failsTest < 1)
                            {
                                for (int i = 0; i < points.Count; i++)
                                {
                                    if ((tempNode.X == points[i].X) &&
                                        (tempNode.Y == points[i].Y))
                                    {
                                        failsTest++;
                                    }
                                }
                            }

                            // بررسی اینکه آیا نقطه گسترش داده شده مسدود است یا خیر؟
                            // اگر پاسخ مثبت است یک واحد به مقدار شمارنده آزمایش اضافه کن
                            if (failsTest < 1)
                            {
                                for (int i = 0; i < blocked.Count; i++)
                                {
                                    if ((tempNode.X == blocked[i].X) &&
                                        (tempNode.Y == blocked[i].Y))
                                    {
                                        failsTest++;
                                    }
                                }
                            }

                            // اگر مقدار شمارنده آزمایش صفر باقی ماند نقطه فعلی را گسترش بده
                            if (failsTest < 1)
                            {
                                if (Current < points.Count)
                                {
                                    //points.Insert(0, new Cell(0, 0, 0));
                                    //points[points.Count + 1] = new Cell(0, 0, 0);

                                    points.Add(new Cell());
                                    Cell prevNode = points[Current + 1];
                                    for (int i = Current + 2; i < points.Count; i++)
                                    {
                                        Cell nextNode = new Cell();
                                        nextNode = prevNode;
                                        prevNode = points[i];
                                        points[i] = nextNode;
                                    }
                                }
                                //points.Add(tempNode);
                                points[Current + 1] = tempNode;

                                // رسم نقطه گسترش یافته به رنگ خاکستری
                                maze.cells[tempNode.X, tempNode.Y].BackColor = Color.Gray;//Gray is expanded Color
                                maze.cells[tempNode.X, tempNode.Y].Refresh();
                            }

                        }
                    }
                }

                Current++;
            }
            // تا این جا با گسترش دادن نقاط و جستجو به هدف رسیده ایم
            // از این پس باید مسیر رسیدن به هدف را رسم کنیم
            // مسیری که پیدا میکنیم لزوماً بهینه نیست و فقط یک مسیر از شروع به هدف است


            // شمارنده ای برای حرکت در مسیر رسیدن به هدف
            int counter = 0;
            List<Point> path = new List<Point>();

            //تا زمانیکه والد سلول فعلی صفر نشده یعنی تا به سلول شروع نرسیده ای
            //سلول را به مسیر اضافه کن
            while (Current != 0)
            {
                path.Add(new Point());
                path[counter] = new Point(points[Current].X, points[Current].Y);
                Current = points[Current].parentCount;
                counter++;
            }

            // سلول شروع را به مسیر اضافه کن
            path.Add(start);
            path[counter] = start;

            // تمام سلول های مسیر را به رنگ قرمز رنگ کن
            for (int i = 0; i < path.Count; i++)
            {
                maze.cells[path[i].X, path[i].Y].BackColor = Color.Red;
                maze.cells[path[i].X, path[i].Y].Refresh();
            }
        }*/

        #endregion

        //-------------------------------------------------------------------------

        private TieBreakingStrategy GetTieBreakingStrategy()
        {
            switch (ComboBoxTie_breaking.SelectedIndex)
            {
                default:
                case 0:
                    return TieBreakingStrategy.NONE;
                case 1:
                    return TieBreakingStrategy.HIGHEST_G_VALUES;
                case 2:
                    return TieBreakingStrategy.SMALLEST_G_VALUES;
            }
        }

        private bool MustUseHeuristic()
        {
            return checkBoxHeuristic.Checked;
        }

        private int GetNeighborhood()
        {
            return (checkBoxDiagonalMove.Checked ? Maze.N_DIRECTIONS_WITH_DIAGONALS : Maze.N_DIRECTIONS_WITHOUT_DIAGONALS);
        }

        private Heuristic SelectProperHeuristic()
        {
            if (MustUseHeuristic())
            {
                if (checkBoxDiagonalMove.Checked) return DiagonalDistanceHeuristic.GetDiagonalDistanceHeuristic();
                else return ManhattanDistanceHeuristic.GetManhattanDistanceHeuristic();
            }
            else
            {
                return NullHeuristic.GetNullHeuristic();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBarBlockedPercent.Value.ToString();
        }

        private float GetProbabilityToBlockACell()
        {
            return (float)(trackBarBlockedPercent.Value) / 100;
        }

        private void UpdateOpenListText(string algorithm)
        {
            switch (algorithm)
            {
                case "A*":
                    richTextBox1.Text = a_star.GetOpenListText();
                    break;
                case "GAA*":
                    richTextBox1.Text = gaa_star.GetOpenListText();
                    break;
            }
        }

        //رسم نقشه جدید
        private void button1_Click(object sender, EventArgs e)
        {
            if (dLiteMazeCreated)
                MessageBox.Show(" را حذف کنید " + " D*Lite " + "ابتدا نقشه", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                button1.Enabled = false;
                mazeCreated = true;

                blocked.Clear();
                /*if(!maze.Equals(null))
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        this.Controls.Remove(maze.cells[i, j]);
                    }
                }*/

                maze = new Maze(DateTime.Now.Millisecond, MAZE_W, MAZE_H, GetProbabilityToBlockACell(), GetMaxCost());
                for (int i = 0; i < MAZE_W; i++)
                {
                    for (int j = 0; j < MAZE_H; j++)
                    {
                        //richTextBox1.Text += 
                        //    "Cost[" + i + "," + j + "]: " + maze.cells[i, j].GetCost() + "\n";

                        maze.cells[i, j].BorderStyle = BorderStyle.FixedSingle;
                        maze.cells[i, j].Width = ((this.Width - 220) / MAZE_W) - 1;// 57;
                        maze.cells[i, j].Height = ((this.Height - 90) / MAZE_H) - 1; //31;
                        maze.cells[i, j].Left = i * maze.cells[i, j].Width + 220;// 800;
                        maze.cells[i, j].Top = j * maze.cells[i, j].Height + 80;// 160;
                        maze.cells[i, j].BackColor = Color.White;
                        maze.cells[i, j].Font = new Font("Arial", 8);
                        maze.cells[i, j].TextAlign = ContentAlignment.MiddleCenter;
                        //maze.cells[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                        this.Controls.Add(maze.cells[i, j]);
                        maze.cells[i, j].MouseClick += new MouseEventHandler(Cell_Click);

                        switch (maze.cells[i, j].GetCost())
                        {
                            case 1:
                                //maze.cells[i, j].Image = imageList1.Images[6];
                                break;
                            case 2:
                                maze.cells[i, j].Image = imageList1.Images[4];
                                break;
                            case 3:
                                maze.cells[i, j].Image = imageList1.Images[3];
                                break;
                            case 0x7F:
                                maze.cells[i, j].Image = imageList1.Images[2];
                                maze.cells[i, j].blocked = true;
                                blocked.Add(new Point(i, j));
                                break;
                            default:
                                break;
                        }
                        if (maze.GetGoal() == maze.cells[i, j])
                        {
                            maze.cells[i, j].Image = imageList1.Images[1];
                            maze.cells[i, j].BackColor = Color.Gold;
                        }
                        else if (maze.GetStart() == maze.cells[i, j])
                        {
                            maze.cells[i, j].Image = imageList1.Images[0];
                            maze.cells[i, j].BackColor = Color.Blue;
                        }

                        if (maze.cells[i, j].BackColor == Color.Blue)
                        {
                            start = new Point(i, j);
                        }
                        if (maze.cells[i, j].BackColor == Color.Gold)
                        {
                            end = new Point(i, j);
                        }

                    }
                }
            }
        }

        //حذف نقشه قبلی
        private void button2_Click(object sender, EventArgs e)
        {
            if (mazeCreated)
            {
                button1.Enabled = true;
                mazeCreated = false;

                if (!maze.Equals(null))
                    for (int i = 0; i < MAZE_W; i++)
                    {
                        for (int j = 0; j < MAZE_H; j++)
                        {
                            this.Controls.Remove(maze.cells[i, j]);
                        }
                    }
            }
            else
            {
                MessageBox.Show("نقشه ای ایجاد نشده است","خطا", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolAStar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dLiteMazeCreated)
                    button2_Click(sender, e);

                else if (mazeCreated)
                {
                    a_star = new AStar(maze, true, must_execute_step_by_step, GetTieBreakingStrategy(),
                        SelectProperHeuristic(), GetNeighborhood());
                    a_star.Solve();

                    for (int i = 0; i < MAZE_W; i++)
                    {
                        for (int j = 0; j < MAZE_H; j++)
                        {
                            maze.cells[i, j].Text = "";

                            if (!maze.cells[i, j].IsPathFlagOn())
                            {
                                if (a_star.IsInOpenList(maze.cells[i, j]))
                                {
                                    //سلول ها ی موجود در لیست گره های باز
                                    maze.cells[i, j].BackColor = Color.Violet;
                                }
                                else if (a_star.IsInClosedList(maze.cells[i, j]))
                                {
                                    //سلول هایی که ملاقات شده اند
                                    maze.cells[i, j].BackColor = Color.LightGreen;
                                }
                            }

                            if (maze.cells[i, j] != maze.GetGoal() && maze.cells[i, j] != maze.GetStart())
                            {
                                if (maze.cells[i, j].IsPathFlagOn())
                                    maze.cells[i, j].BackColor = Color.Red;

                                if (a_star.IsInOpenList(maze.cells[i, j]) || a_star.IsInClosedList(maze.cells[i, j]))
                                {
                                    maze.cells[i, j].Text = Convert.ToString(a_star.GetMazeCellG(maze.cells[i, j]));
                                }
                                else
                                {
                                    maze.cells[i, j].Text += infinit;
                                }
                                maze.cells[i, j].Text += "/" + a_star.GetMazeCellH(maze.cells[i, j]);
                            }
                        }
                    }
                    UpdateOpenListText("A*");
                }
                else if (!mazeCreated)
                    MessageBox.Show("ابتدا دکمه نقشه جدید را کلیک کنید", "خطا",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("ابتدا دکمه نقشه جدید را کلیک کنید", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolGAAStar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dLiteMazeCreated)
                    button2_Click(sender, e);

                else if (mazeCreated)
                {
                    maze.CleanPath();
                    gaa_star = new GAAStarLazy(maze, true, must_execute_step_by_step, GetTieBreakingStrategy(),
                        SelectProperHeuristic(), GetNeighborhood());
                    gaa_star.Solve();

                    for (int i = 0; i < MAZE_W; i++)
                    {
                        for (int j = 0; j < MAZE_H; j++)
                        {
                            maze.cells[i, j].Text = "";

                            if (!maze.cells[i, j].IsPathFlagOn())
                            {
                                if (gaa_star.IsInOpenList(maze.cells[i, j]))
                                {
                                    //سلول ها ی موجود در لیست گره های باز
                                    maze.cells[i, j].BackColor = Color.Violet;
                                }
                                else if (gaa_star.HasBeenVisitedinLastExecution(maze.cells[i, j]))
                                {
                                    //سلول هایی که ملاقات شده اند
                                    maze.cells[i, j].BackColor = Color.LightGreen;
                                }
                            }

                            if (maze.cells[i, j] != maze.GetGoal() && maze.cells[i, j] != maze.GetStart())
                            {
                                if (maze.cells[i, j].IsPathFlagOn())
                                    maze.cells[i, j].BackColor = Color.DarkRed;

                                maze.cells[i, j].Text =
                                    (gaa_star.GetMazeCellG(maze.cells[i, j]) != 0x7FFFFFFF ? Convert.ToString(gaa_star.GetMazeCellG(maze.cells[i, j])) : infinit);
                                maze.cells[i, j].Text += "/" + (gaa_star.GetMazeCellH(maze.cells[i, j]) != 0x7FFFFFFF ? Convert.ToString(gaa_star.GetMazeCellH(maze.cells[i, j])) : infinit);
                            }
                        }
                    }
                    UpdateOpenListText("GAA*");
                }
                else if (!mazeCreated)
                    MessageBox.Show("ابتدا دکمه نقشه جدید را کلیک کنید", "خطا",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("ابتدا دکمه نقشه جدید را کلیک کنید", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl1_MouseHover(object sender, EventArgs e)
        {
            tabControl1.Size = new Size(784, 105);
        }

        private void buttonToolHide_Click(object sender, EventArgs e)
        {
            tabControl1.Size = new Size(784, 25);
        }

        private void toolDStarLite_Click(object sender, EventArgs e)
        {
            try
            {
                if (mazeCreated)
                    button2_Click(sender, e);

                if (!dLiteMazeCreated)
                    MessageBox.Show(" را کلیک کنید" + " D*Lite " + "ابتدا دکمه نقشه  ", "خطا",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                else if (dLiteMazeCreated)
                {
                    maze1.Calculate_path(false);

                    for (int i = 0; i < MAZE_W; i++)
                    {
                        for (int j = 0; j < MAZE_H; j++)
                        {
                            maze1.cells[i, j].Text = "";

                            switch (maze1.cells[i, j].type_robot_vision)
                            {
                                case 0:
                                    if (maze1.cells[i, j].iteration == 1)
                                        maze1.cells[i, j].BackColor = Color.LightGreen;
                                    else if (maze1.cells[i, j].iteration != 0)
                                        maze1.cells[i, j].BackColor = Color.LightGray;
                                    else
                                        maze1.cells[i, j].BackColor = Color.White;
                                    break;
                                case 2:
                                    maze1.cells[i, j].BackColor = Color.Red;
                                    break;
                                default:
                                    break;
                            }

                            if (maze1.goal == maze1.cells[i, j])
                                maze1.cells[i, j].Image = imageList1.Images[1];
                            else if (maze1.start == maze1.cells[i, j])
                                maze1.cells[i, j].Image = imageList1.Images[0];
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(" را کلیک کنید" + " D*Lite " + "ابتدا دکمه نقشه  ", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /*{
            maze.CleanPath();
            d_star_lite = new DStarLite(maze, true, must_execute_step_by_step, GetTieBreakingStrategy(),
                SelectProperHeuristic(), GetNeighborhood());

            //d_star_lite.Solve();
            d_star_lite.Calculate_path(false, 1, maze);

            for (int i = 0; i < MAZE_W; i++)
            {
                for (int j = 0; j < MAZE_H; j++)
                {
                    //maze.cells[i, j] = a_star.GetGraph()[i,j].GetMazeCell();
                    //maze.cells[i, j].Refresh();

                    if (d_star_lite.GetGraph()[i, j].type_robot_vision == 2)
                    {
                        maze.cells[i, j].BackColor = Color.LightBlue;
                    }

                    if (!maze.cells[i, j].IsPathFlagOn())
                    {
                        if (d_star_lite.IsInOpenList(maze.cells[i, j]))
                        {
                            //سلول ها ی موجود در لیست گره های باز
                            //maze.cells[i, j].BackColor = Color.HotPink;
                        }
                        else if (d_star_lite.IsInClosedList(maze.cells[i, j]))
                        {
                            //سلول هایی که ملاقات شده اند
                            maze.cells[i, j].BackColor = Color.LightGreen;
                        }
                    }

                    if (maze.cells[i, j] != maze.GetGoal() && maze.cells[i, j] != maze.GetStart())
                    {
                        if (maze.cells[i, j].IsPathFlagOn())
                            maze.cells[i, j].BackColor = Color.Red;

                        if (d_star_lite.IsInOpenList(maze.cells[i, j]) || d_star_lite.IsInClosedList(maze.cells[i, j]))
                        {
                            maze.cells[i, j].Text = Convert.ToString(d_star_lite.GetMazeCellG(maze.cells[i, j]));
                        }
                        else
                        {
                            maze.cells[i, j].Text += infinit;
                        }
                        maze.cells[i, j].Text += "/" + d_star_lite.GetMazeCellH(maze.cells[i, j]);
                    }
                }
            }
        }*/

        //نقشه
        private void button4_Click(object sender, EventArgs e)
        {
            if (mazeCreated)
                MessageBox.Show(" ابتدا نقشه قبلی را حذف کنید ", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                button4.Enabled = false;
                dLiteMazeCreated = true;

                maze1 =
                    new PathFinder.Heuristic_Algorithms.DLite.Maze
                        (DateTime.Now.Millisecond, MAZE_W, MAZE_H);

                maze1.Initialize();
                //maze1.Calculate_path(false);

                for (int i = 0; i < MAZE_W; i++)
                {
                    for (int j = 0; j < MAZE_H; j++)
                    {
                        //richTextBox1.Text += 
                        //    "Cost[" + i + "," + j + "]: " + maze1.cells[i, j].GetCost() + "\n";

                        maze1.cells[i, j].BorderStyle = BorderStyle.FixedSingle;
                        maze1.cells[i, j].Width = ((this.Width - 220) / MAZE_W) - 1;// 57;
                        maze1.cells[i, j].Height = ((this.Height - 90) / MAZE_H) - 1; //31;
                        maze1.cells[i, j].Left = i * maze1.cells[i, j].Width + 220;// 800;
                        maze1.cells[i, j].Top = j * maze1.cells[i, j].Height + 80;// 160;
                        maze1.cells[i, j].BackColor = Color.White;
                        maze1.cells[i, j].Font = new Font("Arial", 8);
                        maze1.cells[i, j].TextAlign = ContentAlignment.MiddleCenter;
                        this.Controls.Add(maze1.cells[i, j]);
                        maze1.cells[i, j].MouseClick += new MouseEventHandler(Cell_Click);

                        switch (maze1.cells[i, j].real_type)
                        {
                            case 0:
                                //maze1.cells[i, j].Image = imageList1.Images[6];
                                //maze1.cells[i, j].BackColor = Color.Yellow;
                                break;
                            case 1:
                                maze1.cells[i, j].Image = imageList1.Images[2];
                                break;
                            case 2:
                                maze1.cells[i, j].BackColor = Color.Red;
                                break;
                            case 3:
                                maze1.cells[i, j].Image = imageList1.Images[3];
                                break;
                            default:
                                break;
                        }
                        if (maze1.cells[i, j].type_robot_vision == 2)
                            maze1.cells[i, j].BackColor = Color.Tomato;

                        if (maze1.goal == maze1.cells[i, j])
                            maze1.cells[i, j].Image = imageList1.Images[1];
                        else if (maze1.start == maze1.cells[i, j])
                            maze1.cells[i, j].Image = imageList1.Images[0];
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dLiteMazeCreated)
            {
                button4.Enabled = true;
                dLiteMazeCreated = false;

                if (!maze1.Equals(null))
                    for (int i = 0; i < MAZE_W; i++)
                    {
                        for (int j = 0; j < MAZE_H; j++)
                        {
                            this.Controls.Remove(maze1.cells[i, j]);
                        }
                    }
            }
            else
            {
                MessageBox.Show(" ایجاد نشده است " + " D*Lite " + " نقشه", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
