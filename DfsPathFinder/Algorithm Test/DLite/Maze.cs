using System;
using System.Collections.Generic;
using System.Text;

using java.awt;
using java.io;
using java.util;

namespace PathFinder.Algorithm_Test.DLite
{
    class Maze
    {
        private int size_x;
        private int size_y;
        public Node[,] cells;
        private Node original_start;
        public Node start;
        public Node goal;
        private Node robot_cell;
        private bool diagonal;
        private int n_directions;
        private int iteration;
        private Key.Key_comparator cde;
        private TreeSet u;

        private static int[] d_x = { 0, 1, -1, 0, 1, 1, -1, -1 };
        private static int[] d_y = { 1, 0, 0, -1, -1, 1, 1, -1 };

        private static String infinit = Convert.ToString('∞');

        private static Color path_color = new Color(100, 255, 255);
        private static Color wall_color = new Color(100, 100, 255);
        private static Color visited_cell_color = new Color(200, 150, 255);

        bool has_solution = false;

        private Maze()
        {
        }

        public Maze(long paramLong, int paramInt1, int paramInt2)
        {
            java.util.Random localRandom = new java.util.Random(paramLong);

            this.iteration = 0;
            this.diagonal = false;
            this.n_directions = 4;

            this.cde = new Key.Key_comparator();
            this.u = new TreeSet(this.cde);

            this.size_x = paramInt1;
            this.size_y = paramInt2;

            this.cells = new Node[paramInt1, paramInt2];
            for (int ii = 0; ii < paramInt1; ii++)
            {
                for (int jj = 0; jj < paramInt2; jj++)
                {
                    this.cells[ii, jj] = new Node(ii, jj);
                }
            }

            int i = (int)(localRandom.nextDouble() * 2147483647.0D) % paramInt1;
            int j = (int)(localRandom.nextDouble() * 2147483647.0D) % paramInt2;
            this.original_start = (this.robot_cell = this.start = this.cells[i, j]);
            do
            {
                i = (int)(localRandom.nextDouble() * 2147483647.0D) % paramInt1;
                j = (int)(localRandom.nextDouble() * 2147483647.0D) % paramInt2;
                this.goal = this.cells[i, j];
            } while ((this.start.X == this.goal.X) && (this.start.Y == this.goal.Y));

            for (i = 0; i < paramInt1; i++)
                for (j = 0; j < paramInt2; j++)
                {
                    this.cells[i, j].h = (Math.Abs(i - this.goal.X) + Math.Abs(j - this.goal.Y));
                    this.cells[i, j].iteration = this.iteration;

                    if ((this.cells[i, j] == this.start) || (this.cells[i, j] == this.goal) ||
                      (localRandom.nextDouble() > 0.25D)) continue;
                    this.cells[i, j].real_type = 1;
                }
        }

        public bool HasSolution()
        {
            return has_solution;
        }

        private void Copy_maze()
        {
            for (int i = 0; i < this.size_x; i++)
                for (int j = 0; j < this.size_y; j++)
                    this.cells[i, j].type_robot_vision = this.cells[i, j].real_type;
        }

        public void Set_diagonal(bool parambool)
        {
            this.diagonal = parambool;

            if (parambool)
                this.n_directions = 8;
            else
            {
                this.n_directions = 4;
            }

            Initialize();
        }

        public void Set_goal(int paramInt1, int paramInt2)
        {
            Point localPoint = Convert_coordinates_to_indexes(paramInt1, paramInt2);
            if (localPoint == null) return;
            paramInt1 = localPoint.x;
            paramInt2 = localPoint.y;

            if (this.cells[paramInt1, paramInt2] == this.start)
            {
                return;
            }
            this.goal = this.cells[paramInt1, paramInt2];
            this.goal.real_type = (this.goal.type_robot_vision = 0);

            Initialize();
        }

        public void Set_start(int paramInt1, int paramInt2)
        {
            Point localPoint = Convert_coordinates_to_indexes(paramInt1, paramInt2);
            if (localPoint == null) return;
            paramInt1 = localPoint.x;
            paramInt2 = localPoint.y;

            if (this.cells[paramInt1, paramInt2] == this.goal)
            {
                return;
            }
            this.robot_cell = (this.original_start = this.start = this.cells[paramInt1, paramInt2]);
            this.start.real_type = (this.start.type_robot_vision = 0);

            Initialize();
        }

        private Point Convert_coordinates_to_indexes(int paramInt1, int paramInt2)
        {
            if ((paramInt1 < 50) || (paramInt2 < 50)) return null;

            paramInt1 -= 50;
            paramInt2 -= 50;

            int i = paramInt1 / 50;
            int j = paramInt2 / 50;

            if ((0 > i) || (i >= this.size_x) || (0 > j) || (j >= this.size_y)) return null;

            Point localPoint = new Point(i, j);

            return localPoint;
        }

