using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VosSoft.ZuneLcd.Api;

namespace WindowsTray
{
    class ZuneInterface
    {
        ZuneLcdApi zune;
        NotifyIcon notifyIcon;
        public ZuneInterface(NotifyIcon icon)
        {
            notifyIcon = icon;
            zune = new ZuneLcdApi();
            zune.TrackChanged += zune_TrackChanged;
            zune.Launch();
        }

        void zune_TrackChanged(object sender, EventArgs e)
        {
            notifyIcon.ShowBalloonTip(5000, "Track Changed", zune.CurrentTrack.Artist + " - " + zune.CurrentTrack.Title, ToolTipIcon.Info);
        }
    }
}
