using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsTray
{
    class ProtocolProcessing
    {
        HCClient client;
        NotifyIcon notifyIcon;
        public ProtocolProcessing(HCClient Client, NotifyIcon Icon)
        {
            client = Client;
            notifyIcon = Icon;
            client.ConnectionFailed += client_ConnectionFailed;
            client.Connected += client_Connected;
            client.Disconnected += client_Disconnected;
            client.ValueRequestedFromServer += client_ValueRequestedFromServer;
            client.SettingSentFromServer += client_SettingSentFromServer;
            client.MessageReceivedFromServer += client_MessageReceivedFromServer;
        }

        void client_ConnectionFailed()
        {
            notifyIcon.ShowBalloonTip(5000, "Home Control Suite", "Connection failed", ToolTipIcon.Error);
        }

        void client_Connected()
        {
            notifyIcon.ShowBalloonTip(5000, "Home Control Suite", "Connected to server", ToolTipIcon.Info);
        }

        void client_Disconnected()
        {
            notifyIcon.ShowBalloonTip(5000, "Home Control Suite", "Connection lost", ToolTipIcon.Error);
        }

        void client_SettingSentFromServer(string setting, string value)
        {
            if (setting == Setting.Beep)
            {
                SystemSounds.Beep.Play();
            }
        }

        void client_ValueRequestedFromServer(string setting)
        {
            if (setting == Setting.IP)
            {
                client.SendValueToServer(Setting.IP, (Object)getLocalIP());
            }
            else if (setting == Setting.BatteryPercentage)
            {
                client.SendValueToServer(Setting.BatteryPercentage, SystemInformation.PowerStatus.BatteryLifePercent);
            }
        }

        void client_MessageReceivedFromServer(string data)
        {
            notifyIcon.ShowBalloonTip(5000, "Server Message", data, ToolTipIcon.Info);
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
