using Crestron.SimplSharp;
using System;
using System.Collections.Generic;
using Crestron.SimplSharpPro.UI;
using Crestron.SimplSharpPro.DM.AirMedia;
using Crestron.SimplSharpPro.DM;
using Crestron.SimplSharpPro.CrestronConnected;
using Crestron.SimplSharp.Ssh;


namespace NFAHRooms
{ 
    internal class CmdLine
    {
        private Dictionary<string, IDevice> devices = new Dictionary<string, IDevice>();
        //private Email errorEmail;
        private RoomSetup roomSetup;
        private CrestronConnectedDisplayV2 cmddisp1;
        private HdMd4x14kzE hdmd;
            

        public CmdLine(RoomSetup roomSetup, CrestronConnectedDisplayV2 cmddisp1, HdMd4x14kzE hdmd)
        {
            this.roomSetup = roomSetup;
            //this.errorEmail = errorEmail;
            this.cmddisp1 = cmddisp1;
            this.hdmd = hdmd;
        }
        
        public void CheckDeviceStatus(String device, RoomSetup roomSetup)
        {
            var devicetype = GetDevice(device);
            
            try
            {                   
                if (roomSetup.RoomType == "huddle_room")
                {
                    if (devices.ContainsKey(device))
                    {
                        if (devicetype.IsOnline)
                        {
                            CrestronConsole.PrintLine($"{device} is Online");
                        }
                        else
                        {
                            CrestronConsole.PrintLine($"{device} is Offline");
                        }
                    }
                    else
                    {   
                        int i = 0;
                        CrestronConsole.PrintLine("Error.  Valid usage is 'status' <device> Valid devices are: ");
                        foreach(var dev in devices)
                        {
                            if(i > 0)
                                CrestronConsole.Print($", {dev.Key}");
                            else
                                CrestronConsole.Print($"{dev.Key}");
                            i++;
                        }
                    }
                }
            }
            catch (Exception e)
            {   
                ErrorLog.Error($"Error checking device status: {e.Message}");
                CrestronConsole.PrintLine($"Error checking device status: {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject, e.Message);
            }
        }
        public void HDMDControls(string cmd, string value, string ip, string username, string pwd)
        {
            try
            {
                if (hdmd.IsOnline)
                {
                    if (cmd.ToLower() == "led")
                    {
                        if (value.ToLower() == "on")
                        {
                            hdmd.EnableFrontPanelLed();
                        }
                        else if (value.ToLower() == "off")
                        {
                            hdmd.DisableFrontPanelLed();
                        }
                        else
                        {
                            CrestronConsole.PrintLine("Invalid value.  Valid values are 'on' or 'off'");
                        }
                    }
                    if (cmd.ToLower() == "lock")
                    {
                        if (value.ToLower() == "on")
                        {
                            hdmd.EnableFrontPanelLock();
                        }
                        else if (value.ToLower() == "off")
                        {
                            hdmd.DisableFrontPanelLock();
                        }
                        else
                        {
                            CrestronConsole.PrintLine("Invalid value.  Valid values are 'on' or 'off'");
                        }
                    }
                    if (cmd.ToLower() == "route")
                    {
                        ushort input = Convert.ToUInt16(value);
                        if (input >= 0 && input <= 4)
                            hdmd.HdmiOutputs[1].VideoOut = hdmd.HdmiInputs[input];
                        else
                            CrestronConsole.PrintLine("Invalid input.  Valid inputs are 1, 2, 3 4, 0");
                    }
                    if (cmd.ToLower() == "reboot")
                    {
                        SshClient ssh = new SshClient(ip, username, pwd);
                        ssh.Connect();
                        ssh.RunCommand("reboot");
                        ssh.Disconnect();
                    }
                    else
                    {
                        CrestronConsole.PrintLine("Error rebooting HDMD");
                    }
                }
                else
                {
                    CrestronConsole.PrintLine("Display is offline");
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error($"Error with HDMD controls: {e.Message}");
                CrestronConsole.PrintLine($"Error with HDMD controls: {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject, e.Message);
            }
        }
        public void TvControls(string cmd, string value)
        {
            try
            {
            if (cmddisp1.IsOnline)
            { 
                if (cmd.ToLower() == "pwr")
                {
                    if (value.ToLower() == "on")
                    {
                        cmddisp1.Power.PowerOn();
                    }
                    else if (value.ToLower() == "off")
                    {
                        cmddisp1.Power.PowerOff();
                    }
                    else
                    {
                        CrestronConsole.PrintLine("Invalid value.  Valid values are 'on' or 'off'");
                    }
                }
                else if (cmd.ToLower() == "hdmi")
                {
                    ushort input = Convert.ToUInt16(value);
                    if (input > 0 && input <= 4)
                        cmddisp1.Video.Source.SourceSelect.UShortValue = input;
                    else
                        CrestronConsole.PrintLine("Invalid input.  Valid inputs are 1, 2, 3 or 4");
                }
                else
                {
                    CrestronConsole.PrintLine("Invalid command.  Valid commands are 'pwr' or 'hdmi'");
                }
            }
            else
            {
                CrestronConsole.PrintLine("Display is offline");
            }
            }
            catch (Exception e)
            {
                ErrorLog.Error($"Error with TV controls: {e.Message}");
                CrestronConsole.PrintLine($"Error with TV controls: {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject, e.Message);
            }
        }
        public void SNTPServer (string server, string ip, string username, string pwd)
        {
            try
            {
                SshClient ssh = new SshClient(ip, "admin", "password");
                ssh.Connect();
                var command1 = ssh.CreateCommand($"sntp server:{server}");
                var command2 = ssh.CreateCommand("sntp start");
                var response1 = command1.Execute();
                
                CrestronConsole.PrintLine($"response1: {response1}");   
                
                var response2 = command2.Execute();

                CrestronConsole.PrintLine($"response2: {response2}");   
                
                ssh.Disconnect();
                
            }
            catch (Exception e)
            {
                ErrorLog.Error($"Error resetting SNTP: {e.Message}");
                CrestronConsole.PrintLine($"Error resetting SNTP: {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject, e.Message);
            }
        }
        public void SNTPEnable(string server, string ip, string username, string pwd)
        {
            try
            {
                SshClient ssh = new SshClient(ip, "admin", "password"); 
                ssh.Connect();

                var command1 = ssh.CreateCommand($"sntp start");
                var response1 = command1.Execute();

                if (!response1.Equals("NTP Process has been started."))
                {
                    throw new Exception("SNTP Service cannot be started correctly.");
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error($"Error starting SNTP service: {e.Message}");
                CrestronConsole.PrintLine($"Error starting SNTP Service: {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject, e.Message);
            }
        }
        public void NetChange(string procip, string username, string pwd, string command, string data)
        {
            try
            {
                SshClient ssh = new SshClient(procip, username, pwd);

                switch (command)
                {
                    case "ip":
                        ssh.Connect();
                        ssh.RunCommand($"ipa 0 {data} /now");
                        ssh.Disconnect();
                        break;
                    case "sn":
                        ssh.Connect();
                        ssh.RunCommand($"ipm 0 {data} /now");
                        ssh.Disconnect();
                        break;
                    case "gw":
                        ssh.Connect();
                        ssh.RunCommand($"defr 0 {data} /now");
                        ssh.Disconnect();
                        break;
                    case "dns1": case "dns2":
                        ssh.Connect();
                        ssh.RunCommand($"addd {data}");
                        ssh.Disconnect();
                        break;
                    case "host":
                        ssh.Connect();
                        ssh.RunCommand($"host {data} /now");
                        ssh.Disconnect();
                        break;
                    case "dhcp":
                        ssh.Connect();
                        ssh.RunCommand("dhcp 0 off /now");
                        ssh.Disconnect();
                        break;
                    default:
                        CrestronConsole.PrintLine("Network changes failed.");
                        break;
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error($"Error changing network settings: {e.Message}");
                CrestronConsole.PrintLine($"Error changing network settings: {e.Message}");
                string newbody = "Message: " + e.Message + "\n" + "Stack Trace: " + e.StackTrace + "\n" + "Source: " + e.Source;
                Email.SendEmail(RoomSetup.MailSubject, newbody);
            }
        }
        public void ProcReboot(string ip, string username, string pwd)
        {
            try
            {
                SshClient ssh = new SshClient(ip, username, pwd);
                ssh.Connect();
                ssh.RunCommand("reboot");
                ssh.Disconnect();
            }
            catch (Exception e)
            {
                ErrorLog.Error($"Error rebooting Processor: {e.Message}");
                CrestronConsole.PrintLine($"Error rebooting Processor: {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject, e.Message);
            }
        }
        public void AmReboot(string ip, string username, string pwd)
        {
            try
            {
                SshClient ssh = new SshClient(ip, username, pwd);
                ssh.Connect();
                ssh.RunCommand("reboot");
                ssh.Disconnect();
            }
            catch (Exception e)
            {
                ErrorLog.Error($"Error rebooting AM3200: {e.Message}");
                CrestronConsole.PrintLine($"Error rebooting AM3200: {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject, e.Message);
            }
        }
        public void TpReboot(string ip, string username, string pwd)
        {
            try
            {
                SshClient ssh = new SshClient(ip, username, pwd);
                ssh.Connect();
                ssh.RunCommand("reboot");
                ssh.Disconnect();
            }
            catch (Exception e)
            {
                ErrorLog.Error($"Error rebooting Touchpanel: {e.Message}");
                CrestronConsole.PrintLine($"Error rebooting Touchpanel: {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject, e.Message);
            }
        }
        public void TpSetup(string ip, string username, string pwd, string command)
        {
            try
            {
                SshClient ssh = new SshClient(ip, username, pwd);

                if (command == "setup")
                {
                    ssh.Connect();
                    ssh.RunCommand("setup");
                    ssh.Disconnect();
                }
                if (command == "exit")
                {
                    ssh.Connect();
                    ssh.RunCommand("setup exit");
                    ssh.Disconnect();
                }
               else
                {
                    CrestronConsole.PrintLine("Invalid command.  Valid commands are 'setup' or 'exit'");
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error($"Error setting up Touchpanel: {e.Message}");
                CrestronConsole.PrintLine($"Error setting up Touchpanel: {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject, e.Message);
            }
        }
        public void AddDevice(string deviceName, IDevice device)
        {
            try
            {
                devices[deviceName] = device;
            }
            catch (Exception e)
            {
                ErrorLog.Error($"Error adding device: {e.Message}");
                CrestronConsole.PrintLine($"Error adding device: {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject, e.Message);
            }   
        }
        public IDevice GetDevice(string deviceName)
        {
            try
            {
                devices.TryGetValue(deviceName, out IDevice device);
                if (deviceName == null || device == null)
                    return null;
                else
                    return devices[deviceName];
            }
            catch (Exception e)
            {
                ErrorLog.Error($"Error getting device: {e.Message}");
                CrestronConsole.PrintLine($"Error getting device: {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject, e.Message);
                return null;
            }
        }
    }
    public interface IDevice
    {
        bool IsOnline { get; }
    }
    public class Ts1070Device : IDevice
    {
        private Ts1070 _ts1070;

        public Ts1070Device(Ts1070 ts1070)
        {
            _ts1070 = ts1070;
        }

        public bool IsOnline
        {
            get { return _ts1070.IsOnline; }
        }
    }
    public class HdMd4x14kzEDevice : IDevice
    {
        private HdMd4x14kzE _hdmd;

        public HdMd4x14kzEDevice(HdMd4x14kzE hdmd)
        {
            _hdmd = hdmd;
        }

        public bool IsOnline
        {
            get { return _hdmd.IsOnline; }
        }
    }
    public class Am300Device : IDevice
    {
        private Am300 _am300;

        public Am300Device(Am300 am300)
        {
            _am300 = am300;
        }

        public bool IsOnline
        {
            get { return _am300.IsOnline; }
        }
    }
    public class CrestronConnectedDisplayV2Device : IDevice
    {
        private CrestronConnectedDisplayV2 _disp1;

        public CrestronConnectedDisplayV2Device(CrestronConnectedDisplayV2 disp1)
        {
            _disp1 = disp1;
        }

        public bool IsOnline
        {
            get { return _disp1.IsOnline; }
        }
    }
}
