using System;
using System.Windows.Forms;

namespace TH_B3
{
    public partial class bai4 : Form
    {
        public bai4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Server_4 serverForm = new Server_4();
            serverForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Client_4 clientForm = new Client_4();
            clientForm.Show();
        }
    }
}
