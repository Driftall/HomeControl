using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Diagnostics;
using System.IO;

namespace PiServer
{
    class Voice
    {
        public bool isMono = false;
        public Voice()
        {
            Type t = Type.GetType("Mono.Runtime");
            if (t != null)
            {
                isMono = true;
            }
            Speak("Voice initialized");
        }

        public void Speak(string text)
        {
            if(isMono)
                Execute("bash", "-c " + '"' + "flite -voice slt -t '" + text + "'" + '"');
        }

        //http://mono.1490590.n4.nabble.com/excute-linux-command-under-mono-C-td1533563.html
        internal int Execute(string exe, string args)
        {
            ProcessStartInfo oInfo = new ProcessStartInfo(exe, args);
            oInfo.UseShellExecute = false;
            oInfo.CreateNoWindow = true;

            oInfo.RedirectStandardOutput = true;
            oInfo.RedirectStandardError = true;

            StreamReader srOutput = null;
            StreamReader srError = null;

            Process proc = System.Diagnostics.Process.Start(oInfo);
            proc.WaitForExit();
            srOutput = proc.StandardOutput;
            StandardOutput = srOutput.ReadToEnd();
            srError = proc.StandardError;
            StandardError = srError.ReadToEnd();
            int exitCode = proc.ExitCode;
            proc.Close();

            return exitCode;
        }

        internal string StandardOutput
        {
            get;
            private set;
        }
        internal string StandardError
        {
            get;
            private set;
        }
    }
}
