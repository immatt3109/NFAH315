using Crestron.SimplSharp;
using Crestron.SimplSharpPro.DeviceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crestron.SimplSharpPro.DM;

namespace NFAHRooms
{
    internal class HDMD
    {

        private HdMd4x14kzE hdmd;

        public HDMD(HdMd4x14kzE hdmd)
        {
            this.hdmd = hdmd;
        }

        public void RouteVideo(uint video)
        {
            if (video > 0)
            {
                hdmd.HdmiOutputs[1].VideoOut = hdmd.HdmiInputs[video];
            }

            else
                hdmd.HdmiOutputs[1].VideoOut = null;
        }

        public void LockPanel(bool status)
        {
            if (status)
                hdmd.EnableFrontPanelLock();
            else
                hdmd.DisableFrontPanelLock();
        }

        public void FrontLED(bool status)
        {
            if (status)
                hdmd.EnableFrontPanelLed();
            else
                hdmd.DisableFrontPanelLed();
        }

        public void AutoRoute(bool status)
        {
            if (status)
                hdmd.AutoRouteOn();
            else
                hdmd.AutoRouteOff();
        }
    }
}
