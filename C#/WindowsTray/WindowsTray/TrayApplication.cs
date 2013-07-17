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
        private Timer timer;
        private ProtocolProcessor cpu;

        public TrayApplication()
        {
            InitializeContext();
            notifyIcon.ShowBalloonTip(5000, "Home Control Suite", "Started succesfully", ToolTipIcon.None);
            int port;
            int.TryParse(Settings.getSetting(Settings.Port), out port);
            cpu = new ProtocolProcessor(Environment.MachineName, Settings.getSetting(Settings.IP), port);
            cpu.Notify += cpu_Notify;
            timer = new Timer();
            timer.Interval = 10000;
            timer.Tick += timer_Tick;
            timer.Enabled = true;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            cpu.timerTick();
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
