using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeControlProtocol
{
    public class DataProtocol
    {
        public static readonly string changedValue = "00/";
        public static readonly string setValue = "02/";
    }

    public class DeviceProtocol
    {
        public static readonly string Debug = "000/";
        public static readonly string LockStatus = "001/";
        public static readonly string IP = "002/";
        public static readonly string BatteryPercentage = "003/";
        public static readonly string Beep = "004/";
	    public static readonly string DoorLCD = "005/";
        public static readonly string DateTime = "006/";
    }

    public class VariableProtocol
    {
        public static readonly string Off = "0";
        public static readonly string On = "1";
        public static readonly string Unlock = "2";
        public static readonly string QuickLock = "3";
        public static readonly string FullLock = "4";
    }
}
