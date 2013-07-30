using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsTray
{
    public delegate void OnDataPassedHandler(String data);
    public partial class FormSettings : Form
    {
        public event OnDataPassedHandler OnDataPassed;
        public bool formOpen = false;

        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            textBoxIP.Text = Settings.getSetting(Settings.IP);
            textBoxPort.Text = Settings.getSetting(Settings.Port);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Settings.saveSetting(Settings.IP, textBoxIP.Text);
            Settings.saveSetting(Settings.Port, textBoxPort.Text);
            //this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void buttonServerConnect_Click(object sender, EventArgs e)
        {
            OnDataPassed("connect");
            OnDataPassed("connectionStatus");
        }

        private void buttonServerDisconnect_Click(object sender, EventArgs e)
        {
            OnDataPassed("disconnect");
            OnDataPassed("connectionStatus");
        }

        public void setConnectionStatus(string status)
        {
            labelServerStatus.Text = status;
        }
    }
}
