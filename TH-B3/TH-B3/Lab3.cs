using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TH_B3
{
    public partial class Lab3 : Form
    {
        public Lab3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bai1 Bai1 = new bai1();
            Bai1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bai2_telnet Bai2 = new bai2_telnet();
            Bai2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bai3 Bai3 = new bai3();
            Bai3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bai4 Bai4 = new bai4();
            Bai4.Show();
        }
    }
}
