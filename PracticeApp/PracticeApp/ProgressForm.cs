using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeApp
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProgressBar.Maximum = 1000000;
            ProgressBar.Minimum = 0;
            ProgressBar.Step = 1;
            ProgressBar.Style = ProgressBarStyle.Continuous;

            for (int i = 0; i < ProgressBar.Maximum; i++)
            {
                ProgressBar.Value = i+1;

                LblPBar.Text = "Application Loading  : " + Convert.ToString(i/1000);

            }

            MainForm mf = new MainForm();
            mf.Show();


        }
    }
}
