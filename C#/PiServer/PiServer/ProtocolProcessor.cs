using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using HomeControlProtocol;
using System.Collections;

namespace PiServer
{
    class ProtocolProcessor
    {
        public HomeServer server;
        public Dictionary<string, string> protocol;
        public ProtocolProcessor()
        {
            server = new HomeServer("Connected to Home Control Suite PiServer");//,"COM3");
            server.ServerListening += server_ServerListening;
            server.ClientConnected += server_ClientConnected;
            server.ClientDisconnected += server_ClientDisconnected;
            server.DebugReceivedFromClient += server_DebugReceivedFromClient;
            //server.SettingSentFromClient += server_SettingSentFromClient;
            server.ValueUpdatedByClient += server_ValueUpdatedByClient;
            server.MessageReceivedFromClient += server_MessageReceivedFromClient;

            server.startServer(9999); //TODO: Change port to variable
            protocol = new Dictionary<string, string>();
        }

        void server_ServerListening()
        {
            Console.WriteLine("SERVER>Listening");
        }

        void server_ClientConnected(string client)
        {
            Console.WriteLine(String.Concat(client,">Connected"));
        }

        void server_DebugReceivedFromClient(string client, string device, string debug)
        {
            Console.WriteLine(client + ">Debug>" + debug);
        }

        void server_ValueUpdatedByClient(string client, string device, string value)
        {
            Console.WriteLine(client + ">Data>" + device + ">Update>" + value);
            protocol[device] = value;
            if (device == DeviceProtocol.Debug)
            {
                if (value == VariableProtocol.On)
                {
                    
                }
                else
                {

                }
            }
            else if (device == DeviceProtocol.LockStatus)
            {
                if (value == VariableProtocol.On)
                {
                    ArrayList clients = server.GetClientList();
                    foreach (string lClient in clients)
                    {
                        if (lClient != client)
                        {
                            server.SendSettingToClient(lClient, device, value);
                        }
                    }
                }
            }
            else if (device == DeviceProtocol.BatteryPercentage)
            {
                if (value == "100")
                {
                    ArrayList clients = server.GetClientList();
                    foreach (string lClient in clients)
                    {
                        server.SendMessageToClient(lClient, client + ">Battery is 100%");
                    }
                }
            }
        }

        void server_MessageReceivedFromClient(string client, string message)
        {
            Console.WriteLine(client + ">Message>" + message);
        }

        void server_ClientDisconnected(string client)
        {
            Console.WriteLine(client + ">Disconnected");
        }
    }
}
