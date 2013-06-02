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
            Protocol protocol = new Protocol();
            notifyIcon.ShowBalloonTip(5000, "Home Control Suite", "Started succesfully", ToolTipIcon.None);
            //notifyIcon.ShowBalloonTip(5000, "Home Control Suite", "Started succesfully", ToolTipIcon.Info);
        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
            //TODO: Set menu strip permanently
            notifyIcon.ContextMenuStrip.Items.Clear();
            notifyIcon.ContextMenuStrip.Items.Add("Home Control Suite");
            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            notifyIcon.ContextMenuStrip.Items.Add("Settings", null, new EventHandler(SettingsItem_Click));
            notifyIcon.ContextMenuStrip.Items.Add("Exit", null, new EventHandler(ExitItem_Click));
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
            notifyIcon.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
            //notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            //notifyIcon.MouseUp += notifyIcon_MouseUp;
        }

        private void SettingsItem_Click(object sender, EventArgs e)
        {
            FormSettings settingsForm = new FormSettings();
            settingsForm.Show();


        }

        /// <summary>
        /// When the exit menu item is clicked, make a call to terminate the ApplicationContext.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitItem_Click(object sender, EventArgs e)
        {
            ExitThread();
        }
    }
}
