namespace PathFinder
{
    partial class SimulationForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimulationForm));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("انواع سلول", System.Windows.Forms.HorizontalAlignment.Right);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("عامل جستجوگر و هدف", System.Windows.Forms.HorizontalAlignment.Right);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("شروع", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("هدف", 1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("دیوار", 2);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("سلول با هزینه بالا", 3);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("سلول با هزینه متوسط", 4);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("سلول با هزینه پایین", 5);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.دخToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.بازکردنToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ذخیرهToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarBlockedPercent = new System.Windows.Forms.TrackBar();
            this.checkBoxDiagonalMove = new System.Windows.Forms.CheckBox();
            this.checkBoxHeuristic = new System.Windows.Forms.CheckBox();
            this.checkBoxUniformCost = new System.Windows.Forms.CheckBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ComboBoxTie_breaking = new System.Windows.Forms.ToolStripComboBox();
            this.toolAStar = new System.Windows.Forms.ToolStripButton();
            this.toolGAAStar = new System.Windows.Forms.ToolStripButton();
            this.toolDStarLite = new System.Windows.Forms.ToolStripButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonToolHide = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBlockedPercent)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.دخToolStripMenuItem,
            this.restartToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 54);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // دخToolStripMenuItem
            // 
            this.دخToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.بازکردنToolStripMenuItem,
            this.ذخیرهToolStripMenuItem});
            this.دخToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.دخToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("دخToolStripMenuItem.Image")));
            this.دخToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.دخToolStripMenuItem.Name = "دخToolStripMenuItem";
            this.دخToolStripMenuItem.Size = new System.Drawing.Size(100, 50);
            this.دخToolStripMenuItem.Text = "نقشه";
            // 
            // بازکردنToolStripMenuItem
            // 
            this.بازکردنToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.بازکردنToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("بازکردنToolStripMenuItem.Image")));
            this.بازکردنToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.بازکردنToolStripMenuItem.Name = "بازکردنToolStripMenuItem";
            this.بازکردنToolStripMenuItem.Size = new System.Drawing.Size(184, 54);
            this.بازکردنToolStripMenuItem.Text = "باز کردن";
            this.بازکردنToolStripMenuItem.Click += new System.EventHandler(this.بازکردنToolStripMenuItem_Click);
            // 
            // ذخیرهToolStripMenuItem
            // 
            this.ذخیرهToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ذخیرهToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ذخیرهToolStripMenuItem.Image")));
            this.ذخیرهToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ذخیرهToolStripMenuItem.Name = "ذخیرهToolStripMenuItem";
            this.ذخیرهToolStripMenuItem.Size = new System.Drawing.Size(184, 54);
            this.ذخیرهToolStripMenuItem.Text = "ذخیره";
            this.ذخیرهToolStripMenuItem.Click += new System.EventHandler(this.ذخیرهToolStripMenuItem_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.restartToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("restartToolStripMenuItem.Image")));
            this.restartToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(132, 50);
            this.restartToolStripMenuItem.Text = "اجرای مجدد";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.tabControl1.Location = new System.Drawing.Point(0, 54);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.ShowToolTips = true;
            this.tabControl1.Size = new System.Drawing.Size(784, 105);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.MouseHover += new System.EventHandler(this.tabControl1_MouseHover);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabPage1.Size = new System.Drawing.Size(776, 76);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "جستجوهای ناآگاهانه";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.toolStripButton1,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(770, 70);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(124, 67);
            this.toolStripLabel2.Text = "جستجوهای ناآگاهانه";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(60, 67);
            this.toolStripButton1.Text = "اول عمق";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(63, 67);
            this.toolStripButton3.Text = "اول سطح";
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.trackBarBlockedPercent);
            this.tabPage2.Controls.Add(this.checkBoxDiagonalMove);
            this.tabPage2.Controls.Add(this.checkBoxHeuristic);
            this.tabPage2.Controls.Add(this.checkBoxUniformCost);
            this.tabPage2.Controls.Add(this.toolStrip2);
            this.tabPage2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(776, 76);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "جستجوهای آگاهانه";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "la2";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "درصد احتمال مسدود بودن سلول:";
            // 
            // trackBarBlockedPercent
            // 
            this.trackBarBlockedPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarBlockedPercent.BackColor = System.Drawing.Color.White;
            this.trackBarBlockedPercent.Location = new System.Drawing.Point(-4, 26);
            this.trackBarBlockedPercent.Maximum = 100;
            this.trackBarBlockedPercent.Name = "trackBarBlockedPercent";
            this.trackBarBlockedPercent.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBarBlockedPercent.Size = new System.Drawing.Size(219, 45);
            this.trackBarBlockedPercent.TabIndex = 6;
            this.trackBarBlockedPercent.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarBlockedPercent.Value = 25;
            this.trackBarBlockedPercent.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // checkBoxDiagonalMove
            // 
            this.checkBoxDiagonalMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDiagonalMove.AutoSize = true;
            this.checkBoxDiagonalMove.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.checkBoxDiagonalMove.Location = new System.Drawing.Point(231, 52);
            this.checkBoxDiagonalMove.Name = "checkBoxDiagonalMove";
            this.checkBoxDiagonalMove.Size = new System.Drawing.Size(92, 20);
            this.checkBoxDiagonalMove.TabIndex = 5;
            this.checkBoxDiagonalMove.Text = "حرکت مورب";
            this.checkBoxDiagonalMove.UseVisualStyleBackColor = true;
            // 
            // checkBoxHeuristic
            // 
            this.checkBoxHeuristic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxHeuristic.AutoSize = true;
            this.checkBoxHeuristic.Checked = true;
            this.checkBoxHeuristic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHeuristic.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.checkBoxHeuristic.Location = new System.Drawing.Point(231, 26);
            this.checkBoxHeuristic.Name = "checkBoxHeuristic";
            this.checkBoxHeuristic.Size = new System.Drawing.Size(92, 20);
            this.checkBoxHeuristic.TabIndex = 4;
            this.checkBoxHeuristic.Text = "کشف کننده";
            this.checkBoxHeuristic.UseVisualStyleBackColor = true;
            // 
            // checkBoxUniformCost
            // 
            this.checkBoxUniformCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxUniformCost.AutoSize = true;
            this.checkBoxUniformCost.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.checkBoxUniformCost.Location = new System.Drawing.Point(224, 3);
            this.checkBoxUniformCost.Name = "checkBoxUniformCost";
            this.checkBoxUniformCost.Size = new System.Drawing.Size(99, 20);
            this.checkBoxUniformCost.TabIndex = 3;
            this.checkBoxUniformCost.Text = "هزینه یکسان";
            this.checkBoxUniformCost.UseVisualStyleBackColor = true;
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.BackColor = System.Drawing.Color.White;
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.ComboBoxTie_breaking,
            this.toolAStar,
            this.toolGAAStar,
            this.toolDStarLite});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(770, 71);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(133, 68);
            this.toolStripLabel1.Text = "استراتژِی گره شکنی : ";
            // 
            // ComboBoxTie_breaking
            // 
            this.ComboBoxTie_breaking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxTie_breaking.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.ComboBoxTie_breaking.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ComboBoxTie_breaking.Items.AddRange(new object[] {
            "بدون شکست",
            "g بالاترین مقدار",
            "g پایین ترین مقدار"});
            this.ComboBoxTie_breaking.Name = "ComboBoxTie_breaking";
            this.ComboBoxTie_breaking.Size = new System.Drawing.Size(150, 71);
            // 
            // toolAStar
            // 
            this.toolAStar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.toolAStar.Image = ((System.Drawing.Image)(resources.GetObject("toolAStar.Image")));
            this.toolAStar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolAStar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAStar.Name = "toolAStar";
            this.toolAStar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolAStar.Size = new System.Drawing.Size(52, 68);
            this.toolAStar.Text = "A*";
            this.toolAStar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolAStar.Click += new System.EventHandler(this.toolAStar_Click);
            // 
            // toolGAAStar
            // 
            this.toolGAAStar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.toolGAAStar.Image = ((System.Drawing.Image)(resources.GetObject("toolGAAStar.Image")));
            this.toolGAAStar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolGAAStar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGAAStar.Name = "toolGAAStar";
            this.toolGAAStar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolGAAStar.Size = new System.Drawing.Size(52, 68);
            this.toolGAAStar.Text = "GAA*";
            this.toolGAAStar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolGAAStar.Click += new System.EventHandler(this.toolGAAStar_Click);
            // 
            // toolDStarLite
            // 
            this.toolDStarLite.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.toolDStarLite.Image = ((System.Drawing.Image)(resources.GetObject("toolDStarLite.Image")));
            this.toolDStarLite.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolDStarLite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDStarLite.Name = "toolDStarLite";
            this.toolDStarLite.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolDStarLite.Size = new System.Drawing.Size(52, 68);
            this.toolDStarLite.Text = "D*Lite";
            this.toolDStarLite.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolDStarLite.Click += new System.EventHandler(this.toolDStarLite_Click);
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Left;
            listViewGroup1.Header = "انواع سلول";
            listViewGroup1.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "عامل جستجوگر و هدف";
            listViewGroup2.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            listViewGroup2.Name = "listViewGroup2";
            this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            listViewItem1.Group = listViewGroup2;
            listViewItem2.Group = listViewGroup2;
            listViewItem3.Group = listViewGroup1;
            listViewItem3.ToolTipText = "نمی توان وارد این سلول شد";
            listViewItem4.Group = listViewGroup1;
            listViewItem4.ToolTipText = "هزینه عبور از این سلول بالا زیاد است";
            listViewItem5.Group = listViewGroup1;
            listViewItem5.ToolTipText = "هزینه این نوع سلول بین کم و زیاد است";
            listViewItem6.Group = listViewGroup1;
            listViewItem6.ToolTipText = "هزینه عبور از این سلول بالا کم است";
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6});
            this.listView1.LargeImageList = this.imageList2;
            this.listView1.Location = new System.Drawing.Point(0, 159);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(217, 443);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "EVE-icon.png");
            this.imageList2.Images.SetKeyName(1, "home_48.png");
            this.imageList2.Images.SetKeyName(2, "wall_red.png");
            this.imageList2.Images.SetKeyName(3, "t114.jpg");
            this.imageList2.Images.SetKeyName(4, "t13.jpg");
            this.imageList2.Images.SetKeyName(5, "freeCel.png");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "EVE-icon.png");
            this.imageList1.Images.SetKeyName(1, "home_48.png");
            this.imageList1.Images.SetKeyName(2, "wall_red.png");
            this.imageList1.Images.SetKeyName(3, "t114.jpg");
            this.imageList1.Images.SetKeyName(4, "t13.jpg");
            this.imageList1.Images.SetKeyName(5, "freeCel.png");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(321, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 5;
            this.button1.Text = "نقشه جدید";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(397, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 6;
            this.button2.Text = "حذف نقشه قبلی";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 490);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.richTextBox1.Size = new System.Drawing.Size(217, 112);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            this.richTextBox1.Visible = false;
            // 
            // buttonToolHide
            // 
            this.buttonToolHide.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonToolHide.BackgroundImage")));
            this.buttonToolHide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonToolHide.Location = new System.Drawing.Point(259, 52);
            this.buttonToolHide.Name = "buttonToolHide";
            this.buttonToolHide.Size = new System.Drawing.Size(25, 25);
            this.buttonToolHide.TabIndex = 8;
            this.buttonToolHide.UseVisualStyleBackColor = true;
            this.buttonToolHide.Click += new System.EventHandler(this.buttonToolHide_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(607, 53);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "نقشه D*Lite";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(686, 53);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 12;
            this.button6.Text = "حذف نقشه D*Lite";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(331, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "مخصوص سایر الگوریتم ها";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(618, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "مخصوص الگوریتم D*Lite";
            // 
            // SimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 602);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.buttonToolHide);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SimulationForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "الگوریتم های مسیر یابی";
            this.Load += new System.EventHandler(this.SimulationForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBlockedPercent)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem دخToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem بازکردنToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ذخیرهToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolAStar;
        private System.Windows.Forms.ToolStripButton toolDStarLite;
        private System.Windows.Forms.ToolStripButton toolGAAStar;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox ComboBoxTie_breaking;
        private System.Windows.Forms.CheckBox checkBoxUniformCost;
        private System.Windows.Forms.CheckBox checkBoxHeuristic;
        private System.Windows.Forms.CheckBox checkBoxDiagonalMove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarBlockedPercent;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Button buttonToolHide;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

    }
}

