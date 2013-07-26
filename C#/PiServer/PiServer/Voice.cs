using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;

namespace PiServer
{
    class Voice
    {
        public SpeechSynthesizer voice;
        public Voice()
        {
            voice = new SpeechSynthesizer();
            voice.SpeakAsync("Voice initialized");
        }
    }
}
