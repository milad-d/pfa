namespace PathFinder
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonTestForm = new System.Windows.Forms.Button();
            this.buttonSimulationForm = new System.Windows.Forms.Button();
            this.link = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // buttonTestForm
            // 
            this.buttonTestForm.Location = new System.Drawing.Point(169, 13);
            this.buttonTestForm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonTestForm.Name = "buttonTestForm";
            this.buttonTestForm.Size = new System.Drawing.Size(150, 130);
            this.buttonTestForm.TabIndex = 0;
            this.buttonTestForm.Text = "آزمایش الگوریتم ها";
            this.buttonTestForm.UseVisualStyleBackColor = true;
            this.buttonTestForm.Click += new System.EventHandler(this.buttonTestForm_Click);
            // 
            // buttonSimulationForm
            // 
            this.buttonSimulationForm.Location = new System.Drawing.Point(12, 13);
            this.buttonSimulationForm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonSimulationForm.Name = "buttonSimulationForm";
            this.buttonSimulationForm.Size = new System.Drawing.Size(150, 130);
            this.buttonSimulationForm.TabIndex = 1;
            this.buttonSimulationForm.Text = "شبیه سازی الگوریتم ها";
            this.buttonSimulationForm.UseVisualStyleBackColor = true;
            this.buttonSimulationForm.Click += new System.EventHandler(this.buttonSimulationForm_Click);
            // 
            // link
            // 
            this.link.Cursor = System.Windows.Forms.Cursors.Hand;
            this.link.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.link.Image = ((System.Drawing.Image)(resources.GetObject("link.Image")));
            this.link.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.link.Location = new System.Drawing.Point(12, 147);
            this.link.Name = "link";
            this.link.Size = new System.Drawing.Size(307, 184);
            this.link.TabIndex = 3;
            this.link.TabStop = true;
            this.link.Text = "سایت پروژه";
            this.link.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
            this.link.Click += new System.EventHandler(this.link_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(331, 354);
            this.Controls.Add(this.link);
            this.Controls.Add(this.buttonSimulationForm);
            this.Controls.Add(this.buttonTestForm);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "الگوریتم های مسیریابی";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonTestForm;
        private System.Windows.Forms.Button buttonSimulationForm;
        private System.Windows.Forms.LinkLabel link;
    }
}