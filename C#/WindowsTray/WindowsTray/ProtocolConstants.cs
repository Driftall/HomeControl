using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsTray
{
    class Protocol
    {
        public static readonly string getValue = "00/";
        public static readonly string gotValue = "01/";
        public static readonly string setValue = "02/";
    }

    class Setting
    {
        public static readonly string Volume = "001/";
        public static readonly string IP = "002/";
        public static readonly string BatteryPercentage = "003/";
        public static readonly string Beep = "004/";
    }
}
