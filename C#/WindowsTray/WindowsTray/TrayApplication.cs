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
    public class TrayApplication : ApplicationContext
    {
        //Source: https://www.simple-talk.com/dotnet/.net-framework/creating-tray-applications-in-.net-a-practical-guide/#ninth
        private static readonly string IconFileName = "home.ico";
        private static readonly string DefaultTooltip = "Home Control Suite";

        private System.ComponentModel.IContainer components;	// a list of components to dispose when the context is disposed
        private NotifyIcon notifyIcon;				            // the icon that sits in the system tray
        private ProtocolProcessor cpu;

        private FormSettings formSettings;

        public TrayApplication()
        {
            InitializeContext();
            notifyIcon.ShowBalloonTip(5000, "Home Control Suite", "Started succesfully", ToolTipIcon.None);
            int port;
            int.TryParse(Settings.getSetting(Settings.Port), out port);
            cpu = new ProtocolProcessor(Environment.MachineName, Settings.getSetting(Settings.IP), port);
            cpu.Notify += cpu_Notify;
        }

        void cpu_Notify(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon)
        {
            notifyIcon.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);
        }

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
            formSettings = new FormSettings();
            formSettings.OnDataPassed += settingsForm_OnDataPassed;
            DialogResult result = formSettings.ShowDialog();
        }

        void settingsForm_OnDataPassed(string data)
        {
            switch (data)
            {
                case "connect":
                    int port;
                    int.TryParse(Settings.getSetting(Settings.Port), out port);
                    cpu.client.Connect(Settings.getSetting(Settings.IP), port);
                    break;
                case "disconnect":
                    cpu.client.Disconnect();
                    break;
                case "connectionStatus":
                    if (cpu.client.getConnectionStatus())
                    {
                        formSettings.setConnectionStatus("Status: Connected");
                    }
                    else
                    {
                        formSettings.setConnectionStatus("Status: Disconnected");
                        //TODO: FUTURE: Automatically update the status
                    }
                    break;
            }
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
