using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        public CustomApplicationContext()
        {
            InitializeContext();
            //Protocol protocol = new Protocol();
            notifyIcon.ShowBalloonTip(5000, "Home Control Suite", "Started succesfully", ToolTipIcon.None);
            SocketLibrary.Client client = new SocketLibrary.Client(Environment.MachineName);
            client.Connected += client_Connected;
            client.Disconnected += client_Disconnected;
            client.DataReceivedFromServer += client_DataReceivedFromServer;
            client.Connect("127.0.0.1", 9999);
        }

        void client_DataReceivedFromServer(string data)
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
