using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomeControlProtocol;
using System.Timers;

namespace PiServer
{
    class ConsoleApplication
    {
        ProtocolProcessor cpu;
        public ConsoleApplication()
        {
            Console.WriteLine("Home Control PiServer");
            Console.WriteLine("Raspberry Pi server for the Home Control Suite");

            cpu = new ProtocolProcessor();
            bool run = true;
            while (run)
            {
                string input = Console.ReadLine();
                string cmd = input.Split(' ')[0];
                switch (cmd)
                {
                    case "debug":
                        {
                            string debug = input.Remove(0, cmd.Length + 1);
                            cpu.server.SendDebugToClient("BLAKE-PC", DeviceProtocol.DoorLCD, debug);
                            break;
                        }
                    case "message":
                        {
                            string message = input.Remove(0, cmd.Length + 1);
                            cpu.server.SendMessageToClient("BLAKE-PC", message);
                            break;
                        }
                    case "weather":
                        {
                            Weather weather = Wunderground.GetWeather("IP26 4LB");
                            Console.WriteLine("The weather in " + weather.City + "(" + weather.Postcode + ") is " + weather.TempC + " degrees celcius with " + weather.WeatherDesc);
                            break;
                        }
                    case "timer":
                        {
                            hTimer tempTimer = new hTimer("Test timer", 0, 20);
                            tempTimer.TimerComplete += timer_TimerComplete;
                            break;
                        }
                    case "alarm":
                        {
                            hAlarm tempAlarm = new hAlarm("Test alarm", DateTime.Now.AddSeconds(10));
                            tempAlarm.AlarmActive += alarm_AlarmActive;
                            break;
                        }
                    case "wakeup":
                        {
                            Weather weather = Wunderground.GetWeather("IP26 4LB");
                            DateTime tempTime = DateTime.Now;
                            String finalString;
                            String hourString;
                            String minuteString;
                            String amPMstring;
                            String dateString;
                            if (tempTime.Hour > 12)
                            {
                                amPMstring = "PM";
                                hourString = (tempTime.Hour - 12).ToString();
                            }
                            else
                            {
                                amPMstring = "AM";
                                hourString = tempTime.Hour.ToString();
                            }
                            if (tempTime.Minute == 0)
                                minuteString = "";
                            else
                            {
                                minuteString = ":";
                                if (tempTime.Minute < 10)
                                    minuteString = minuteString + "0" + tempTime.Minute.ToString();
                                else
                                    minuteString = tempTime.Minute.ToString();
                            }
                            dateString = "today is Friday the 26th of July";
                            finalString = hourString + minuteString + " " + amPMstring + ", " + dateString;
                            //String weatherDescString;
                            //if(weatherDescString.StartsWith("Partlweather.WeatherDesc;
                            String wakeupString = "Good Morning Sir, it is " + finalString + ", the weather in " + weather.City + " is " + weather.TempC + " degrees and " + weather.WeatherDesc;
                            cpu.voice.voice.SpeakAsync(wakeupString);
                            Console.WriteLine(wakeupString);
                            break;
                        }
                    case "setwakeup":
                        {
                            DateTime wakeup = new DateTime(1, 1, 1, 8, 0, 0);
                            SQLiteDatabase sqlite = new SQLiteDatabase("saved.db3");
                            Dictionary<string, string> table = new Dictionary<string,string>();
                            table["Name"] = "wakeupAlarm";
                            table["Data"] = wakeup.ToShortTimeString();
                            sqlite.Update("staticEvents", table, "Name = 'wakeupAlarm'");
                            //sqlite.Insert("staticEvents", table);
                            break;
                        }
                }
            }
        }

        void alarm_AlarmActive(string name)
        {
            Console.WriteLine(name + " activated");
        }

        void timer_TimerComplete(string name)
        {
            Console.WriteLine(name + " completed");
        }
    }
}
