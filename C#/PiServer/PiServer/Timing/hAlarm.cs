using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using HomeControlProtocol;

namespace PiServer
{
    public delegate void AlarmActiveHandler(string name);
    class hAlarm
    {
        public event AlarmActiveHandler AlarmActive;
        string name;
        DateTime alarmDateTime;
        Timer timer;

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
            /*timer = new Timer(1000);
            timer.Elapsed += timer_Elapsed;
            timer.AutoReset = true;
            timer.Start();*/
        }

        /*void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now.Year == alarmDateTime.Year)
            {
                if (DateTime.Now.Month == alarmDateTime.Month)
                {
                    if (DateTime.Now.Day == alarmDateTime.Day)
                    {
                        if (DateTime.Now.Hour == alarmDateTime.Hour)
                        {
                            if (DateTime.Now.Minute == alarmDateTime.Minute)
                            {
                                AlarmActive(name);
                                timer.Stop();
                            }
                        }
                    }
                }
            }
        }*/
    }
}
