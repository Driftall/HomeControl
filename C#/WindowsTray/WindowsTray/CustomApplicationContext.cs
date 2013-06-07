using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsTray
{
    public class CustomApplicationContext : ApplicationContext
    {
        //Source: https://www.simple-talk.com/dotnet/.net-framework/creating-tray-applications-in-.net-a-practical-guide/#ninth
        private static readonly string IconFileName = "home.ico";
        private static readonly string DefaultTooltip = "Home Control Suite";

        HCClient client;

        public CustomApplicationContext()
        {
            InitializeContext();
            //Protocol protocol = new Protocol();
            notifyIcon.ShowBalloonTip(5000, "Home Control Suite", "Started succesfully", ToolTipIcon.None);
            client = new HCClient(Environment.MachineName);
            client.ConnectionFailed += client_ConnectionFailed;
            client.Connected += client_Connected;
            client.Disconnected += client_Disconnected;
            client.ValueRequestedFromServer += client_ValueRequestedFromServer;
            client.MessageReceivedFromServer +=client_MessageReceivedFromServer;
            client.Connect("127.0.0.1", 9999);
        }

        void client_ConnectionFailed()
        {
            notifyIcon.ShowBalloonTip(5000, "Home Control Suite", "Connection failed", ToolTipIcon.Error);
        }

        void client_ValueRequestedFromServer(string setting)
        {
            if(setting == Setting.IP)
            {
                client.SendValueToServer(Setting.IP, (Object)getLocalIP());                      
            }
        }

        //http://stackoverflow.com/questions/1069103/how-to-get-my-own-ip-address-in-c
        string getLocalIP()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        void client_MessageReceivedFromServer(string data)
        {
            notifyIcon.ShowBalloonTip(5000, "Server Message", data, ToolTipIcon.Info);
        }

        void client_Connected()
        {
            notifyIcon.ShowBalloonTip(5000, "Home Control Suite", "Connected to server", ToolTipIcon.Info);
        }

        void client_Disconnected()
        {
            notifyIcon.ShowBalloonTip(5000, "Home Control Suite", "Connection lost", ToolTipIcon.Error);
        }

        private System.ComponentModel.IContainer components;	// a list of components to dispose when the context is disposed
        private NotifyIcon notifyIcon;				            // the icon that sits in the system tray

        private void InitializeContext()
        {
            components = new System.ComponentModel.Container();
            notifyIcon = new NotifyIcon(components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = new Icon(IconFileName),
                Text = DefaultTooltip,
                Visible = true
            };
            notifyIcon.ContextMenuStrip.Items.Add("Home Control Suite");
            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            notifyIcon.ContextMenuStrip.Items.Add("Settings", null, new EventHandler(SettingsItem_Click));
            notifyIcon.ContextMenuStrip.Items.Add("Exit", null, new EventHandler(ExitItem_Click));

            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
        }

        void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            DialogResult result = new FormSettings().ShowDialog();
        }

        private void SettingsItem_Click(object sender, EventArgs e)
        {
            DialogResult result = new FormSettings().ShowDialog();
        }

        private void ExitItem_Click(object sender, EventArgs e)
        {
            ExitThread();
        }
    }
}
