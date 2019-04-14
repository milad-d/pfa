namespace PathFinder
{
    partial class TestForm
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
            this.buttonStartTest = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonClear = new System.Windows.Forms.Button();
            this.checkBoxBlockSomeCell = new System.Windows.Forms.CheckBox();
            this.checkBoxUnBlock = new System.Windows.Forms.CheckBox();
            this.checkBoxMovingTarget = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericStep = new System.Windows.Forms.NumericUpDown();
            this.numericNChange = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericMazeH = new System.Windows.Forms.NumericUpDown();
            this.numericMazeW = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboTieBreaking = new System.Windows.Forms.ComboBox();
            this.buttonGAATest = new System.Windows.Forms.Button();
            this.buttonBfsTest = new System.Windows.Forms.Button();
            this.buttonTestRuntimeBFS = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonTestRuntimeDLite_Heap = new System.Windows.Forms.Button();
            this.buttonTestRuntimeDLite = new System.Windows.Forms.Button();
            this.buttonTestRuntimeGAA = new System.Windows.Forms.Button();
            this.buttonTestRuntimeAStar = new System.Windows.Forms.Button();
            this.buttonTestRuntimeDFS = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMazeH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMazeW)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStartTest
            // 
            this.buttonStartTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartTest.Location = new System.Drawing.Point(651, 102);
            this.buttonStartTest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonStartTest.Name = "buttonStartTest";
            this.buttonStartTest.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonStartTest.Size = new System.Drawing.Size(107, 28);
            this.buttonStartTest.TabIndex = 0;
            this.buttonStartTest.Text = "محاسبات  *A";
            this.buttonStartTest.UseVisualStyleBackColor = true;
            this.buttonStartTest.Click += new System.EventHandler(this.buttonStartTest_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(16, 262);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.richTextBox1.Size = new System.Drawing.Size(760, 314);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(16, 223);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(87, 28);
            this.buttonClear.TabIndex = 2;
            this.buttonClear.Text = "پاک کردن";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxBlockSomeCell
            // 
            this.checkBoxBlockSomeCell.AutoSize = true;
            this.checkBoxBlockSomeCell.Checked = true;
            this.checkBoxBlockSomeCell.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBlockSomeCell.Location = new System.Drawing.Point(667, 32);
            this.checkBoxBlockSomeCell.Name = "checkBoxBlockSomeCell";
            this.checkBoxBlockSomeCell.Size = new System.Drawing.Size(85, 20);
            this.checkBoxBlockSomeCell.TabIndex = 3;
            this.checkBoxBlockSomeCell.Text = "بلوکه کردن";
            this.checkBoxBlockSomeCell.UseVisualStyleBackColor = true;
            // 
            // checkBoxUnBlock
            // 
            this.checkBoxUnBlock.AutoSize = true;
            this.checkBoxUnBlock.Checked = true;
            this.checkBoxUnBlock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUnBlock.Location = new System.Drawing.Point(587, 32);
            this.checkBoxUnBlock.Name = "checkBoxUnBlock";
            this.checkBoxUnBlock.Size = new System.Drawing.Size(74, 20);
            this.checkBoxUnBlock.TabIndex = 4;
            this.checkBoxUnBlock.Text = "آزاد کردن";
            this.checkBoxUnBlock.UseVisualStyleBackColor = true;
            // 
            // checkBoxMovingTarget
            // 
            this.checkBoxMovingTarget.AutoSize = true;
            this.checkBoxMovingTarget.Location = new System.Drawing.Point(485, 32);
            this.checkBoxMovingTarget.Name = "checkBoxMovingTarget";
            this.checkBoxMovingTarget.Size = new System.Drawing.Size(96, 20);
            this.checkBoxMovingTarget.TabIndex = 5;
            this.checkBoxMovingTarget.Text = "هدف متحرک";
            this.checkBoxMovingTarget.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(317, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "تغییر سلول در هر چند گام؟";
            // 
            // numericStep
            // 
            this.numericStep.Location = new System.Drawing.Point(270, 31);
            this.numericStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericStep.Name = "numericStep";
            this.numericStep.Size = new System.Drawing.Size(41, 23);
            this.numericStep.TabIndex = 7;
            this.numericStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericStep.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericNChange
            // 
            this.numericNChange.Location = new System.Drawing.Point(82, 31);
            this.numericNChange.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericNChange.Name = "numericNChange";
            this.numericNChange.Size = new System.Drawing.Size(41, 23);
            this.numericNChange.TabIndex = 9;
            this.numericNChange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericNChange.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(129, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "تعداد سلول های متغیر:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(686, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "اندازه ماز : ";
            // 
            // numericMazeH
            // 
            this.numericMazeH.Location = new System.Drawing.Point(501, 65);
            this.numericMazeH.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericMazeH.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericMazeH.Name = "numericMazeH";
            this.numericMazeH.Size = new System.Drawing.Size(41, 23);
            this.numericMazeH.TabIndex = 12;
            this.numericMazeH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericMazeH.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // numericMazeW
            // 
            this.numericMazeW.Location = new System.Drawing.Point(602, 65);
            this.numericMazeW.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericMazeW.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericMazeW.Name = "numericMazeW";
            this.numericMazeW.Size = new System.Drawing.Size(41, 23);
            this.numericMazeW.TabIndex = 11;
            this.numericMazeW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericMazeW.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(649, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "طول";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(548, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "عرض";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(319, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(153, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "استراتژی شکست اتصال : ";
            // 
            // comboTieBreaking
            // 
            this.comboTieBreaking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTieBreaking.FormattingEnabled = true;
            this.comboTieBreaking.Items.AddRange(new object[] {
            "بالا",
            "پایین",
            "انجام ندادن"});
            this.comboTieBreaking.Location = new System.Drawing.Point(195, 64);
            this.comboTieBreaking.Name = "comboTieBreaking";
            this.comboTieBreaking.Size = new System.Drawing.Size(121, 24);
            this.comboTieBreaking.TabIndex = 16;
            this.comboTieBreaking.SelectedIndexChanged += new System.EventHandler(this.comboTieBreaking_SelectedIndexChanged);
            // 
            // buttonGAATest
            // 
            this.buttonGAATest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGAATest.Location = new System.Drawing.Point(538, 102);
            this.buttonGAATest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonGAATest.Name = "buttonGAATest";
            this.buttonGAATest.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonGAATest.Size = new System.Drawing.Size(107, 28);
            this.buttonGAATest.TabIndex = 17;
            this.buttonGAATest.Text = "*GAA";
            this.buttonGAATest.UseVisualStyleBackColor = true;
            this.buttonGAATest.Click += new System.EventHandler(this.buttonGAATest_Click);
            // 
            // buttonBfsTest
            // 
            this.buttonBfsTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBfsTest.Location = new System.Drawing.Point(427, 102);
            this.buttonBfsTest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonBfsTest.Name = "buttonBfsTest";
            this.buttonBfsTest.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonBfsTest.Size = new System.Drawing.Size(107, 28);
            this.buttonBfsTest.TabIndex = 18;
            this.buttonBfsTest.Text = "اول سطح (BFS)";
            this.buttonBfsTest.UseVisualStyleBackColor = true;
            this.buttonBfsTest.Visible = false;
            this.buttonBfsTest.Click += new System.EventHandler(this.buttonBfsTest_Click);
            // 
            // buttonTestRuntimeBFS
            // 
            this.buttonTestRuntimeBFS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTestRuntimeBFS.Location = new System.Drawing.Point(653, 23);
            this.buttonTestRuntimeBFS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonTestRuntimeBFS.Name = "buttonTestRuntimeBFS";
            this.buttonTestRuntimeBFS.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonTestRuntimeBFS.Size = new System.Drawing.Size(107, 28);
            this.buttonTestRuntimeBFS.TabIndex = 19;
            this.buttonTestRuntimeBFS.Text = "اول سطح (BFS)";
            this.buttonTestRuntimeBFS.UseVisualStyleBackColor = true;
            this.buttonTestRuntimeBFS.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonTestRuntimeDLite_Heap);
            this.groupBox1.Controls.Add(this.buttonTestRuntimeDLite);
            this.groupBox1.Controls.Add(this.buttonTestRuntimeGAA);
            this.groupBox1.Controls.Add(this.buttonTestRuntimeAStar);
            this.groupBox1.Controls.Add(this.buttonTestRuntimeDFS);
            this.groupBox1.Controls.Add(this.buttonTestRuntimeBFS);
            this.groupBox1.Location = new System.Drawing.Point(12, 153);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(764, 63);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "آزمون زمان اجرا";
            // 
            // buttonTestRuntimeDLite_Heap
            // 
            this.buttonTestRuntimeDLite_Heap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTestRuntimeDLite_Heap.Location = new System.Drawing.Point(88, 23);
            this.buttonTestRuntimeDLite_Heap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonTestRuntimeDLite_Heap.Name = "buttonTestRuntimeDLite_Heap";
            this.buttonTestRuntimeDLite_Heap.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonTestRuntimeDLite_Heap.Size = new System.Drawing.Size(107, 28);
            this.buttonTestRuntimeDLite_Heap.TabIndex = 24;
            this.buttonTestRuntimeDLite_Heap.Text = "D*Lite-Heap";
            this.buttonTestRuntimeDLite_Heap.UseVisualStyleBackColor = true;
            this.buttonTestRuntimeDLite_Heap.Visible = false;
            this.buttonTestRuntimeDLite_Heap.Click += new System.EventHandler(this.buttonTestRuntimeDLite_Heap_Click);
            // 
            // buttonTestRuntimeDLite
            // 
            this.buttonTestRuntimeDLite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTestRuntimeDLite.Location = new System.Drawing.Point(201, 23);
            this.buttonTestRuntimeDLite.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonTestRuntimeDLite.Name = "buttonTestRuntimeDLite";
            this.buttonTestRuntimeDLite.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonTestRuntimeDLite.Size = new System.Drawing.Size(107, 28);
            this.buttonTestRuntimeDLite.TabIndex = 23;
            this.buttonTestRuntimeDLite.Text = "D*Lite";
            this.buttonTestRuntimeDLite.UseVisualStyleBackColor = true;
            this.buttonTestRuntimeDLite.Click += new System.EventHandler(this.buttonTestRuntimeDLite_Click);
            // 
            // buttonTestRuntimeGAA
            // 
            this.buttonTestRuntimeGAA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTestRuntimeGAA.Location = new System.Drawing.Point(314, 23);
            this.buttonTestRuntimeGAA.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonTestRuntimeGAA.Name = "buttonTestRuntimeGAA";
            this.buttonTestRuntimeGAA.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonTestRuntimeGAA.Size = new System.Drawing.Size(107, 28);
            this.buttonTestRuntimeGAA.TabIndex = 22;
            this.buttonTestRuntimeGAA.Text = "GAA*";
            this.buttonTestRuntimeGAA.UseVisualStyleBackColor = true;
            this.buttonTestRuntimeGAA.Click += new System.EventHandler(this.buttonTestRuntimeGAA_Click);
            // 
            // buttonTestRuntimeAStar
            // 
            this.buttonTestRuntimeAStar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTestRuntimeAStar.Location = new System.Drawing.Point(427, 23);
            this.buttonTestRuntimeAStar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonTestRuntimeAStar.Name = "buttonTestRuntimeAStar";
            this.buttonTestRuntimeAStar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonTestRuntimeAStar.Size = new System.Drawing.Size(107, 28);
            this.buttonTestRuntimeAStar.TabIndex = 21;
            this.buttonTestRuntimeAStar.Text = "A*";
            this.buttonTestRuntimeAStar.UseVisualStyleBackColor = true;
            this.buttonTestRuntimeAStar.Click += new System.EventHandler(this.buttonTestRuntimeAStar_Click);
            // 
            // buttonTestRuntimeDFS
            // 
            this.buttonTestRuntimeDFS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTestRuntimeDFS.Location = new System.Drawing.Point(540, 23);
            this.buttonTestRuntimeDFS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonTestRuntimeDFS.Name = "buttonTestRuntimeDFS";
            this.buttonTestRuntimeDFS.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonTestRuntimeDFS.Size = new System.Drawing.Size(107, 28);
            this.buttonTestRuntimeDFS.TabIndex = 20;
            this.buttonTestRuntimeDFS.Text = "اول عمق (DFS)";
            this.buttonTestRuntimeDFS.UseVisualStyleBackColor = true;
            this.buttonTestRuntimeDFS.Click += new System.EventHandler(this.buttonTestRuntimeDFS_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonStartTest);
            this.groupBox2.Controls.Add(this.buttonBfsTest);
            this.groupBox2.Controls.Add(this.comboTieBreaking);
            this.groupBox2.Controls.Add(this.buttonGAATest);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.checkBoxBlockSomeCell);
            this.groupBox2.Controls.Add(this.numericMazeH);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.checkBoxUnBlock);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numericMazeW);
            this.groupBox2.Controls.Add(this.checkBoxMovingTarget);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numericStep);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numericNChange);
            this.groupBox2.Location = new System.Drawing.Point(12, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(764, 137);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "آزمون تعداد جستجو ها و زمان رسیدن به هدف";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 580);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 22;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(46, 17);
            this.toolStripStatusLabel1.Text = "زمان : ";
            // 
            // toolStripTime
            // 
            this.toolStripTime.Name = "toolStripTime";
            this.toolStripTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripTime.Size = new System.Drawing.Size(26, 17);
            this.toolStripTime.Text = "0.0";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(355, 223);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(421, 28);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 23;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(109, 223);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 28);
            this.button1.TabIndex = 24;
            this.button1.Text = "توقف الگوریتم";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 602);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.richTextBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "TestForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "آزمایش الگوریتم ها";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMazeH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMazeW)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStartTest;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.CheckBox checkBoxBlockSomeCell;
        private System.Windows.Forms.CheckBox checkBoxUnBlock;
        private System.Windows.Forms.CheckBox checkBoxMovingTarget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericStep;
        private System.Windows.Forms.NumericUpDown numericNChange;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericMazeH;
        private System.Windows.Forms.NumericUpDown numericMazeW;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboTieBreaking;
        private System.Windows.Forms.Button buttonGAATest;
        private System.Windows.Forms.Button buttonBfsTest;
        private System.Windows.Forms.Button buttonTestRuntimeBFS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonTestRuntimeDLite;
        private System.Windows.Forms.Button buttonTestRuntimeGAA;
        private System.Windows.Forms.Button buttonTestRuntimeAStar;
        private System.Windows.Forms.Button buttonTestRuntimeDFS;
        private System.Windows.Forms.Button buttonTestRuntimeDLite_Heap;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripTime;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
    }
}