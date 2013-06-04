using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

using System.Windows.Forms;

namespace WindowsTray
{
    public delegate void ConnectedHandler();
    public delegate void DisconnectedHandler();

    public class Socket_Client
    {
        //public event CommandReceivedHandler CommandReceived;
        //public event ConsoleHandler ConsoleWrite;

        public event ConnectedHandler Connected;
        public event DisconnectedHandler Disconnected;

        private Socket client;
        private string server;
        const int serverPort = 6667;
        private const int bufferSize = 2048;

        public int indexID = 0;

        public void Connect(string ServerAddress)
        {
            this.client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.server = ServerAddress;

            var connectionOperation = new SocketAsyncEventArgs { RemoteEndPoint = new DnsEndPoint(this.server, serverPort) };
            connectionOperation.Completed += OnConnectionToServerCompleted;

            this.client.ConnectAsync(connectionOperation);
        }

        public void Disconnect()
        {
            this.client.Shutdown(SocketShutdown.Both);
            this.client.Close();
        }

        private void OnConnectionToServerCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                SendIDToServer();
                ReceiveMessage();
            }
            else
            {
                //MessageBox.Show(e.SocketError.ToString());
                //ConsoleWrite("SOCKET", "Connection to server failed");
            }
        }

        public void SendIDToServer()
        {
            var asyncEvent = new SocketAsyncEventArgs { RemoteEndPoint = new DnsEndPoint(server, serverPort) };

            var buffer = Encoding.UTF8.GetBytes(Device.ID);
            asyncEvent.SetBuffer(buffer, 0, buffer.Length);

            client.SendAsync(asyncEvent);
        }

        public void SendToServer(string message)
        {
            var asyncEvent = new SocketAsyncEventArgs { RemoteEndPoint = new DnsEndPoint(server, serverPort) };

            var buffer = Encoding.UTF8.GetBytes(message);
            asyncEvent.SetBuffer(buffer, 0, buffer.Length);

            client.SendAsync(asyncEvent);
        }

        private void ReceiveMessage()
        {
            var responseListener = new SocketAsyncEventArgs();
            responseListener.Completed += OnMessageReceivedFromServer;

            var responseBuffer = new byte[bufferSize];
            responseListener.SetBuffer(responseBuffer, 0, bufferSize);

            client.ReceiveAsync(responseListener);
        }

        private void OnMessageReceivedFromServer(object sender, SocketAsyncEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Buffer, 0, e.BytesTransferred);

            var isConnectionLost = string.IsNullOrWhiteSpace(message);
            if (isConnectionLost)
            {
                Disconnected();
                return;
            }
            Connected();

                //CommandReceived(sender, cmd);
            ReceiveMessage();
        }
    }
}
