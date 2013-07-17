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
    public delegate void OnDebugReceivedFromServerHandler(string device, string debug);
    public delegate void OnSettingSentFromServerHandler(string device, string value); //could be a Setting?
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

        void client_DebugReceivedFromServer(string debug)
        {
            string message = debug.Remove(0, 4);
            string device = debug.Remove(4);
            DebugReceivedFromServer(device, message);
        }

        void client_DataReceivedFromServer(string data)
        {
            string device = data.Remove(4);
            string command = data.Remove(0, 4);
            if (command.StartsWith(DataProtocol.setValue))
            {
                string value = command.Remove(0, 3);
                SettingSentFromServer(device, value);
            }
        }

        void client_MessageReceivedFromServer(string message)
        {
            MessageReceivedFromServer(message);
        }

        public void SendDebugToServer(string device, string debug)
        {
            client.SendDebugToServer(device + debug);
        }

        public void ChangeValueOnServer(string device, Object value)
        {
            client.SendDataToServer(device + DataProtocol.changedValue + value);
        }

        public void SendMessageToServer(string message)
        {
            client.SendMessageToServer(message);
        }
    }
}
