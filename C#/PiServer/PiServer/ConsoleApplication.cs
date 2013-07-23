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
        Timer ticker;
        public ConsoleApplication()
        {
            Console.WriteLine("Home Control PiServer");
            Console.WriteLine("Raspberry Pi server for the Home Control Suite");

            cpu = new ProtocolProcessor();
            ticker = new Timer(5000);
            ticker.AutoReset = true;
            ticker.Elapsed += ticker_Elapsed;
            ticker.Start();
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
                }
            }
        }

        void ticker_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Tick yo");
            //Thu 18 Jul 18:09
            //DayOfWeek.Friday
            int dayInt = (int)DateTime.Now.DayOfWeek;
            if (dayInt == 0) dayInt = 7;
            String day = "0" + dayInt.ToString();
            String date = DateTime.Now.Day.ToString();
            if (date.Length != 2) date = "0" + date;
            String month = DateTime.Now.Month.ToString();
            if (month.Length != 2) month = "0" + month;
            String hour = DateTime.Now.Hour.ToString();
            if (hour.Length != 2) hour = "0" + hour;
            String minute = DateTime.Now.Minute.ToString();
            if (minute.Length != 2) minute = "0" + minute;
            Console.WriteLine(day.ToString() + " " + date + " " + month + " " + hour + ":" + minute);
            //cpu.Tick();
        }
    }
}
