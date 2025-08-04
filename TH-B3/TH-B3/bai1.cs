using System;
using System.Windows.Forms;

namespace TH_B3
{
    public partial class bai1 : Form
    {
        public bai1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UDP_Server.Server_1 serverForm = new UDP_Server.Server_1();
            serverForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UDP_Client.Client_1 clientForm = new UDP_Client.Client_1();
            clientForm.Show();
        }
    }
}
