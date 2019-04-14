using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace PathFinder.Algorithm_Test
{
    class BFS
    {
        // لیستی از سلولهای مسدود
        List<Point> blocked = new List<Point>();
        // فضای جستجو متشکل از سلولهای مربعی شکل
        //Cell[,] Grid = new Cell[size, size];
        //نقطه شروع
        public LightCell start = new LightCell();
        // نقطه پایان
        public LightCell end = new LightCell();
        // لیستی از نقاط گسترش داده فعلی
        List<LightCell> points = new List<LightCell>();

        public List<LightCell> path = new List<LightCell>();
        
        int w, h;
        bool mark_path;
        bool step_by_step;
        bool has_solution;
        Maze maze;

        public BFS(Maze maze, bool mark_path, bool step_by_step)
        {
            h = maze.GetH();
            w = maze.GetW();

            has_solution = true;
            this.mark_path = mark_path;
            this.step_by_step = step_by_step;
            this.maze = maze;


            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    switch (maze.cells[i, j].GetCost())
                    {
                        case 0x7F:
                            maze.cells[i, j].blocked = true;
                            blocked.Add(new Point(i, j));
                            break;
                        default:
                            break;
                    }
                }
            }


            this.start.X = maze.GetStart().Y; 
            this.start.Y = maze.GetStart().X;
            this.end.X = maze.GetGoal().Y;
            this.end.Y = maze.GetGoal().X;
        }

        public bool HasSolution()
        {
            return has_solution;
        }

        public void Solve()
        {
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
            points.Add(new LightCell(start.X, start.Y, Current));

            //تا زمانی که نقطه فعلی به هدف نرسیده است دستورات این حلقه را اجرا کن
            while (!(points[Current].X == end.X && points[Current].Y == end.Y))
            {
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
                            LightCell  tempNode = new LightCell ();
                            tempNode = new LightCell (points[Current].X + x,
                                                points[Current].Y + y,
                                                Current);

                            // بررسی اینکه آیا نقطه از صفحه خارج شده است یا خیر
                            // اگر پاسخ مثبت است یک واحد به مقدار شمارنده آزمایش اضافه کن
                            if ((tempNode.X < 0) ||
                                (tempNode.Y < 0) ||
                                (tempNode.X > h - 1) ||
                                (tempNode.Y > w - 1))
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

                                tempCount++;
                            }

                        }
                    }
                }

                Current++;
                tempCount--;

                if (Current == points.Count)
                {
                    has_solution = false;
                    break;
                }
            }
            // تا این جا با گسترش دادن نقاط و جستجو به هدف رسیده ایم
            // از این پس باید مسیر رسیدن به هدف را رسم کنیم
            // مسیری که پیدا میکنیم لزوماً بهینه نیست و فقط یک مسیر از شروع به هدف است


            if (has_solution)
            {
                // شمارنده ای برای حرکت در مسیر رسیدن به هدف
                int counter = 0;
                path = new List<LightCell>();
                //تا زمانیکه والد سلول فعلی صفر نشده یعنی تا به سلول شروع نرسیده ای
                //سلول را به مسیر اضافه کن
                while (Current != 0)
                {
                    path.Add(new LightCell());
                    path[counter] = new LightCell(points[Current].X, points[Current].Y,0);
                    Current = points[Current].parentCount;
                    counter++;
                }

                // سلول شروع را به مسیر اضافه کن
                path.Add(start);
                path[counter] = start;
            }
        }

        public void Solve2()
        {
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
            //points.Add(new LightCell (start.X, start.Y, Current));
            points.Add(new LightCell(maze.GetStart(), Current));

            //تا زمانی که نقطه فعلی به هدف نرسیده است دستورات این حلقه را اجرا کن
            while (!(points[Current].X == end.X && points[Current].Y == end.Y))
            {
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
                            LightCell  tempNode = new LightCell ();
                            tempNode = new LightCell (points[Current].X + x,
                                                points[Current].Y + y,
                                                Current);

                            // بررسی اینکه آیا نقطه از صفحه خارج شده است یا خیر
                            // اگر پاسخ مثبت است یک واحد به مقدار شمارنده آزمایش اضافه کن
                            if ((tempNode.X < 0) ||
                                (tempNode.Y < 0) ||
                                (tempNode.X > h - 1) ||
                                (tempNode.Y > w - 1))
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

                                tempCount++;
                            }

                        }
                    }
                }

                Current++;
                tempCount--;

                if (Current == points.Count)
                {
                    has_solution = false;
                    break;
                }
            }
            // تا این جا با گسترش دادن نقاط و جستجو به هدف رسیده ایم
            // از این پس باید مسیر رسیدن به هدف را رسم کنیم
            // مسیری که پیدا میکنیم لزوماً بهینه نیست و فقط یک مسیر از شروع به هدف است


            if (has_solution)
            {
                // شمارنده ای برای حرکت در مسیر رسیدن به هدف
                int counter = 0;
                path = new List<LightCell>();
                LightCell node;
                LightCell child;
                //تا زمانیکه والد سلول فعلی صفر نشده یعنی تا به سلول شروع نرسیده ای
                //سلول را به مسیر اضافه کن
                while (Current != 0)
                {
                    path.Add(new LightCell());
                    path[counter] = new LightCell(points[Current].X, points[Current].Y,0);
                    Current = points[Current].parentCount;
                    counter++;

                    points[Current].SetNextMazeCell(points[points[Current].parentCount]);
                    //node = new LightCell(path[counter-1].X, path[counter-1].Y, 1);
                    //child = new LightCell(points[Current].X, points[Current].Y, node);
                    //node.SetNextMazeCell(child);
                    //node.SetPathFlag();


                    /*node.GetMazeLightCell().SetPathFlag();
                    node_child = node;
                    node = node.parent;

                    do
                    {
                        node.GetMazeLightCell().SetNextMazeCell(node_child.GetMazeLightCell());
                        node.GetMazeLightCell().SetPathFlag();
                        node_child = node;
                        node = node.parent;
                    } while (node != null);*/

                }

                // سلول شروع را به مسیر اضافه کن
                path.Add(start);
                path[counter] = start;

                for (int i = 0; i < path.Count - 1; i++)
                {
                    path[i].SetPathFlag();
                    path[i].SetNextMazeCell(path[i + 1]);
                }
                path[path.Count - 1].SetPathFlag();
            }
        }

    }
}
