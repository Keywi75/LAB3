using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TH_B3
{
    public partial class Server_4 : Form
    {
        private Dictionary<string, Socket> clients = new Dictionary<string, Socket>();
        private object lockObj = new object();

        public Server_4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread serverThread = new Thread(() =>
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 8080);
                Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(ipep);
                serverSocket.Listen(10);

                UpdateTextBox("Server đang lắng nghe ...");

                while (true)
                {
                    Socket client = serverSocket.Accept();
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
            });

            serverThread.IsBackground = true;
            serverThread.Start();
        }

        private void HandleClient(Socket clientSocket)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesReceived = clientSocket.Receive(buffer);
                string username = Encoding.UTF8.GetString(buffer, 0, bytesReceived).Trim();

                lock (lockObj)
                {
                    if (clients.ContainsKey(username))
                    {
                        byte[] msg = Encoding.UTF8.GetBytes("Tên người dùng đã tồn tại.");
                        clientSocket.Send(msg);
                        clientSocket.Close();
                        return;
                    }
                    else
                    {
                        clients.Add(username, clientSocket);
                        byte[] msg = Encoding.UTF8.GetBytes("Tên người dùng hợp lệ.");
                        clientSocket.Send(msg);
                        string joinMessage = $"[Server]: Người dùng '{username}' đã tham gia phòng chat.";
                        UpdateTextBox(joinMessage);

                        BroadcastMessage(joinMessage, username);

                    }
                }

                while (true)
                {
                    buffer = new byte[1024];
                    bytesReceived = clientSocket.Receive(buffer);
                    if (bytesReceived == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesReceived).Trim();
                    string fullMessage = $"[{username}]: {message}";

                    UpdateTextBox(fullMessage);
                    BroadcastMessage(fullMessage, username);
                }

            }
            catch (SocketException)
            {
                UpdateTextBox("[Server]: Một client đã ngắt kết nối.");
            }
            finally
            {
                lock (lockObj)
                {
                    foreach (var kvp in clients)
                    {
                        if (kvp.Value == clientSocket)
                        {
                            clients.Remove(kvp.Key);
                            break;
                        }
                    }
                }
                clientSocket.Close();
            }
        }


        private void UpdateTextBox(string message)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new Action<string>(UpdateTextBox), message);
            }
            else
            {
                textBox1.AppendText(message + Environment.NewLine);
            }
        }
        private void BroadcastMessage(string message, string fromUser)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            lock (lockObj)
            {
                foreach (var kvp in clients)
                {
                    try
                    {
                        if (kvp.Key != fromUser)
                        {
                            kvp.Value.Send(data);
                        }
                    }
                    catch
                    {
                        
                    }
                }
            }
        }

    }
}
