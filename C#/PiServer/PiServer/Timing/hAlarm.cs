using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using HomeControlProtocol;

namespace PiServer
{
    class hAlarm
    {
        string name;
        DateTime alarmDateTime;

        public hAlarm(string Name, DateTime alarm)
        {
            name = Name;
            alarmDateTime = alarm;
            SQLiteDatabase database = new SQLiteDatabase("saved.db3");
            Dictionary<string, string> alarmDict = new Dictionary<string,string>();
            alarmDict["Enabled"] = "1";
            alarmDict["Name"] = Name;
            alarm = alarm.AddSeconds(60-alarm.Second);
            alarmDict["DateTime"] = alarm.ToString();
            database.Insert(DatabaseProtocol.Alarms,alarmDict);
        }
    }
}
