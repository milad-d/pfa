using System;
using System.Windows.Forms;

namespace PathFinder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/milad-d/pfa");
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void link_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/milad-d/pfa");
            }
            catch (Exception)
            {
            }
        }

        private void buttonSimulationForm_Click(object sender, EventArgs e)
        {
            var form = new SimulationForm();
            form.ShowDialog();
        }

        private void buttonTestForm_Click(object sender, EventArgs e)
        {
            var form = new TestForm();
            form.ShowDialog();
        }
    }
}
