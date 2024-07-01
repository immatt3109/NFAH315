using System;
using Crestron.SimplSharp;                          	// For Basic SIMPL# Classes
using Crestron.SimplSharpPro;                       	// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.CrestronThread;        	// For Threading
using Crestron.SimplSharpPro.UI;
using Crestron.SimplSharpPro.DM.AirMedia;
using Crestron.SimplSharpPro.DM;
using Crestron.SimplSharpPro.CrestronConnected;         	// For Generic Device Support
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharp.CrestronIO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Crestron.SimplSharp.Scheduler;
using Crestron.SimplSharp.Ssh;
using Independentsoft.Exchange;
using Newtonsoft.Json.Linq;
using Crestron.SimplSharp.Net;
using System.Diagnostics;
using Crestron.SimplSharp.CrestronAuthentication;
using System.Linq;


namespace NFAHRooms
{

    public class ControlSystem : CrestronControlSystem
    {
        /// <summary>
        /// ControlSystem Constructor. Starting point for the SIMPL#Pro program.
        /// Use the constructor to:
        /// * Initialize the maximum number of threads (max = 400)
        /// * Register devices
        /// * Register event handlers
        /// * Add Console Commands
        /// 
        /// Please be aware that the constructor needs to exit quickly; if it doesn't
        /// exit in time, the SIMPL#Pro program will exit.
        /// 
        /// You cannot send / receive data in the constructor
        /// </summary>
        /// 

        private RoomSetup roomSetup;
        private Email ErrorEmail;
        private Ts1070 tp;
        private Am300 am3200;
        private HdMd4x14kzE hdmd;
        private CrestronConnectedDisplayV2 disp1;
        private readonly CmdLine cmd;

        

        

        public ControlSystem() : base()
        {
            ErrorEmail = new Email();
            ErrorEmail.EmailSetup();

            string configRoomFilePath = "/user/room_setup.json";
            roomSetup = RoomSetup.LoadRoomSetup(configRoomFilePath);
            
            
            try
            {
                Thread.MaxNumberOfUserThreads = 20;

                

                //Subscribe to the controller events (System, Program, and Ethernet)
                CrestronEnvironment.SystemEventHandler += new SystemEventHandler(_ControllerSystemEventHandler);
                CrestronEnvironment.ProgramStatusEventHandler += new ProgramStatusEventHandler(_ControllerProgramEventHandler);
                CrestronEnvironment.EthernetEventHandler += new EthernetEventHandler(_ControllerEthernetEventHandler);

                if (roomSetup.RoomType == "huddle_room")
                {
                    tp = new Ts1070(0x03, this);
                    hdmd = new HdMd4x14kzE(0x04, this);
                    disp1 = new CrestronConnectedDisplayV2(0x05, this);
                    am3200 = new Am300(0x06, this);

                    cmd = new CmdLine(roomSetup, ErrorEmail, disp1, hdmd);
                    cmd.AddDevice("tp", new Ts1070Device(tp));
                    cmd.AddDevice("hdmd", new HdMd4x14kzEDevice(hdmd));
                    cmd.AddDevice("disp1", new CrestronConnectedDisplayV2Device(disp1));
                    cmd.AddDevice("am3200", new Am300Device(am3200));
                    
                    HuddleHandler huddleHandler = new HuddleHandler(tp, hdmd, am3200, disp1, roomSetup);
                    HDMD hDMD = new HDMD(hdmd);

                    huddleHandler.Initialize();

                    

                    CrestronConsole.PrintLine("Huddle Room Setup");
                    //HuddleRoomSetup();
                }
                else if (roomSetup.RoomType == "evertz_room")
                {
                    CrestronConsole.PrintLine("Evertz Room Setup");
                    //EvertzRoomSetup();
                }
                else if (roomSetup.RoomType == "nvx_room")
                {
                    CrestronConsole.PrintLine("NVX Room Setup");
                    //NvxRoomSetup();
                }
                else
                {
                    CrestronConsole.PrintLine("Room Type not found");
                    throw new Exception("Room Type not found.  huddle_room/evertz_room/nvx_room");
                }

                CrestronConsole.AddNewConsoleCommand(DeviceStatus, "status", "Check Crestron Device Online Status", ConsoleAccessLevelEnum.AccessOperator);
                CrestronConsole.AddNewConsoleCommand(AlertTest, "AlertTest", "Test Timer Alerts", ConsoleAccessLevelEnum.AccessOperator);
                CrestronConsole.AddNewConsoleCommand(HdmdControls,"HDMD", "Controls for HDMD", ConsoleAccessLevelEnum.AccessOperator);  
                CrestronConsole.AddNewConsoleCommand(TVControls, "TV", "Controls for Display", ConsoleAccessLevelEnum.AccessOperator);
                CrestronConsole.AddNewConsoleCommand(AMControls, "AM", "Controls for AirMedia", ConsoleAccessLevelEnum.AccessOperator);
                CrestronConsole.AddNewConsoleCommand(TPControls, "TP", "Controls for Touchpanel", ConsoleAccessLevelEnum.AccessOperator);

                
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in the constructor: {0}", e.Message);
                CrestronConsole.PrintLine("Error in the constructor: {0}", e.Message);
                ErrorEmail.SendEmail(roomSetup.MailSubject, e.Message);
            }
        }
        public void HdmdControls(string param)
        {
            try
            {
                var ip = hdmd.ConnectedIpList[0].DeviceIpAddress;

                if (string.IsNullOrEmpty(param) || param == "?")
                {
                    CrestronConsole.PrintLine("Valid usage is 'HDMD' <cmd> <value>\r\nValid < cmd > parameters: led, lock, route, reboot\r\nValid <led> values: on, off\r\n" +
                        "Valid <lock> values: on,off\r\n" + "Valid <route> values: 1,2,3,4\r\n" + "ex. <HDMD led on> or HDMD route 1\r\n");
                    return;
                }

                if (param.ToLower() == "reboot")
                {
                    string cmdvalue = param;
                    string val = null;
                    cmd.HDMDControls(cmdvalue, val, ip, Constants.UserName, Constants.Password);
                    return;
                }               

                if (!param.Contains(" "))
                {
                    CrestronConsole.PrintLine("Error.  Valid usage is 'HDMD' <cmd> <value>");
                    return;
                }

                var data = param.Split(' ');

                if (data.Length != 2)
                {
                    CrestronConsole.PrintLine("Error.  Valid usage is 'HDMD' <cmd> <value>");
                    return;
                }
                
                var command = data[0];
                var value = data[1];
                 
                cmd.HDMDControls(command, value, ip, Constants.UserName, Constants.Password);
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error in HDMDControls: {0}", e.Message);
                ErrorLog.Error("Error in HDMDControls: {0}", e.Message);
                ErrorEmail.SendEmail(roomSetup.MailSubject, e.Message);
            } 
        }