        public void Print_cell(int paramInt1, int paramInt2)
        {
            Point localPoint = Convert_coordinates_to_indexes(paramInt1, paramInt2);

            if (localPoint == null) return;

            int i = localPoint.x;
            int j = localPoint.y;
            Node localNode = this.cells[i, j];

            System.Console.WriteLine("I am " + localNode + " and my parent is " + localNode.parent);
        }

        public void Transform_cell(int paramInt1, int paramInt2)
        {
            Point localPoint = Convert_coordinates_to_indexes(paramInt1, paramInt2);

            if (localPoint == null) return;
            int i = localPoint.x;
            int j = localPoint.y;

            Node localNode = this.cells[i, j];

            if ((localNode != this.start) && (localNode != this.goal) && (localNode != this.robot_cell))
                if (localNode.real_type == 1)
                    localNode.real_type = 0;
                else
                    localNode.real_type = 1;
        }

        public Dimension Printed_maze_size()
        {
            return new Dimension(50 * (this.size_x + 1), 50 * (this.size_y + 1));
        }

        public String Get_stack()
        {
            String str = "";
            Iterator localIterator = this.u.iterator();

            while (localIterator.hasNext())
            {
                str = str + ((Key)localIterator.next()).toString() + "\n";
            }

            return str;
        }

        private int H(Node paramNode)
        {
            if (!this.diagonal)
            {
                return Math.Abs(paramNode.X - this.start.X) + Math.Abs(paramNode.Y - this.start.Y);
            }
            return Math.Max(Math.Abs(paramNode.X - this.start.X), Math.Abs(paramNode.Y - this.start.Y));
        }

        private void Clear_path()
        {
            for (int i = 0; i < this.size_x; i++)
                for (int j = 0; j < this.size_y; j++)
                    if (this.cells[i, j].type_robot_vision == 2)
                        this.cells[i, j].type_robot_vision = 0;
        }

        private bool Mark_path()
        {
            if (this.start.g == 2147483647) return false;

            has_solution = true;

            Object localObject = this.start;
            while (localObject != this.goal)
            {
                int i = ((Node)localObject).g;
                Node localNode = null;

                for (int j = 0; j < this.n_directions; j++)
                {
                    int k = ((Node)localObject).X + d_x[j];
                    int m = ((Node)localObject).Y + d_y[j];

                    if ((0 > k) || (k >= this.size_x) || (0 > m) || (m >= this.size_y) || (this.cells[k, m].type_robot_vision == 1))
                    {
                        continue;
                    }
                    if (this.cells[k, m].g < i)
                    {
                        i = this.cells[k, m].g;
                        localNode = this.cells[k, m];
                    }

                }

                ((Node)localObject).type_robot_vision = 2;
                localObject = localNode;
            }
            return true;
        }

        public void Initialize()
        {
            this.iteration = 0;

            Clear_path();
            Copy_maze();

            this.robot_cell = (this.start = this.original_start);

            for (int i = 0; i < this.size_x; i++)
            {
                for (int j = 0; j < this.size_y; j++)
                {
                    this.cells[i, j].g = (this.cells[i, j].rhs = 2147483647);
                    this.cells[i, j].h = H(this.cells[i, j]);
                    this.cells[i, j].iteration = this.iteration;
                    this.cells[i, j].parent = null;
                }
            }

            this.goal.rhs = 0;
            this.iteration += 1;
            this.u.clear();
            this.u.add(new Key(this.goal));
        }

        private void Update_cell(Node paramNode)
        {
            Node localNode = null;

            System.Console.WriteLine("Update_cell: " + paramNode);

            Key localKey = new Key(paramNode);
            paramNode.iteration = this.iteration;

            if (paramNode != this.goal)
            {
                paramNode.rhs = 2147483647;

                for (int i = 0; i < this.n_directions; i++)
                {
                    int j = paramNode.X + d_x[i];
                    int k = paramNode.Y + d_y[i];

                    if ((0 > j) || (j >= this.size_x) || (0 > k) || (k >= this.size_y) || (this.cells[j, k].type_robot_vision == 1))
                        continue;
                    int m;
                    if (this.cells[j, k].g == 2147483647) m = 2147483647;
                    else
                    {
                        m = this.cells[j, k].g + 1;
                    }
                    if (paramNode.rhs > m)
                    {
                        paramNode.rhs = m;
                        localNode = this.cells[j, k];
                    }
                }
            }

            this.u.remove(localKey);
            paramNode.parent = localNode;

            System.Console.WriteLine("New Parent: " + localNode);

            if (paramNode.g != paramNode.rhs)
                this.u.add(new Key(paramNode));
        }

        public bool Execution_end()
        {
            return (this.u.isEmpty()) || ((this.cde.compare(new Key(this.start), (Key)this.u.first()) <= 0) && (this.start.g == this.start.rhs));
        }

        public bool Reached_goal()
        {
            return this.robot_cell == this.goal;
        }

