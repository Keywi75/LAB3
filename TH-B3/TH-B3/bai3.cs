using System;
using System.Windows.Forms;

namespace TH_B3
{
    public partial class bai3 : Form
    {
        public bai3()
        {
            InitializeComponent();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Server_3 serverForm = new Server_3();
            serverForm.Show(); 

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Client_3 clientForm = new Client_3();
            clientForm.Show();
        }
    }
}
