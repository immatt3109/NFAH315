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
    public static class HDMD
    {

        public static void RouteVideo(uint video)
        {
            if (video > 0)
            {
                ControlSystem.hdmd.HdmiOutputs[1].VideoOut = ControlSystem.hdmd.HdmiInputs[video];
            }

            else
                ControlSystem.hdmd.HdmiOutputs[1].VideoOut = null;
        }

        public static void LockPanel(bool status)
        {
            if (status)
                ControlSystem.hdmd.EnableFrontPanelLock();
            else
                ControlSystem.hdmd.DisableFrontPanelLock();
        }

        public static void FrontLED(bool status)
        {
            if (status)
                ControlSystem.hdmd.EnableFrontPanelLed();
            else
                ControlSystem.hdmd.DisableFrontPanelLed();
        }

        public static void AutoRoute(bool status)
        {
            if (status)
                ControlSystem.hdmd.AutoRouteOn();
            else
                ControlSystem.hdmd.AutoRouteOff();
        }
    }
}
