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
