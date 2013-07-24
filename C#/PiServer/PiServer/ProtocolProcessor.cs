﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using HomeControlProtocol;
using System.Collections;
using System.Timers;
using System.Data;

namespace PiServer
{
    class ProtocolProcessor
    {
        public HomeServer server;
        public Dictionary<string, string> protocol;
        Timer timeChecker;
        public ProtocolProcessor()
        {
            server = new HomeServer("Connected to Home Control Suite PiServer");//,"COM3");//,"ttyACM0");
            server.ServerListening += server_ServerListening;
            server.ClientConnected += server_ClientConnected;
            server.ClientDisconnected += server_ClientDisconnected;
            server.DebugReceivedFromClient += server_DebugReceivedFromClient;
            server.ValueUpdatedByClient += server_ValueUpdatedByClient;
            server.MessageReceivedFromClient += server_MessageReceivedFromClient;

            timeChecker = new Timer(30000);
            timeChecker.Elapsed += timeChecker_Elapsed;
            timeChecker.AutoReset = true;
            timeChecker.Start();

            server.startServer(9999); //TODO: Change port to variable
            protocol = new Dictionary<string, string>();
        }

        void timeChecker_Elapsed(object sender, ElapsedEventArgs e)
        {
            SQLiteDatabase database = new SQLiteDatabase("saved.db3");
            DataTable alarms = database.GetDataTable("SELECT * FROM " + DatabaseProtocol.Alarms + ";");
            foreach (DataRow alarm in alarms.Rows)
            {
                if(alarm["Enabled"].ToString() == "1")
                {
                    DateTime tempDT = DateTime.Parse(alarm["DateTime"].ToString());
                    if ((tempDT.Hour == DateTime.Now.Hour) &&
                        (tempDT.Minute == DateTime.Now.Minute))
                    {
                        Console.WriteLine(alarm["Name"].ToString() + " is now active");
                        Dictionary<string, string> tempDict = new Dictionary<string,string>();
                        tempDict["Enabled"] = "0";
                        database.Update(DatabaseProtocol.Alarms, tempDict, "Name = '" + alarm["Name"] + "' AND DateTime = '" + alarm["DateTime"] + "'");
                    }
                }
            }
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
                else if (value == VariableProtocol.Off)
                {

                }
            }
            else if (device == DeviceProtocol.LockStatus)
            {
                if (value == VariableProtocol.Unlock)
                {
                    if (client != "arduino")
                    {
                        server.SendSettingToClient(client, device, value);
                    }
                }
                else
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
