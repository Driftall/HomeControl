using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsTray
{
    //public delegate void OnSettingReceivedFromServerHandler(string 
    class HCClient : SocketLibrary.Client
    {
        public HCClient(string Name) : base(Name)
        {
            this.DataReceivedFromServer += HCClient_DataReceivedFromServer;

        }

        void HCClient_DataReceivedFromServer(string data)
        {
            throw new NotImplementedException();
        }

        public void setValue(int setting, object value)
        {
            this.SendDataToServer(ProtocolConstants.SETTING + setting.ToString() + value);
        }
    }
}
