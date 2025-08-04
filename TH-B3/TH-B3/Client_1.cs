using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace UDP_Client
{
    public partial class Client_1 : Form
    {
        private UdpClient udpClient;

        public Client_1()
        {
            InitializeComponent();
            udpClient = new UdpClient();
        }

        private void Send_Btn_Click(object sender, EventArgs e)
        {
            string serverIP = txtIPAddress.Text.Trim();
            if (!int.TryParse(txtPort.Text.Trim(), out int serverPort))
            {
                MessageBox.Show("Please enter a valid port number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string message = txtMessage.Text.Trim();

            if (string.IsNullOrEmpty(serverIP) || string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Please enter a valid IP address and message.");
                return;
            }
            try
            {
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
                byte[] sendBytes = Encoding.UTF8.GetBytes(message);
                udpClient.Send(sendBytes, sendBytes.Length, serverEndPoint);
                txtMessage.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Send Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void Clear_Btn_Click(object sender, EventArgs e)
        {
            txtIPAddress.Clear();
            txtPort.Clear();
            txtMessage.Clear();
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            udpClient.Close();
        }
    }
}
