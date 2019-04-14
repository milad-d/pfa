using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
                System.Diagnostics.Process.Start("Http://pfa.codeplex.com");
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void link_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("Http://pfa.codeplex.com");
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void buttonSimulationForm_Click(object sender, EventArgs e)
        {
            try
            {
                SimulationForm form = new SimulationForm();
                form.ShowDialog();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void buttonTestForm_Click(object sender, EventArgs e)
        {
            try
            {
                TestForm form = new TestForm();
                form.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
