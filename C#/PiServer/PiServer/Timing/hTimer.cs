using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PiServer
{
    public delegate void TimerCompleteHandler(string name);
    class hTimer
    {
        public event TimerCompleteHandler TimerComplete;
        string name;
        int minutesLeft;
        int secondsLeft;
        Timer timer;

        public hTimer(string Name, int Minutes, int Seconds)
        {
            name = Name;
            minutesLeft = Minutes;
            secondsLeft = Seconds;
            timer = new Timer(1000);
            timer.Elapsed += timer_Elapsed;
            timer.AutoReset = true;
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (secondsLeft > 0)
            {
                secondsLeft--;
                if ((minutesLeft > 0) && (secondsLeft == 0))
                {
                    minutesLeft = 0;
                    secondsLeft = 59;
                }
            }
            if ((secondsLeft == 0) && (minutesLeft == 0))
            {
                TimerComplete(name);
                timer.Stop();
            }
        }
    }
}
