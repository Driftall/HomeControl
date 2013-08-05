using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiServer
{
    class Events
    {
        public delegate void DoEventHandler(string client, byte device, string value);
        public event DoEventHandler HandleEvent;
        string file;
        Dictionary<string, string> eventList;
        public Events(string masterFile)
        {
            file = masterFile;
            eventList = new Dictionary<string, string>();
            if (File.Exists(masterFile))
            {
                string[] lines = File.ReadAllLines(masterFile);
                foreach (string line in lines)
                {
                    string[] lineData = line.Split(';');
                    eventList[lineData[0]] = lineData[1];
                }
            }
            else
            {
                File.Create(masterFile);
            }
        }

        public string[] getEvents()
        {
            string[] events = eventList.Keys.ToArray();
            return events;
        }

        public void CheckEvents(Dictionary<byte, String> protocol)
        {
            string[] keys = eventList.Keys.ToArray();
            foreach (string key in keys)
            {
                string[] stringSeparators = new string[] {" AND "};
                string[] conditions = key.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                bool continueEvent = true;
                foreach (string condition in conditions)
                {
                    if (condition.Contains(" == "))
                    {
                        stringSeparators = new string[] { " == " };
                        string[] data = condition.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        byte device = HomeControlProtocol.ProtocolConversion.getProtocolByte(data[0]);
                        if (protocol[device] != data[1])
                        {
                            continueEvent = false;
                        }
                    }
                }
                if(continueEvent)
                    DoEvent(key);
            }
        }

        public void DoEvent(string Event)
        {
            string file = eventList[Event];
            string[] lines = File.ReadAllLines(file);
            foreach (string toDo in lines)
            {
                //http://stackoverflow.com/questions/1126915/how-do-i-split-a-string-by-a-multi-character-delimiter-in-c
                string[] stringSeparators = new string[] {" = "};
                string[] data = toDo.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                byte device = HomeControlProtocol.ProtocolConversion.getProtocolByte(data[0]);
                string value = data[1];
                HandleEvent("SERVER", device, value);
            }
        }
    }
}
