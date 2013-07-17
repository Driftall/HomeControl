using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SocketLibrary;
using System.Collections;

namespace HomeControlProtocol
{
    public delegate void ServerListeningHandler();
    public delegate void ClientConnectedHandler(string client);
    public delegate void ClientDisconnectedHandler(string client);
    public delegate void OnDebugReceivedFromClientHandler(string client, string device, string debug);
    public delegate void OnValueUpdatedByClientHandler(string client, string device, string value);
    public delegate void OnMessageReceivedFromClientHandler(string client, string message);
    public class HomeServer
    {
        public event ServerListeningHandler ServerListening;
        public event ClientConnectedHandler ClientConnected;
        public event ClientDisconnectedHandler ClientDisconnected;
        public event OnDebugReceivedFromClientHandler DebugReceivedFromClient;
        public event OnValueUpdatedByClientHandler ValueUpdatedByClient;
        public event OnMessageReceivedFromClientHandler MessageReceivedFromClient;

        Server server;
        Arduino arduino;

        bool arduinoConnected = false;

        public HomeServer(String welcomeMessage, String COMPort = "")
        {
            server = new Server(welcomeMessage);
            server.ServerListening += server_ServerListening;
            server.ClientConnected += server_ClientConnected;
            server.ClientDisconnected += server_ClientDisconnected;
            server.DebugReceivedFromClient += server_DebugReceivedFromClient;
            server.DataReceivedFromClient += server_DataReceivedFromClient;
            server.MessageReceivedFromClient += server_MessageReceivedFromClient;

            if (COMPort != "") 
            {
                arduino = new Arduino(COMPort);
                arduinoConnected = true;
                arduino.ArduinoConnected += arduino_ArduinoConnected;
                arduino.ArduinoDisconnected += arduino_ArduinoDisconnected;
                arduino.DebugReceivedFromArduino += arduino_DebugReceivedFromArduino;
                arduino.DataReceivedFromArduino += arduino_DataReceivedFromArduino;
                arduino.MessageReceivedFromArduino += arduino_MessageReceivedFromArduino;
            }
        }

        public ArrayList GetClientList()
        {
            return server.GetClientList();
        }

        void server_ServerListening()
        {
            ServerListening();
        }

        public void startServer(int port)
        {
            server.Listen(port);
            if (arduinoConnected)
            {
                arduino.Listen();
            }
        }

        void arduino_ArduinoConnected(string client)
        {
            ClientConnected(client);
        }

        void arduino_ArduinoDisconnected(string client)
        {
            ClientDisconnected(client);
        }

        void arduino_DebugReceivedFromArduino(string debug)
        {
            string debugLine = debug.Remove(0,2);
            string debugDevice = debugLine.Remove(4);
            string debugText = debugLine.Remove(0, 4);

            DebugReceivedFromClient("arduino", debugDevice, debugLine);
        }

        void arduino_DataReceivedFromArduino(string data)
        {
            string device = data.Remove(4);
            string command = data.Remove(0, 4);
            string value = command.Remove(0, 3);
            if (command.StartsWith(DataProtocol.changedValue))
            {
                ValueUpdatedByClient("arduino", device, value);
            }
        }

        void arduino_MessageReceivedFromArduino(string message)
        {
            MessageReceivedFromClient("arduino", message);
        }

        void server_ClientConnected(string client)
        {
            ClientConnected(client);
        }

        void server_ClientDisconnected(string client)
        {
            ClientDisconnected(client);
        }

        void server_DebugReceivedFromClient(string client, string debug)
        {
            string message = debug.Remove(0, 4);
            string device = debug.Remove(4);
            DebugReceivedFromClient(client, device, message);
        }

        void server_DataReceivedFromClient(string client, string data)
        {
            string device = data.Remove(4);
            string command = data.Remove(0, 4);
            string value = command.Remove(0, 3);
            if (command.StartsWith(DataProtocol.changedValue))
            {
                ValueUpdatedByClient(client, device, value);
            }
        }

        void server_MessageReceivedFromClient(string client, string message)
        {
            MessageReceivedFromClient(client, message);
        }

        public void SendSettingToClient(string client, string device, Object value)
        {
            if (client == "arduino")
            {
                arduino.SendDataToArduino(device + DataProtocol.setValue + value);
            }
            else
            {
                server.SendDataToClient(client, device + DataProtocol.setValue + value);
            }
        }

        public void SendDebugToClient(string client, string device, Object value)
        {
            server.SendDebugToClient(client, device + (string)value);
        }

        public void SendMessageToClient(string client, string message)
        {
            server.SendMessageToClient(client, message);
        }
    }
}
