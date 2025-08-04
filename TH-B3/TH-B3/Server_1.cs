using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace UDP_Server
{
    public partial class Server_1 : Form
    {

        private UdpClient udpServer;
        private Thread receiveThread;
        private bool isRunning = false;
        public Server_1()
        {
            InitializeComponent();
        }

        private void Listen_Btn_Click(object sender, EventArgs e)
        {
            int port;
            if (!int.TryParse(txtPort.Text.Trim(), out port))
            {
                MessageBox.Show("Vui lòng nhập cổng hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!isRunning)
            {
                isRunning = true;
                receiveThread = new Thread(() => StartListening(port));
                receiveThread.IsBackground = true;
                receiveThread.Start();
                Listen_Btn.Enabled = false;
            }
        }

        private void StartListening(int port)
        {
            try
            {
                udpServer = new UdpClient(port);
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);

                Invoke(new Action(() => listMessages.Items.Add($"Đang lắng nghe trên cổng {port}...")));

                while (isRunning)
                {
                    byte[] receiveBytes = udpServer.Receive(ref remoteEndPoint);
                    string receivedData = Encoding.UTF8.GetString(receiveBytes);

                    Invoke(new Action(() =>
                    {
                        listMessages.Items.Add($"Connected from {remoteEndPoint}: {receivedData}");
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRunning = false;
            udpServer?.Close();
            receiveThread?.Abort();
        }
    }
}