        public void TVControls(string param)
        {
            try
            {
                if (string.IsNullOrEmpty(param) || param == "?")
                {
                    CrestronConsole.PrintLine("Valid usage is TV <cmd> <value>\r\nValid <cmd> parameters: pwr, hdmi\r\nValid <pwr> values: on, off\r\n" +
                        "Valid <hdmi> values: 1, 2, 3, 4\r\n" + "ex. <TV pwr on> or TV hdmi 1\r\n" );
                    return;
                }

                if (!param.Contains(" "))
                {
                    CrestronConsole.PrintLine("Error.  Valid usage is 'TV' <cmd> <value>");
                    return;
                }

                var data = param.Split(' ');

                if (data.Length != 2)
                {
                    CrestronConsole.PrintLine("Error.  Valid usage is 'TV' <cmd> <value>");
                    return;
                }
                             
                var command = data[0];
                var value = data[1];
                
                cmd.TvControls(command, value);
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error in TVControls: {0}", e.Message);
                ErrorLog.Error("Error in TVControls: {0}", e.Message);
                ErrorEmail.SendEmail(roomSetup.MailSubject, e.Message);
            }
        }

        public void AMControls(string param)
        {
            try
            {
                if (string.IsNullOrEmpty(param) || param == "?")
                {
                    CrestronConsole.PrintLine("Valid usage is AM <cmd>\r\nValid <cmd> values: reboot\r\n");
                    return;
                }

                if (param.ToLower() == "reboot" && am3200.IsOnline)
                {
                    var ip = am3200.ConnectedIpList[0].DeviceIpAddress;
                    cmd.AmReboot(ip, Constants.UserName, Constants.Password);
                }
                else if (!am3200.IsOnline)
                {
                    CrestronConsole.PrintLine("AM-3200 is Offline.  Cannot reboot");
                }
                else
                {
                    CrestronConsole.PrintLine("Error.\r\nValid usage is 'AM' <cmd>\r\nValid <cmd> values: reboot\r\n");
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error in AMControls: {0}", e.Message);
                ErrorLog.Error("Error in AMControls: {0}", e.Message);
                ErrorEmail.SendEmail(roomSetup.MailSubject, e.Message);
            }
        }

        public void TPControls(string param)
        {
            try
            {
                if (string.IsNullOrEmpty(param) || param == "?")
                {
                    CrestronConsole.PrintLine("Valid usage is TPControls <cmd>\r\nValid <cmd> values: reboot, setup or exitsetup\r\n");
                    return;
                }

                if (param.ToLower() == "reboot"  && tp.IsOnline)
                {
                    var ip = tp.ConnectedIpList[0].DeviceIpAddress;
                    cmd.TpReboot(ip, Constants.UserName, Constants.Password);
                }
                else if ((param.ToLower() == "setup" || (param.ToLower() == "exit")&& tp.IsOnline))
                {
                    var ip = tp.ConnectedIpList[0].DeviceIpAddress;
                    cmd.TpSetup(ip, Constants.UserName, Constants.Password, param);
                }
                else if (!tp.IsOnline)
                {
                    CrestronConsole.PrintLine("Touchpanel is Offline.  Cannot reboot");
                }
                else
                {
                    CrestronConsole.PrintLine("Error.\r\nValid usage is 'TPControls' <cmd>\r\nValid <cmd> values: reboot\r\n");
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error in TPControls: {0}", e.Message);
                ErrorLog.Error("Error in TPControls: {0}", e.Message);
                ErrorEmail.SendEmail(roomSetup.MailSubject, e.Message);
            }
        }
       public void DeviceStatus(string param)
       {
            cmd.CheckDeviceStatus(param, roomSetup);            
       }

        private void AlertTest(string param)
        {
            try
            {
                if (string.IsNullOrEmpty(param))
                {
                    CrestronConsole.PrintLine("Error.  Valid usage is 'AlertTest' <name> <time>");
                    return;
                }

                if (!param.Contains(","))
                {
                    CrestronConsole.PrintLine("Error.  Valid usage is 'AlertTest' <name> <time>");
                    return;
                }

                var data = param.Split(',');

                if (data.Length != 2)
                {
                    CrestronConsole.PrintLine("Error.  Valid usage is 'AlertTest' <name> <time>");
                    return;
                }

                var name = data[0];
                var time = data[1];
                
                CrestronConsole.PrintLine("Alert Test: {0} {1}", name, time);
                //Scheduling Alert = new Scheduling(roomSetup);
                //Alert.Alert_Timer(name, Convert.ToInt32(time));
               

            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error in AlertTest: {0}", e.Message);
                ErrorLog.Error("Error in AlertTest: {0}", e.Message);
                
            }
        }
        public override void InitializeSystem()
        {
            short adapt = CrestronEthernetHelper.GetAdapterdIdForSpecifiedAdapterType(EthernetAdapterType.EthernetLANAdapter);
                    
            string ip = CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_STATIC_IPADDRESS, adapt);
            string sn = CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_STATIC_IPMASK, adapt);
            string gw = CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_STATIC_ROUTER, adapt);
            string dns = CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_DNS_SERVER, adapt);
            string hostname = CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_HOSTNAME, adapt);
            string dhcp = CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_CURRENT_DHCP_STATE, adapt);
                                    
            int FirstSpace = dns.IndexOf(" ");
            
            string dns1 = null;
            string dns2 = null;

            if (FirstSpace > 0)
            {
                dns1 = dns.Substring(0, FirstSpace);
            }

            int FirstComma = dns.IndexOf(",");
            int SecondSpace = dns.IndexOf(" ", FirstComma);
            if (FirstComma > FirstSpace && SecondSpace > FirstComma )
            {
                dns2 = dns.Substring((FirstComma + 1), (SecondSpace - FirstComma)).Trim();
            }

            Email ErrorEmail = new Email();
            ErrorEmail.EmailSetup();

            bool NetChange = false;
            try
            {   if (dhcp.ToLower() == "on")
                {
                    cmd.NetChange(CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_CURRENT_IP_ADDRESS, adapt),
                       roomSetup.Crestron.Username, roomSetup.Crestron.Password, "dhcp", "off");
                    NetChange = true;
                }
                if (roomSetup.Crestron.ProcessorIp != ip)
                {   
                    cmd.NetChange(CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_CURRENT_IP_ADDRESS, adapt),
                        roomSetup.Crestron.Username, roomSetup.Crestron.Password, "ip", roomSetup.Crestron.ProcessorIp);
                    NetChange = true;
                }
                if (roomSetup.Crestron.HostName != hostname)
                {   
                    cmd.NetChange(CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_CURRENT_IP_ADDRESS, adapt),
                        roomSetup.Crestron.Username, roomSetup.Crestron.Password, "host", roomSetup.Crestron.HostName);
                    NetChange = true;
                }
                if (roomSetup.Crestron.Subnet != sn)
                {   
                    cmd.NetChange(CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_CURRENT_IP_ADDRESS, adapt),
                        roomSetup.Crestron.Username, roomSetup.Crestron.Password, "sn", roomSetup.Crestron.Subnet);
                    NetChange = true;
                }
                if (roomSetup.Crestron.Gateway != gw)
                {   
                    cmd.NetChange(CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_CURRENT_IP_ADDRESS, adapt),
                        roomSetup.Crestron.Username, roomSetup.Crestron.Password, "gw", roomSetup.Crestron.Gateway);
                    NetChange = true;
                }
                if (roomSetup.Crestron.Dns1 != dns1)
                {   
                    cmd.NetChange(CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_CURRENT_IP_ADDRESS, adapt),
                        roomSetup.Crestron.Username, roomSetup.Crestron.Password, "dns1", roomSetup.Crestron.Dns1);
                }
                if (roomSetup.Crestron.Dns2 != dns2)
                {   
                    cmd.NetChange(CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_CURRENT_IP_ADDRESS, adapt),
                        roomSetup.Crestron.Username, roomSetup.Crestron.Password, "dns2", roomSetup.Crestron.Dns2);
                }
                
                if (CrestronEnvironment.GetTimeZone().ID != uint.Parse(roomSetup.Crestron.TimezoneId))
                {
                    CrestronEnvironment.SetTimeZone(int.Parse(roomSetup.Crestron.TimezoneId));
                }
                
                if (SNTP.Server != roomSetup.Crestron.SntpServer)
                {   
                    cmd.SNTPServer(roomSetup.Crestron.SntpServer,ip, roomSetup.Crestron.Username, roomSetup.Crestron.Password);
                }
                
                if (!SNTP.Enabled)
                {
                    cmd.SNTPEnable(roomSetup.Crestron.SntpServer, ip, roomSetup.Crestron.Username, roomSetup.Crestron.Password);
                }

                
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error in InitializeSystem: {0}", e.Message);
                ErrorLog.Error("Error in InitializeSystem: {0}", e.Message);
                var error = "Message: " + e.Message + "\r\nStacktrace: " + e.StackTrace;
                ErrorEmail.SendEmail(roomSetup.MailSubject, error);
            }
            
            if (NetChange)
            {
                cmd.ProcReboot(CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_CURRENT_IP_ADDRESS, adapt), 
                    roomSetup.Crestron.Username, roomSetup.Crestron.Password);
            }
                        
            Scheduling roomscheduler = new Scheduling(roomSetup, tp, am3200, disp1);
            roomscheduler.SystemEventGroup = new ScheduledEventGroup("NFAH");
            roomscheduler.SystemEventGroup.ClearAllEvents();
            roomscheduler.AddDailyTimerEvent();

            try
            {
                // Determine what type of room it is:
                // 1. Huddle Room -> call huddle room setup method
                // 2. Evertz Room -> call evertz room setup method
                // 3. NVX Room -> call NVX setup method

                //if (roomSetup.RoomType == "huddle_room")
                //{
                    //if (roomSetup.HuddleRoomSettings.Autoroute.ToLower() == "on")
                    //{

                    //    hdmd.AutoRouteOn();
                    //                    }
                    //else
                    //{   CrestronConsole.PrintLine("AutoRoute Off");
                    //    hdmd.AutoRouteOff();
                    //}   

                    //if (roomSetup.HuddleRoomSettings.FrontpanelLock.ToLower() == "on")
                    //{
                    //    CrestronConsole.PrintLine("Front Panel Lock On");
                    //    hdmd.EnableFrontPanelLock();
                    //}
                    //else
                    //{
                    //    hdmd.DisableFrontPanelLock();
                    //}

                    //if (roomSetup.HuddleRoomSettings.FrontpanelLed.ToLower() == "on")
                    //{
                    //    CrestronConsole.PrintLine("Front Panel LED On");
                    //    hdmd.EnableFrontPanelLed();
                    //}
                    //else
                    //{   CrestronConsole.PrintLine("Front Panel LED Off");
                    //    hdmd.DisableFrontPanelLed();

                    //}
                //}
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error in InitializeSystem: {0}", e.Message);
                ErrorLog.Error("Error in InitializeSystem: {0}", e.Message);
                ErrorEmail.SendEmail(roomSetup.MailSubject, e.Message);
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