        public void Calculate_path(bool parambool)
        {
            while (!Execution_end())
            {
                Key localKey = (Key)this.u.first();
                this.u.remove(localKey);
                Node localNode = localKey.cell;

                localNode.iteration = this.iteration;
                int i;
                int j;
                int k;
                if (localNode.g > localNode.rhs)
                {
                    localNode.g = localNode.rhs;

                    if (localNode != this.start)
                    {
                        for (i = 0; i < this.n_directions; i++)
                        {
                            j = localNode.X + d_x[i];
                            k = localNode.Y + d_y[i];

                            if ((0 > j) || (j >= this.size_x) || (0 > k) || (k >= this.size_y) || (this.cells[j, k].type_robot_vision == 1) || (localNode.g + 1 >= this.cells[j, k].rhs))
                            {
                                continue;
                            }
                            Update_cell(this.cells[j, k]);
                        }
                    }
                }
                else
                {
                    localNode.g = 2147483647;

                    Update_cell(localNode);

                    for (i = 0; i < this.n_directions; i++)
                    {
                        j = localNode.X + d_x[i];
                        k = localNode.Y + d_y[i];

                        if ((0 > j) || (j >= this.size_x) || (0 > k) || (k >= this.size_y) || (this.cells[j, k].type_robot_vision == 1))
                        {
                            continue;
                        }
                        Update_cell(this.cells[j, k]);
                    }

                }

                if (parambool)
                    break;
            }
            if (Execution_end()) Mark_path();
        }

        public void Move(bool parambool)
        {
            int i = 2147483647;
            Node localNode1 = null;
            int j = 0;
            int i2;
            Node localNode2;
            for (int k = 0; k < 8; k++)
            {
                int n = this.robot_cell.X + d_x[k];
                i2 = this.robot_cell.Y + d_y[k];

                if ((0 > n) || (n >= this.size_x) || (0 > i2) || (i2 >= this.size_y) ||
                  (this.cells[n, i2].type_robot_vision == this.cells[n, i2].real_type) || (
                  (this.cells[n, i2].type_robot_vision == 2) && (this.cells[n, i2].real_type == 0)))
                {
                    continue;
                }
                localNode2 = this.cells[n, i2];
                j = 1;
                int i3;
                int i4;
                if (localNode2.type_robot_vision == 1)
                {
                    localNode2.type_robot_vision = 0;
                    localNode2.g = (localNode2.rhs = 2147483647);
                    localNode2.parent = null;

                    Update_cell(localNode2);
                    for (int i5 = 0; i5 < this.n_directions; i5++)
                    {
                        i3 = localNode2.X + d_x[i5];
                        i4 = localNode2.Y + d_y[i5];

                        if ((0 > i3) || (i3 >= this.size_x) || (0 > i4) || (i4 >= this.size_y) || (this.cells[i3, i4].type_robot_vision == 1) || (localNode2.g + 1 >= this.cells[i3, i4].rhs))
                        {
                            continue;
                        }
                        Update_cell(this.cells[i3, i4]);
                    }

                }
                else
                {
                    Key localKey2 = new Key(localNode2);

                    this.u.remove(localKey2);

                    localNode2.g = (localNode2.rhs = 2147483647);
                    localNode2.parent = null;
                    localNode2.type_robot_vision = 1;

                    for (int i6 = 0; i6 < this.n_directions; i6++)
                    {
                        i3 = localNode2.X + d_x[i6];
                        i4 = localNode2.Y + d_y[i6];

                        if ((0 > i3) || (i3 >= this.size_x) || (0 > i4) || (i4 >= this.size_y) || (this.cells[i3, i4].type_robot_vision == 1) || (this.cells[i3, i4].parent != localNode2))
                        {
                            continue;
                        }
                        Update_cell(this.cells[i3, i4]);
                    }

                }

            }

            if (j != 0)
            {
                Clear_path();
                this.start = this.robot_cell;
                this.iteration += 1;
                TreeSet localTreeSet = new TreeSet(this.cde);

                while (this.u.size() > 0)
                {
                    Key localKey1 = (Key)this.u.first();
                    this.u.remove(localKey1);
                    localNode2 = localKey1.cell;
                    localTreeSet.add(new Key(localNode2));
                }
                this.u = localTreeSet;
                Calculate_path(parambool);
                return;
            }

            for (int m = 0; m < this.n_directions; m++)
            {
                int i1 = this.robot_cell.X + d_x[m];
                i2 = this.robot_cell.Y + d_y[m];

                if ((0 > i1) || (i1 >= this.size_x) || (0 > i2) || (i2 >= this.size_y) || (this.cells[i1, i2].type_robot_vision == 1) || (this.cells[i1, i2].g >= i))
                    continue;
                i = this.cells[i1, i2].g;
                localNode1 = this.cells[i1, i2];
            }

            if (i != 2147483647)
            {
                Clear_path();
                this.start = (this.robot_cell = localNode1);
                Mark_path();
            }
        }


    }
}
