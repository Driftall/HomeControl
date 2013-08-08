using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;

using MqttLib;

namespace WindowsTray
{
    public delegate void OnNotify(int timeout, String tipTitle, String tipText, ToolTipIcon tipIcon);
    class ProtocolProcessor
    {
        public event OnNotify Notify;
        [DllImport("user32")]
        public static extern void LockWorkStation();

        public bool isLaptop = true;
        float lastBatteryPercentage;

        IMqtt client;

        Timer timer;

        public ProtocolProcessor(String clientName, String IP)
        {
            if(SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.NoSystemBattery)
            {
                isLaptop = false;
            }

            Microsoft.Win32.SystemEvents.SessionSwitch += new Microsoft.Win32.SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            Microsoft.Win32.SystemEvents.PowerModeChanged += new Microsoft.Win32.PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);

            client = MqttClientFactory.CreateClient("tcp://" + IP + ":1883", Environment.MachineName);
            client.Connected += client_Connected;
            client.ConnectionLost += client_ConnectionLost;
            client.PublishArrived += client_PublishArrived;
            client.Published += client_Published;
            client.Subscribed += client_Subscribed;
            client.Unsubscribed += client_Unsubscribed;
            client.Connect();
            client.Subscribe("BLAKE-PC/#", 0);
            client.Subscribe("UNIVERSAL/#", 0);

            timer = new Timer();
            timer.Interval = 10000;
            timer.Tick += timer_Tick;
            timer.Enabled = true;
        }

        void client_Connected(object sender, EventArgs e)
        {
            client.Publish("CONNECTED", Environment.MachineName, 0, true);
        }

        void client_ConnectionLost(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        bool client_PublishArrived(object sender, PublishArrivedArgs e)
        {
            string topic = e.Topic;
            if (e.Topic.StartsWith("UNIVERSAL"))
            {
                topic = e.Topic.Remove(0, 10);
            }
            else if (e.Topic.StartsWith(Environment.MachineName))
            {
                topic = e.Topic.Remove(0, Environment.MachineName.Length + 1);
            }
            if (topic == "SESSION")
            {
                if (e.Payload == "LOCK")
                {
                    LockWorkStation();
                }
            }
            else if (topic == "BEEP")
            {
                SystemSounds.Beep.Play();
            }
            return true;
        }

        void client_Published(object sender, CompleteArgs e)
        {
            throw new NotImplementedException();
        }

        void client_Subscribed(object sender, CompleteArgs e)
        {
            throw new NotImplementedException();
        }

        void client_Unsubscribed(object sender, CompleteArgs e)
        {
            throw new NotImplementedException();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (isLaptop)
            {
                float batteryPercentage = SystemInformation.PowerStatus.BatteryLifePercent * 100;
                if (batteryPercentage != lastBatteryPercentage)
                {
                    lastBatteryPercentage = batteryPercentage;
                    client.Publish("SERVER/BATTERY/" + Environment.MachineName, batteryPercentage.ToString(), 0, true);
                }
            }
        }

        void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Suspend)
            {
                client.Publish("SERVER/POWER/" + Environment.MachineName, "SUSPEND", 0, true);
            }
            else if (e.Mode == PowerModes.Resume)
            {
                client.Publish("SERVER/POWER/" + Environment.MachineName, "RESUME", 0, true);
            }
        }

        void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                client.Publish("SERVER/SESSION/" + Environment.MachineName, "LOCK", 0, true);
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                client.Publish("SERVER/SESSION/" + Environment.MachineName, "UNLOCK", 0, true);
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

        void client_MessageReceivedFromServer(string data)
        {
            Notify(5000, "Server Message", data, ToolTipIcon.Info);
        }
    }
}
