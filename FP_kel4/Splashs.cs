using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FP_kel4
{
    public partial class Splashs : Form
    {
        public Splashs()
        {
            InitializeComponent();
        }
        int start = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            start += 1;
            MyProgress.Value = start;
            if(MyProgress.Value == 100)
            {
                MyProgress.Value = 0;
                timer1.Stop();
                Logins Mylogin = new Logins();
                this.Hide();
                Mylogin.Show();
            }
        }

        private void Splashs_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
