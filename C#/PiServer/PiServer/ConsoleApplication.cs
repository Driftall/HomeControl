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
            ticker = new Timer(1000);
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
                }
            }
        }

        void ticker_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Tick yo");
        }
    }
}
