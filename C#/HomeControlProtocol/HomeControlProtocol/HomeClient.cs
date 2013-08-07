using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeControlProtocol;

namespace WindowsTray
{
    public delegate void ConnectedHandler(string welcomeMessage);
    public delegate void ConnectionFailedHandler();
    public delegate void DisconnectedHandler();
    public delegate void OnDebugReceivedFromServerHandler(byte device, string debug);
    public delegate void OnSettingSentFromServerHandler(byte device, string value); //could be a Setting?
    public delegate void OnMessageReceivedFromServerHandler(string message);
    //http://www.codeproject.com/Articles/24948/Three-Ways-To-Extend-A-Class
    public class HomeClient
    {
        public event ConnectedHandler Connected;
        public event ConnectionFailedHandler ConnectionFailed;
        public event DisconnectedHandler Disconnected;
        public event OnDebugReceivedFromServerHandler DebugReceivedFromServer;
        public event OnSettingSentFromServerHandler SettingSentFromServer;
        public event OnMessageReceivedFromServerHandler MessageReceivedFromServer;

        SocketLibrary.Client client;

        public HomeClient(string Name)
        {
            client = new SocketLibrary.Client(Name);
            client.Connected += client_Connected;
            client.ConnectionFailed += client_ConnectionFailed;
            client.Disconnected += client_Disconnected;
            client.DebugReceivedFromServer += client_DebugReceivedFromServer;
            client.DataReceivedFromServer += client_DataReceivedFromServer;
            client.MessageReceivedFromServer += client_MessageReceivedFromServer;
        }

        public void Connect(string serverAddress, int Port)
        {
            client.Connect(serverAddress, Port);
        }

        public void Disconnect()
        {
            client.Disconnect();
        }

        void client_Connected(string welcomeMessage)
        {
            Connected(welcomeMessage);
        }

        void client_ConnectionFailed()
        {
            ConnectionFailed();
        }

        void client_Disconnected()
        {
            Disconnected();
        }

        void client_DebugReceivedFromServer(byte[] debug)
        {
            byte device = debug[0];
            byte[] bytes = new byte[debug.Length - 1];
            System.Buffer.BlockCopy(debug, 1, bytes, 0, bytes.Length);
            string message = Encoding.UTF8.GetString(bytes);
            DebugReceivedFromServer(device, message);
        }

        void client_DataReceivedFromServer(byte[] data)
        {
            byte device = data[0];
            byte command = data[1];
            if (command == DataProtocol.setClientValue)
            {
                byte[] bytes = new byte[data.Length - 2];
                System.Buffer.BlockCopy(data, 2, bytes, 0, bytes.Length);
                string value = Encoding.UTF8.GetString(bytes);
                SettingSentFromServer(device, value);
            }
        }

        void client_MessageReceivedFromServer(string message)
        {
            MessageReceivedFromServer(message);
        }

        public void SendDebugToServer(byte device, string debug)
        {
            byte[] bytesToSend = SocketLibrary.ConverterProtocol.AddProtocol(device, Encoding.UTF8.GetBytes(debug));
            client.SendDebugToServer(bytesToSend);
        }

        public void ChangeValueOnServer(byte device, String value)
        {
            byte[] bytesToSend = new byte[value.Length + 2];
            bytesToSend[0] = device;
            bytesToSend[1] = DataProtocol.clientChangedValue;
            System.Buffer.BlockCopy(Encoding.UTF8.GetBytes(value), 0, bytesToSend, 2, value.Length);
            client.SendDataToServer(bytesToSend);
        }

        public void SendMessageToServer(string message)
        {
            client.SendMessageToServer(message);
        }

        public bool getConnectionStatus()
        {
            return client.connectionStatus;
        }
    }
}
