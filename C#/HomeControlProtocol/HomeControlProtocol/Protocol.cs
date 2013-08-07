using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeControlProtocol
{
    public class Version
    {
        public static readonly float Protocol = 0.2F;
    }

    public class DataProtocol
    {
        public static readonly byte clientChangedValue = 0;
        public static readonly byte setClientValue = 2;
    }

    public class VariableProtocol
    {
        public static readonly string Off = "0";
        public static readonly string On = "1";
        public static readonly string Unlock = "2";
        public static readonly string QuickLock = "3";
        public static readonly string FullLock = "4";
        public static readonly string PlaybackStarted = "5";
        public static readonly string PlaybackResumed = "6";
        public static readonly string PlaybackPaused = "7";
        public static readonly string PlaybackStopped = "8";
        public static readonly string PlaybackEnded = "9";
    }

    public class ProtocolConversion
    {
        public static String getProtocolName(byte protocol)
        {
            if(protocol == DataProtocol.clientChangedValue)
                return "clientChangedValue";
            else if(protocol == DataProtocol.setClientValue)
                return "setClientValue";
            
            return protocol.ToString();
        }
    }
}