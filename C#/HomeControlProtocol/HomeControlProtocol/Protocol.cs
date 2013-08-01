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
        public static readonly byte changedValue = 0;
        public static readonly byte setValue = 2;
    }

    public class DeviceProtocol
    {
        public static readonly byte Debug = 0;
        public static readonly byte LockStatus = 1;
        public static readonly byte IP = 2;
        public static readonly byte BatteryPercentage = 3;
        public static readonly byte Beep = 4;
        public static readonly byte DoorLCD = 5;
        public static readonly byte DateTime = 6;
        public static readonly byte Alarm = 7;
        public static readonly byte Timer = 8;
    }

    public class VariableProtocol
    {
        public static readonly string Off = "0";
        public static readonly string On = "1";
        public static readonly string Unlock = "2";
        public static readonly string QuickLock = "3";
        public static readonly string FullLock = "4";
    }

    public class DatabaseProtocol
    {
        public static readonly string Alarms = "Alarms";
    }

    public class ProtocolConversion
    {
        public static String getProtocolName(byte protocol)
        {
            if(protocol == DataProtocol.changedValue)
                return "changedValue";
            else if(protocol == DataProtocol.setValue)
                return "setValue";
            if (protocol == DeviceProtocol.Debug)
                return "Debug";
            else if (protocol == DeviceProtocol.LockStatus)
                return "LockStatus";
            else if (protocol == DeviceProtocol.IP)
                return "IP";
            else if (protocol == DeviceProtocol.BatteryPercentage)
                return "BatteryPercentage";
            else if (protocol == DeviceProtocol.Beep)
                return "Beep";
            else if (protocol == DeviceProtocol.DoorLCD)
                return "DoorLCD";
            else if (protocol == DeviceProtocol.DateTime)
                return "DateTime";
            else if (protocol == DeviceProtocol.Alarm)
                return "Alarm";
            else if (protocol == DeviceProtocol.Timer)
                return "Timer";
            throw new Exception("Not added");
        }
    }
}
