using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

//using java;
//using java.awt.image;

namespace PathFinder.Algorithm_Test
{
    public class Maze
    {
        public static int N_DIRECTIONS_WITHOUT_DIAGONALS = 4;
        public static int N_DIRECTIONS_WITH_DIAGONALS = 8;
        public static int []delta_x = {0 , 1 , -1 ,  0 ,   1  , 1 , -1 , -1};
        public static int []delta_y = {1 , 0 ,  0 , -1 ,  -1  , 1 ,  1 , -1};

        private int w , h;
        public LightCell [,]cells;
        private LightCell start , goal;

        private Maze()
        {

        }

        public Maze(long seed , int w , int h , float probability_to_block_a_cell , int max_cost)
        {
	        Random random = new Random();//(int)seed);

	        if(w < 2 || h < 2 
                     || probability_to_block_a_cell > 1 
                     || probability_to_block_a_cell < 0 
                     || max_cost > 0x7F 
                     || max_cost < 1)
		        throw new ArgumentException();

	        this.w = w;
	        this.h = h;
	        cells = new LightCell[h, w];

	        for(int y = 0 ; y < h ; y++)
            {
		        for(int x = 0 ; x < w ; x++)
                {
			        bool blocked = (random.NextDouble() < probability_to_block_a_cell);
			        byte cost = (byte)(random.Next(max_cost) + 1);

                    cells[y, x] = new LightCell(x, y, cost);
			        if(blocked) cells[y, x].Block();
		        }
	        }

	        /* Choose the start cell. */
	        {
		        int x , y;
		        x = random.Next(w);
		        y = random.Next(h);
		        SetStart(x , y);
	        }

	        /* Choose the goal cell. */
	        do
            {
		        int x , y;
		        x = random.Next(w);
		        y = random.Next(h);
		        SetGoal(x , y);
	        }while(start == goal);
        }

        public Maze clone()
        {
	        Maze maze = new Maze();

	        maze.w = w;
	        maze.h = h;

	        maze.cells = new LightCell[h, w];
	        for(int y = 0 ; y < h ; y++)
            {
		        for(int x = 0 ; x < w ; x++)
                {
			        maze.cells[y, x] = cells[y, x].clone();
		        }
	        }

	        maze.start = maze.cells[start.Y, start.X];
	        maze.goal = maze.cells[goal.Y, goal.X];

	        return maze;
        }

        public void CleanPath()
        {
	        for(int y = 0 ; y < h ; y++)
            {
		        for(int x = 0 ; x < w ; x++)
                {
			        cells[y, x].ClearPathFlag();
		        }
	        }
        }

