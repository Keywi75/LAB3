using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TH_B3
{
    public partial class Client_3 : Form
    {
        Socket clientSocket;

        public Client_3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = textBox1.Text.Trim();
                int port = int.Parse(textBox2.Text.Trim());

        
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(new IPEndPoint(IPAddress.Parse(ip), port));

                MessageBox.Show("Kết nối thành công tới server!");

                // Bắt đầu luồng nhận dữ liệu 
                Thread receiveThread = new Thread(ReceiveData);
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (clientSocket != null && clientSocket.Connected)
                {
                    string message = textBox4.Text.Trim() + "\n"; // Kết thúc bằng \n để server biết kết thúc gói
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    clientSocket.Send(data);

                    textBox3.AppendText( message + "\r\n");
                    textBox4.Clear();
                }
                else
                {
                    MessageBox.Show("Chưa kết nối đến server!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi: " + ex.Message);
            }
        }

        private void ReceiveData()
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            while (true)
            {
                try
                {
                    bytesRead = clientSocket.Receive(buffer);
                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

              
                    textBox3.Invoke(new Action(() =>
                    {
                        textBox3.AppendText("Server: " + response + "\r\n");
                    }));
                }
                catch
                {
                    break; // Thoát khỏi vòng lặp khi lỗi hoặc server đóng kết nối
                }
            }
        }
    }
}
