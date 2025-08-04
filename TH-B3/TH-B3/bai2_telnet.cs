using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TH_B3
{
    public partial class bai2_telnet : Form
    {
        public bai2_telnet()
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

            // Tạo socket lắng nghe
            Socket listenerSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

            IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            listenerSocket.Bind(ipepServer);
            listenerSocket.Listen(-1);
            textBox1.AppendText("Telnet running ip: 127.0.0.1 port:8080 \r\n");
            // Chấp nhận client kết nối
            Socket clientSocket = listenerSocket.Accept();

            

            // Nhận dữ liệu liên tục
            while (clientSocket.Connected)
            {
                string text = "";
                try
                {
                    do
                    {
                        bytesReceived = clientSocket.Receive(recv);
                        text += Encoding.ASCII.GetString(recv);
                    }
                    while (text[text.Length - 1] != '\n');

                    textBox1.AppendText("message: " + text + "\r\n");
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
