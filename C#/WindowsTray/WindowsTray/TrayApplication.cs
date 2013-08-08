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
            cpu = new ProtocolProcessor(Environment.MachineName, Settings.getSetting(Settings.IP));
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

        void settingsForm_OnDataPassed(string data)
        {
            switch (data)
            {
                case "connect":


                    break;
                case "disconnect":

                    break;
                case "connectionStatus":
                    if (true)
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

        void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (formSettings != null)
            {
                if (formSettings.formOpen == false)
                {
                    formSettings = new FormSettings();
                    formSettings.formOpen = true;
                    formSettings.OnDataPassed += settingsForm_OnDataPassed;
                    DialogResult result = formSettings.ShowDialog();
                    formSettings.formOpen = false;
                }
            }   
        }

        private void SettingsItem_Click(object sender, EventArgs e)
        {
            if (formSettings == null)
            {
                formSettings = new FormSettings();
                formSettings.formOpen = true;
                formSettings.OnDataPassed += settingsForm_OnDataPassed;
                DialogResult result = formSettings.ShowDialog();
                formSettings.formOpen = false;
            }
            else if (formSettings.formOpen == false)
            {
                formSettings = new FormSettings();
                formSettings.formOpen = true;
                formSettings.OnDataPassed += settingsForm_OnDataPassed;
                DialogResult result = formSettings.ShowDialog();
                formSettings.formOpen = false;
            }
        }

        private void ExitItem_Click(object sender, EventArgs e)
        {
            ExitThread();
        }
    }
}
