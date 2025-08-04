using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TH_B3
{
    public partial class Server_3: Form
    {
        public Server_3()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Tránh lỗi cross-thread
            CheckForIllegalCrossThreadCalls = false;

            Thread serverThread = new Thread(new ThreadStart(StartUnsafeThread));
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        private void StartUnsafeThread()
        {
            int bytesReceived = 0;
            byte[] recv = new byte[1];

            Socket listenerSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

            string ip = textBox2.Text;
            int port = int.Parse(textBox3.Text);
            IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse(ip), port);
            listenerSocket.Bind(ipepServer);
            listenerSocket.Listen(-1);
            textBox1.AppendText("Telnet running ip:" + ip + "port: " + port + "\r\n");
            // Chấp nhận client kết nối
            Socket clientSocket = listenerSocket.Accept();

            

            while (clientSocket.Connected)
            {
                string text = "";
                try
                {
                    do
                    {
                        bytesReceived = clientSocket.Receive(recv);
                        text += Encoding.UTF8.GetString(recv);
                    }
                    while (text[text.Length - 1] != '\n');

                    textBox1.AppendText("Client: " + text + "\r\n");
                }
                catch (Exception ex)
                {
                    textBox1.AppendText("Lỗi: " + ex.Message + "\r\n");
                    break;
                }
            }

            listenerSocket.Close();
        }
    }
}