        /*public void Draw(System.Drawing.Graphics g2 , MazeDrawingOptions options , AStarContainer drawer)
        {
	        int o_x = 0 , o_y = 0 , size_with_border;
            System.Drawing.Point d;

	        size_with_border = options.cell_size + options.cell_border * 2;
	        d = GetDrawnMazeSize(w , h , options);

            SolidBrush brush = new SolidBrush(options.background_color);
	        g2.FillRectangle(brush, 0 , 0 , d.X , d.Y);

	        if(options.draw_coordinates)
            {
		        String str;
		        int x , y;

		        o_y = o_x = options.cell_border * 2 + options.cell_size;

		        //g2.setColor(options.border_color);
		        //g2.fillRect(0 , 0 , size_with_border , size_with_border);
                brush = new SolidBrush(options.border_color);
                g2.FillRectangle(brush, 0, 0, size_with_border, size_with_border);

		        for(int i = 1 ; i <= w ; i++)
                {
			        str = i.ToString();

                    brush = new SolidBrush(options.border_color);
                    g2.FillRectangle(brush, i * size_with_border, 0, size_with_border, size_with_border);
                    brush = new SolidBrush(options.background_color);
                    g2.FillRectangle(brush, i * size_with_border + options.cell_border, options.cell_border, options.cell_size, options.cell_size);

                    brush = new SolidBrush(options.border_color);
			        d = Misc.GetStringPosition(g2 , str , options.cell_size , options.cell_size);
			        x = i * size_with_border + options.cell_border + d.X;
			        y = options.cell_border + d.Y;
			        g2.DrawString(str,new System.Drawing.Font("Arial",10,FontStyle.Regular), brush, x , y);
		        }

		        for(int i = 1 ; i <= h ; i++)
                {
			        str = Convert.ToString((char)(i - 1 + 'A'));

                    brush = new SolidBrush(options.border_color);
                    g2.FillRectangle(brush, 0, i * size_with_border, size_with_border, size_with_border);
                    brush = new SolidBrush(options.background_color);
                    g2.FillRectangle(brush, options.cell_border, i * size_with_border + options.cell_border, options.cell_size, options.cell_size);

                    brush = new SolidBrush(options.border_color);
			        d = Misc.GetStringPosition(g2 , str , options.cell_size , options.cell_size);
			        x = options.cell_border + d.X;
			        y = i * size_with_border + options.cell_border + d.Y;
                    g2.DrawString(str, new System.Drawing.Font("Arial", 10, FontStyle.Regular), brush, x, y);
		        }
	        }

            {
                //BufferedImage buffer = new BufferedImage(options.cell_size, options.cell_size, BufferedImage.TYPE_INT_RGB);
                //Bitmap buffer = new Bitmap(options.cell_size, options.cell_size);
                //Graphics2D bg2 = (Graphics2D)buffer.getGraphics();

                System.Windows.Forms.PictureBox buffer = new System.Windows.Forms.PictureBox();
                buffer.Size=new Size(options.cell_size, options.cell_size);
                Graphics bg2 = buffer.CreateGraphics();
                
                //bg2.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {

                        brush = new SolidBrush(options.border_color);
                        g2.FillRectangle(brush, o_x + x * size_with_border, o_y + y * size_with_border, size_with_border, size_with_border);
                        brush = new SolidBrush(options.background_color);
                        g2.FillRectangle(brush, o_x + x * size_with_border + options.cell_border, o_y + y * size_with_border + options.cell_border,
                            options.cell_size, options.cell_size);
                        drawer.DrawMazeLightCell(bg2, options, cells[y,x]);
                        //g2.DrawImage(buffer.Image, o_x + x * size_with_border + options.cell_border,
                        //    o_y + y * size_with_border + options.cell_border);
                    }
                }
            }
        }*/

        public LightCell GetGoal()
        {
	        return goal;
        }

        public LightCell GetStart()
        {
	        return start;
        }

        public LightCell GetMazeCell(int x , int y)
        {
	        return cells[y, x];
        }

        public int GetH()
        {
	        return h;
        }

        public int GetW()
        {
	        return w;
        }

        public void SetGoal(LightCell maze_cell)
        {
	        goal = maze_cell;
	        if(maze_cell.IsBlocked()) maze_cell.SetCost(1);
        }

        public void SetStart(LightCell maze_cell)
        {
	        start = maze_cell;
	        if(maze_cell.IsBlocked()) maze_cell.SetCost(1);
        }

        public void SetGoal(int x , int y)
        {
	        SetGoal(cells[y, x]);
        }

        public void SetStart(int x , int y)
        {
	        SetStart(cells[y, x]);
        }

        public String toString()
        {
            String s = "W: " + w + " H: " + h + "\n";

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (cells[y, x] == goal)
                    {
                        s += "G";
                    }
                    else if (cells[y, x] == start)
                    {
                        s += "S";
                    }
                    else if (cells[y, x].IsBlocked())
                    {
                        s += "X";
                    }
                    else if (cells[y, x].IsPathFlagOn())
                    {
                        s += ".";
                    }
                    else
                    {
                        s += " ";
                    }
                }
                s += "\n";
            }
            return s;
        }
    }
}
