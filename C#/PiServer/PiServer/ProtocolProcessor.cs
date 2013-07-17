using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using HomeControlProtocol;

namespace PiServer
{
    class ProtocolProcessor
    {
        public HomeServer server;
        public ProtocolProcessor()
        {
            server = new HomeServer("Connected to Home Control Suite PiServer");//,"COM3");
            server.ServerListening += server_ServerListening;
            server.ClientConnected += server_ClientConnected;
            server.ClientDisconnected += server_ClientDisconnected;
            server.DebugReceivedFromClient += server_DebugReceivedFromClient;
            server.SettingSentFromClient += server_SettingSentFromClient;
            server.ValueUpdatedByClient += server_ValueUpdatedByClient;
            server.MessageReceivedFromClient += server_MessageReceivedFromClient;

            server.startServer(9999); //TODO: Change port to variable
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

        void server_SettingSentFromClient(string client, string device, string value)
        {
            Console.WriteLine(client + ">Data>" + device + ">Set>" + value);
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
                    server.SendSettingToClient("BLAKE-PC", device, value);
                }
            }
        }

        void server_ValueUpdatedByClient(string client, string device, string value)
        {
            Console.WriteLine(client + ">Data>" + device + ">Update>" + value);
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
