namespace PathFinder
{
    partial class Cell
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SetStart = new System.Windows.Forms.ToolStripMenuItem();
            this.SetEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetStart,
            this.SetEnd});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(102, 48);
            // 
            // SetStart
            // 
            this.SetStart.Name = "SetStart";
            this.SetStart.Size = new System.Drawing.Size(101, 22);
            this.SetStart.Text = "شروع";
            this.SetStart.Click += new System.EventHandler(this.SetStart_Click);
            // 
            // SetEnd
            // 
            this.SetEnd.Name = "SetEnd";
            this.SetEnd.Size = new System.Drawing.Size(101, 22);
            this.SetEnd.Text = "پایان";
            this.SetEnd.Click += new System.EventHandler(this.SetEnd_Click);
            // 
            // Cell
            // 
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Size = new System.Drawing.Size(23, 23);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Cell_MouseDoubleClick);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem SetStart;
        private System.Windows.Forms.ToolStripMenuItem SetEnd;
    }
}
