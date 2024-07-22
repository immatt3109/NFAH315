using System;
using Crestron.SimplSharp;                          	// For Basic SIMPL# Classes
using Crestron.SimplSharpPro;                       	// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.CrestronThread;        	// For Threading
using Crestron.SimplSharpPro.UI;
using Crestron.SimplSharpPro.DM.AirMedia;
using Crestron.SimplSharpPro.DM;
using Crestron.SimplSharpPro.CrestronConnected;         	// For Generic Device Support
using Crestron.SimplSharp.Scheduler;
using Crestron.SimplSharpPro.DeviceSupport;


namespace NFAHRooms
{    public class ControlSystem : CrestronControlSystem
    {
        public static Ts1070 tp;
        public static Am300 am3200;
        public static HdMd4x14kzE hdmd;
        public static CrestronConnectedDisplayV2 disp1;
        public static CrestronConnectedDisplayV2 disp2;
        public static CrestronConnectedDisplayV2 disp3;
        public static RoomViewConnectedDisplay proj1;
        public static RoomViewConnectedDisplay proj2;
        public static RoomViewConnectedDisplay proj3;
                
        public ControlSystem() : base()
        {
            string configRoomFilePath = "/user/room_setup.json";
            RoomSetup.LoadRoomSetup(configRoomFilePath);
            
            try
            {
                Thread.MaxNumberOfUserThreads = 20;
                 
                CrestronEnvironment.SystemEventHandler += new SystemEventHandler(_ControllerSystemEventHandler);
                CrestronEnvironment.ProgramStatusEventHandler += new ProgramStatusEventHandler(_ControllerProgramEventHandler);
                CrestronEnvironment.EthernetEventHandler += new EthernetEventHandler(_ControllerEthernetEventHandler);

                if (RoomSetup.RoomType.ToLower() == "huddle_room")
                {
                    tp = new Ts1070(0x03, this);
                    hdmd = new HdMd4x14kzE(0x04, this);
                    disp1 = new CrestronConnectedDisplayV2(0x05, this);
                    am3200 = new Am300(0x06, this);
                  
                    HuddleHandler huddleHandler = new HuddleHandler(tp, hdmd, am3200, disp1);
                    
                    huddleHandler.Initialize();
                    CrestronConsole.PrintLine("Huddle Room Setup");
                }
                else if (RoomSetup.RoomType.ToLower() == "evertz_room")
                {                    
                    tp = new Ts1070(0x03, this);
                    am3200 = new Am300(0x06, this);

                    if (RoomSetup.Display1 == "proj")
                        proj1 = new RoomViewConnectedDisplay(0x05, this);

                    if (RoomSetup.Display2 == "proj")
                        proj2 = new RoomViewConnectedDisplay(0x15, this);

                    if (RoomSetup.Display3 == "proj")
                        proj3 = new RoomViewConnectedDisplay(0x25, this);

                    if (RoomSetup.Display1 == "tv")
                        disp1 = new CrestronConnectedDisplayV2(0x05, this);

                    if (RoomSetup.Display2 == "tv")
                        disp2 = new CrestronConnectedDisplayV2(0x15, this);

                    if (RoomSetup.Display3 == "tv")
                        disp3 = new CrestronConnectedDisplayV2(0x25, this);

                    EvertzHandler.Initialize();
                    
                    CrestronConsole.PrintLine("Evertz Room Setup");
                }
                else if (RoomSetup.RoomType.ToLower() == "nvx_room")
                {
                    CrestronConsole.PrintLine("NVX Room Setup");
                }
                else
                {
                    CrestronConsole.PrintLine("Room Type not found");
                    throw new Exception("Room Type not found.  huddle_room/evertz_room/nvx_room");
                }

                
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in the constructor: {0}", e.Message);
                CrestronConsole.PrintLine("Error in the constructor: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject, e.Message);
            }
        }

        public override void InitializeSystem()
        {
            try
            {   SNTP.Server = RoomSetup.Crestron.SntpServer;
                SNTP.Enable();
                CrestronEnvironment.SetTimeZone(Convert.ToInt32(RoomSetup.Crestron.TimezoneId));

                string pResponse = null;
                CrestronConsole.SendControlSystemCommand("SNTP server:" + RoomSetup.Crestron.SntpServer, ref pResponse);
                CrestronConsole.SendControlSystemCommand("SNTP start", ref pResponse);

                Email.Initialize();
                
                Scheduling.SystemEventGroup = new ScheduledEventGroup("NFAH");
                Scheduling.SystemEventGroup.ClearAllEvents();
                Scheduling.AddDailyTimerEvent();

                am3200.HdmiOut.Resolution = CommonStreamingSupport.eScreenResolutions.Resolution1080p60Hz;
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in InitializeSystem: {0}", e.Message);
                CrestronConsole.PrintLine("Error in InitializeSystem: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject, e.Message);
            }
        }

        /// <summary>
        /// Event Handler for Ethernet events: Link Up and Link Down. 
        /// Use these events to close / re-open sockets, etc. 
        /// </summary>
        /// <param name="ethernetEventArgs">This parameter holds the values 
        /// such as whether it's a Link Up or Link Down event. It will also indicate 
        /// wich Ethernet adapter this event belongs to.
        /// </param>
        void _ControllerEthernetEventHandler(EthernetEventArgs ethernetEventArgs)
        {
            switch (ethernetEventArgs.EthernetEventType)
            {//Determine the event type Link Up or Link Down
                case (eEthernetEventType.LinkDown):
                    //Next need to determine which adapter the event is for. 
                    //LAN is the adapter is the port connected to external networks.
                    if (ethernetEventArgs.EthernetAdapter == EthernetAdapterType.EthernetLANAdapter)
                    {
                        //
                    }
                    break;
                case (eEthernetEventType.LinkUp):
                    if (ethernetEventArgs.EthernetAdapter == EthernetAdapterType.EthernetLANAdapter)
                    {

                    }
                    break;
            }
        }

        /// <summary>
        /// Event Handler for Programmatic events: Stop, Pause, Resume.
        /// Use this event to clean up when a program is stopping, pausing, and resuming.
        /// This event only applies to this SIMPL#Pro program, it doesn't receive events
        /// for other programs stopping
        /// </summary>
        /// <param name="programStatusEventType"></param>
        void _ControllerProgramEventHandler(eProgramStatusEventType programStatusEventType)
        {
            switch (programStatusEventType)
            {
                case (eProgramStatusEventType.Paused):
                    //The program has been paused.  Pause all user threads/timers as needed.
                    break;
                case (eProgramStatusEventType.Resumed):
                    //The program has been resumed. Resume all the user threads/timers as needed.
                    break;
                case (eProgramStatusEventType.Stopping):
                    //The program has been stopped.
                    //Close all threads. 
                    //Shutdown all Client/Servers in the system.
                    //General cleanup.
                    //Unsubscribe to all System Monitor events
                    break;
            }

        }

        /// <summary>
        /// Event Handler for system events, Disk Inserted/Ejected, and Reboot
        /// Use this event to clean up when someone types in reboot, or when your SD /USB
        /// removable media is ejected / re-inserted.
        /// </summary>
        /// <param name="systemEventType"></param>
        void _ControllerSystemEventHandler(eSystemEventType systemEventType)
        {
            switch (systemEventType)
            {
                case (eSystemEventType.DiskInserted):
                    //Removable media was detected on the system
                    break;
                case (eSystemEventType.DiskRemoved):
                    //Removable media was detached from the system
                    break;
                case (eSystemEventType.Rebooting):
                    //The system is rebooting. 
                    //Very limited time to preform clean up and save any settings to disk.
                    break;
            }

        }

    }
}