using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using PathFinder.Heuristic_Algorithms;

namespace PathFinder.UnInformed_Algorithms
{
    class DFS
    {
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

        int w, h;
        bool mark_path;
        bool step_by_step;
        bool has_solution;
        Maze maze;

        public DFS(Maze maze, bool mark_path, bool step_by_step)
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
                    if (maze.GetGoal() == maze.cells[i, j])
                    {
                        maze.cells[i, j].BackColor = Color.Gold;
                    }
                    else if (maze.GetStart() == maze.cells[i, j])
                    {
                        maze.cells[i, j].BackColor = Color.Blue;
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
            {
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
                    maze.cells[points[Current].X, points[Current].Y].BackColor = Color.LightGreen;
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
                                    maze.cells[tempNode.X, tempNode.Y].BackColor = Color.Violet;
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
            }
        }
    }
}
