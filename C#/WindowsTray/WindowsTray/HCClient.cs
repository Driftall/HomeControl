using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsTray
{
    public delegate void OnSettingSentFromServerHandler(string setting, string value); //could be a Setting?
    public delegate void OnValueRequestedFromServerHandler(string setting);
    public delegate void OnMessageReceivedFromServerHandler(string message);
    class HCClient : SocketLibrary.Client
    {
        public event OnSettingSentFromServerHandler SettingSentFromServer;
        public event OnValueRequestedFromServerHandler ValueRequestedFromServer;
        public event OnMessageReceivedFromServerHandler MessageReceivedFromServer;

        public HCClient(string Name) : base(Name)
        {
            this.DataReceivedFromServer += HCClient_DataReceivedFromServer;

        }

        void HCClient_DataReceivedFromServer(string data)
        {
            if (data.StartsWith(Protocol.getValue))
            {
                string setting = data.Remove(0, 3);
                ValueRequestedFromServer(setting);
            }
            else if(data.StartsWith(Protocol.setValue))
            {
                string setting = data.Remove(0, 3);
                string value = setting.Remove(0,4);
                SettingSentFromServer(setting, value);
            }
            else
            {
                MessageReceivedFromServer(data);
            }
        }

        public void SendValueToServer(string setting, Object value)
        {
            this.SendDataToServer(Protocol.gotValue + setting + value);
        }
    }
}
