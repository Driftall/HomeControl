using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HomeControlProtocol;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace WindowsTray
{
    public delegate void OnNotify(int timeout, String tipTitle, String tipText, ToolTipIcon tipIcon);
    class ProtocolProcessor
    {
        public event OnNotify Notify;
        [DllImport("user32")]
        public static extern void LockWorkStation();

        public bool isLaptop = true;

        HomeClient client;

        Dictionary<string, string> proto;

        public ProtocolProcessor(String clientName, String IP, int Port)
        {
            if(SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.NoSystemBattery)
            {
                isLaptop = false;
            }
            Microsoft.Win32.SystemEvents.SessionSwitch += new Microsoft.Win32.SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            client = new HomeClient(clientName);
            client.Connected += client_Connected;
            client.ConnectionFailed += client_ConnectionFailed;
            client.Disconnected += client_Disconnected;
            client.DebugReceivedFromServer += client_DebugReceivedFromServer;
            client.SettingSentFromServer += client_SettingSentFromServer;
            client.MessageReceivedFromServer += client_MessageReceivedFromServer;
            client.Connect(IP, Port);
            proto = new Dictionary<string, string>();
        }

        void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                //I left my desk
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                client.SendSettingToServer(DeviceProtocol.LockStatus, "false");
            }
        }

        void client_ConnectionFailed()
        {
            Notify(5000, "Home Control Suite", "Connection failed", ToolTipIcon.Error);
        }

        void client_Connected(string welcomeMessage)
        {
            Notify(5000, "Home Control Suite", welcomeMessage, ToolTipIcon.Info);
        }

        void client_Disconnected()
        {
            Notify(5000, "Home Control Suite", "Connection lost", ToolTipIcon.Error);
        }

        void client_DebugReceivedFromServer(string device, string debug)
        {
            Notify(5000, "Debug Message", device + debug, ToolTipIcon.Info);
        }

        void client_SettingSentFromServer(string setting, string value)
        {
            if ((setting == DeviceProtocol.LockStatus) && (value == "true"))
            {
                LockWorkStation();
            }
            else if (setting == DeviceProtocol.Beep)
            {
                SystemSounds.Beep.Play();
            }
        }

        void client_MessageReceivedFromServer(string data)
        {
            Notify(5000, "Server Message", data, ToolTipIcon.Info);
        }

        public void timerTick()
        {
            if (isLaptop)
            {
                float batteryPercentage = SystemInformation.PowerStatus.BatteryLifePercent * 100;
                if (batteryPercentage.ToString() != proto[DeviceProtocol.BatteryPercentage])
                {
                    client.ChangeValueOnServer(DeviceProtocol.BatteryPercentage, batteryPercentage.ToString());
                }
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
    }
}
