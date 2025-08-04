using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography;
using TH_B3;


namespace TH_B3
{
    public partial class Client_4 : Form
    {
        private Socket clientSocket;
        private bool connected = false;
        private string username = "";

        public Client_4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                try
                {
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    clientSocket.Connect("127.0.0.1", 8080);

                    username = textBox2.Text.Trim();
                    byte[] nameData = Encoding.UTF8.GetBytes(username);
                    clientSocket.Send(nameData);

         
                    byte[] buffer = new byte[1024];
                    int recv = clientSocket.Receive(buffer);
                    string response = Encoding.UTF8.GetString(buffer, 0, recv);

                    if (response.Contains("Tên người dùng đã tồn tại"))
                    {
                        MessageBox.Show("Tên người dùng đã tồn tại, hãy chọn tên khác!");
                        clientSocket.Close();
                        return;
                    }

                    connected = true;
                    Thread receiveThread = new Thread(ReceiveMessages);
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                }
                catch
                {
                    MessageBox.Show("Không thể kết nối tới server.");
                    return;
                }
            }

            // Gửi tin nhắn sau khi đã xác nhận tên
            if (textBox3.Text.Trim() != "")
            {
                string message = textBox3.Text.Trim();
                string enmess = AESen.Encrypt(message);
                byte[] msgData = Encoding.UTF8.GetBytes(enmess);
                clientSocket.Send(msgData);
                textBox1.AppendText($"[Tôi]: {message}{Environment.NewLine}");

                textBox3.Clear();
            }
        }


        private void ReceiveMessages()
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (true) 
                {
                    try
                    {
                        int bytesReceived = clientSocket.Receive(buffer);
                        if (bytesReceived == 0) break;

                        string rawMessage = Encoding.UTF8.GetString(buffer, 0, bytesReceived).Trim();
                        string displayMessage;

                        int separatorIndex = rawMessage.IndexOf("]: ");

                        if (separatorIndex > 0 && !rawMessage.StartsWith("[Server]:"))
                        {
                            string sender = rawMessage.Substring(0, separatorIndex + 1);
                            string content = rawMessage.Substring(separatorIndex + 3);

                            try
                            {
                                string decryptedContent = AESen.Decrypt(content);
                                displayMessage = $"{sender}: {decryptedContent}";
                            }
                            catch
                            {
                                
                                displayMessage = $"{rawMessage} (Không thể giải mã)";
                            }
                        }
                        else
                        {
                            displayMessage = rawMessage;
                        }


                        UpdateChat(displayMessage);
                    }
                    catch (SocketException)
                    {
                        UpdateChat("[System]: Mất kết nối tới server.");
                        break;
                    }
                    
                }
            }
            catch
            {
                MessageBox.Show("Mất kết nối tới server.");
                clientSocket.Close();
                connected = false;
            }
        }

        private void UpdateChat(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateChat), message);
            }
            else
            {
                textBox1.AppendText($"{message}{Environment.NewLine}");
            }
        }
    }
}
