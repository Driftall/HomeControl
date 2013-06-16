using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
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
            notifyIcon.ShowBalloonTip(5000, "Home Control Suite", "Started succesfully", ToolTipIcon.None);
            client = new HCClient(Environment.MachineName);
            ProtocolProcessing protoProcessor = new ProtocolProcessing(client,notifyIcon);
            int port;
            int.TryParse(Settings.getSetting(Settings.Port), out port);
            client.Connect(Settings.getSetting(Settings.IP), port);
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
