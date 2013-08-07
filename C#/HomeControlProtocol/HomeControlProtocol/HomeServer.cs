using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SocketLibrary;
using System.Collections;
using System.Timers;

namespace HomeControlProtocol
{
    public delegate void ServerListeningHandler();
    public delegate void ClientConnectedHandler(string client);
    public delegate void ClientDisconnectedHandler(string client);
    public delegate void OnDebugReceivedFromClientHandler(string client, byte device, string debug);
    public delegate void OnValueUpdatedByClientHandler(string client, byte device, string value);
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
        UDPServer UDPserver;
        Arduino arduino;
        Timer arduinoTimer;

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

            UDPserver = new UDPServer();
            UDPserver.ClientConnected += UDPserver_ClientConnected;
            UDPserver.DebugReceivedFromClient += UDPserver_DebugReceivedFromClient;
            UDPserver.DataReceivedFromClient += UDPserver_DataReceivedFromClient;
            UDPserver.MessageReceivedFromClient += UDPserver_MessageReceivedFromClient;

            if (COMPort != "")
            {
                arduino = new Arduino(COMPort);
                arduinoConnected = true;
                arduino.ArduinoConnected += arduino_ArduinoConnected;
                arduino.ArduinoDisconnected += arduino_ArduinoDisconnected;
                arduino.DebugReceivedFromArduino += arduino_DebugReceivedFromArduino;
                arduino.DataReceivedFromArduino += arduino_DataReceivedFromArduino;
                arduino.MessageReceivedFromArduino += arduino_MessageReceivedFromArduino;
                arduinoTimer = new Timer(30000);
                arduinoTimer.AutoReset = true;
                arduinoTimer.Elapsed += arduinoTimer_Elapsed;
            }
        }

        #region UDPserver
        void UDPserver_ClientConnected(string client)
        {
            ClientConnected(client);
        }

        void UDPserver_DebugReceivedFromClient(string client, byte[] debug)
        {
            byte device = debug[0];
            byte[] bytes = new byte[debug.Length - 1];
            System.Buffer.BlockCopy(debug, 1, bytes, 0, bytes.Length);
            string message = Encoding.UTF8.GetString(bytes);
            DebugReceivedFromClient(client, device, message);
        }

        void UDPserver_DataReceivedFromClient(string client, byte[] data)
        {
            byte device = data[0];
            byte command = data[1];
            if (command == DataProtocol.clientChangedValue)
            {
                byte[] bytes = new byte[data.Length - 2];
                System.Buffer.BlockCopy(data, 2, bytes, 0, bytes.Length);
                string value = Encoding.UTF8.GetString(bytes);
                ValueUpdatedByClient(client, device, value);
            }
        }

        void UDPserver_MessageReceivedFromClient(string client, string message)
        {
            MessageReceivedFromClient(client, message);
        }
        #endregion
        #region Arduino
        void arduino_ArduinoConnected(string client)
        {
            ClientConnected(client);
        }

        void arduino_ArduinoDisconnected(string client)
        {
            ClientDisconnected(client);
        }

        void arduino_DebugReceivedFromArduino(byte[] debug)
        {
            byte device = debug[0];
            byte[] bytes = new byte[debug.Length - 1];
            System.Buffer.BlockCopy(debug, 1, bytes, 0, bytes.Length);
            string message = Encoding.UTF8.GetString(bytes);
            DebugReceivedFromClient("arduino", device, message);
        }

        void arduino_DataReceivedFromArduino(byte[] data)
        {
            byte device = data[0];
            byte command = data[1];
            if (command == DataProtocol.clientChangedValue)
            {
                byte[] bytes = new byte[data.Length - 2];
                System.Buffer.BlockCopy(data, 2, bytes, 0, bytes.Length);
                string value = Encoding.UTF8.GetString(bytes);
                ValueUpdatedByClient("arduino", device, value);
            }
        }

        void arduino_MessageReceivedFromArduino(string message)
        {
            MessageReceivedFromClient("arduino", message);
        }
        #endregion
        #region Server
        void server_ClientConnected(string client)
        {
            ClientConnected(client);
        }

        void server_ClientDisconnected(string client)
        {
            ClientDisconnected(client);
        }

        void server_DebugReceivedFromClient(string client, byte[] debug)
        {
            byte device = debug[0];
            byte[] bytes = new byte[debug.Length - 1];
            System.Buffer.BlockCopy(debug, 1, bytes, 0, bytes.Length);
            string message = Encoding.UTF8.GetString(bytes);
            DebugReceivedFromClient(client, device, message);
        }

        void server_DataReceivedFromClient(string client, byte[] data)
        {
            byte device = data[0];
            byte command = data[1];
            if (command == DataProtocol.clientChangedValue)
            {
                byte[] bytes = new byte[data.Length - 2];
                System.Buffer.BlockCopy(data, 2, bytes, 0, bytes.Length);
                string value = Encoding.UTF8.GetString(bytes);
                ValueUpdatedByClient(client, device, value);
            }
        }

        void server_MessageReceivedFromClient(string client, string message)
        {
            MessageReceivedFromClient(client, message);
        }
        #endregion

        void arduinoTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            /*int dayInt = (int)DateTime.Now.DayOfWeek;
            if (dayInt == 0) dayInt = 7;
            String day = "0" + dayInt.ToString();
            String date = DateTime.Now.Day.ToString();
            if (date.Length != 2) date = "0" + date;
            String month = DateTime.Now.Month.ToString();
            if (month.Length != 2) month = "0" + month;
            String hour = DateTime.Now.Hour.ToString();
            if (hour.Length != 2) hour = "0" + hour;
            String minute = DateTime.Now.Minute.ToString();
            if (minute.Length != 2) minute = "0" + minute;
            string toSend = day.ToString() + " " + date + " " + month + " " + hour + ":" + minute;
            byte[] bytesToSend = new byte[toSend.Length + 2];
            bytesToSend[0] = DeviceProtocol.DateTime;
            bytesToSend[1] = DataProtocol.setClientValue;
            System.Buffer.BlockCopy(Encoding.UTF8.GetBytes(toSend), 0, bytesToSend, 2, toSend.Length);
            arduino.SendDataToArduino(bytesToSend);*/
        }

        public ArrayList GetClientList()
        {
            return server.GetClientList();
        }

        void server_ServerListening()
        {
            ServerListening();
        }

        public void startServer(int TCPport, int UDPport)
        {
            server.Listen(TCPport);
            UDPserver.Listen(UDPport);
            if (arduinoConnected)
            {
                arduino.Listen();
            }
        }

        public void SendSettingToClient(string client, byte device, String value)
        {
            byte[] bytes = new byte[value.Length + 2];
            bytes[0] = device;
            bytes[1] = DataProtocol.setClientValue;
            System.Buffer.BlockCopy(Encoding.UTF8.GetBytes(value), 0, bytes, 2, value.Length);
            if (client == "arduino")
            {
                arduino.SendDataToArduino(bytes);
            }
            else
            {
                server.SendDataToClient(client, bytes);
            }
        }

        public void SendDebugToClient(string client, byte device, String value)
        {
            byte[] bytes = SocketLibrary.ConverterProtocol.AddProtocol(device, Encoding.UTF8.GetBytes(value)); 
            System.Buffer.BlockCopy(Encoding.UTF8.GetBytes(value), 0, bytes, 1, value.Length);
            server.SendDataToClient(client, bytes);
        }

        public void SendMessageToClient(string client, string message)
        {
            server.SendMessageToClient(client, message);
        }
    }
}
