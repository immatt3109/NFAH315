using System;
using Crestron.SimplSharp;                          	// For Basic SIMPL# Classes
using Crestron.SimplSharpPro;                       	// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.CrestronThread;        	// For Threading
using Crestron.SimplSharpPro.Diagnostics;		    	// For System Monitor Access
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro.UI;
using Crestron.SimplSharpPro.DM.AirMedia;
using Crestron.SimplSharpPro.DM;
using Crestron.SimplSharpPro.CrestronConnected;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace NFAHRooms
{
    

    public class HuddleHandler
    {
        private Ts1070 tp;
        private HdMd4x14kzE hdmd;
        private Am300 am3200;
        private CrestronConnectedDisplayV2 disp1;
        private Email _email; // ErrorEmail;
        private HDMD hdmdClass;
        private RoomSetup roomSetup;
        private Scheduling err_counts;
        

        
        public HuddleHandler(Ts1070 tp, HdMd4x14kzE hdmd, Am300 am3200, CrestronConnectedDisplayV2 disp1, RoomSetup roomSetup)
        {
            _email = new Email();
            hdmdClass = new HDMD(hdmd);
            _email.EmailSetup();

            this.tp = tp;
            this.hdmd = hdmd;
            this.am3200 = am3200;
            this.disp1 = disp1;

            this.tp.SigChange += new SigEventHandler(tp_SigChange);
            this.tp.OnlineStatusChange += new OnlineStatusChangeEventHandler(tp_OnlineStatusChange);
            this.hdmd.OnlineStatusChange += new OnlineStatusChangeEventHandler(hdmd_OnlineStatusChange);
            this.hdmd.DMSystemChange += new DMSystemEventHandler(hdmd_DMSystemChange);
            this.disp1.OnlineStatusChange += new OnlineStatusChangeEventHandler(disp1_OnlineStatusChange);
            this.am3200.OnlineStatusChange += new OnlineStatusChangeEventHandler(am3200_OnlineStatusChange);
            this.disp1.BaseEvent += new BaseEventHandler(disp1_BaseEvent);

            this.roomSetup = roomSetup;

            
            
        }

        private void disp1_BaseEvent(GenericBase currentDevice, BaseEventArgs args)
        {
            ///
            ///btnPwrOff = 23,  //If power is on and you want to turn it off, it's this button
            ///btnPwrOn = 33,  //If power is off and you want to turn it on, it's this button
            ///btnPwrOnVis = 33 //If power is off this button should be visible
            ///
            
            if (disp1.Power.PowerOnFeedback.BoolValue)  //Power On
            {
                tp.BooleanInput[((uint)Join.btnPwrOnVis)].BoolValue = false;
                hdmdClass.RouteVideo(((uint)roomSetup.HuddleRoomSettings.DefaultVideoOutput));
                if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                    disp1.Video.Source.SourceSelect.UShortValue = 1;
                if (hdmd.FrontPanelLockEnabledFeedback.BoolValue && roomSetup.HuddleRoomSettings.FrontpanelLock == "off")
                    hdmd.DisableFrontPanelLock();
            }
            
            if (disp1.Power.PowerOffFeedback.BoolValue ) //Power Off
            {
                tp.BooleanInput[((uint)Join.btnPCOnVis)].BoolValue = false;
                tp.BooleanInput[((uint)Join.btnAirMediaOnVis)].BoolValue = false;
                tp.BooleanInput[((uint)Join.btnAuxOnVis)].BoolValue = false;
                tp.BooleanInput[((uint)Join.btnPwrOnVis)].BoolValue = true;
                hdmdClass.RouteVideo(0);
                if (hdmd.FrontPanelLockDisabledFeedback.BoolValue)
                    hdmd.EnableFrontPanelLock();
            }
        }
        private void tp_SigChange(BasicTriList currentDevice, SigEventArgs args)
        {
            try
            {
                if (currentDevice == tp)
                {
                    switch (args.Sig.Type)
                    {
                        case eSigType.NA:
                            break;
                        case eSigType.Bool:
                            {
                                if (args.Sig.BoolValue)
                                {
                                    switch (args.Sig.Number)
                                    {
                                        case 20:
                                            {   
                                                if (disp1.Power.PowerOnFeedback.BoolValue)
                                                {
                                                    if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                        disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    hdmdClass.RouteVideo(2);
                                                }   
                                                break;
                                            }
                                        case 21:
                                            {
                                                if (disp1.Power.PowerOnFeedback.BoolValue)
                                                {
                                                    if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                        disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    hdmdClass.RouteVideo(3);

                                                }
                                                break;
                                            }
                                        case 22:
                                            {
                                                if (disp1.Power.PowerOnFeedback.BoolValue)
                                                {
                                                    if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                        disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    hdmdClass.RouteVideo(1);
                                                }
                                                break;
                                            }
                                        case 33:  //Power On
                                            {   
                                                if (disp1.Power.PowerOffFeedback.BoolValue)
                                                {
                                                    disp1.Power.PowerOn();
                                                    if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                        disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                }
                                                break;
                                            }
                                        case 30:
                                            {
                                                break;
                                            }
                                        case 31:
                                            {
                                                break;
                                            }
                                        case 32:
                                            {
                                                break;
                                            }
                                        case 23:  //Power Off
                                            {
                                                if (disp1.Power.PowerOnFeedback.BoolValue)
                                                {
                                                    disp1.Power.PowerOff();
                                                }
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                }
                                break;
                            }
                        case eSigType.UShort:
                            break;
                        case eSigType.String:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLog.Notice($"HuddleHandler Error:  {e.Message}");
                CrestronConsole.PrintLine($"HuddleHandler Error:  { e.Message}");
            }
        }

        
        private void tp_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            try { 
            if (args.DeviceOnLine)
            {
                CrestronConsole.PrintLine("Online Status Change: {0} IPID: {1} Status: {2}", currentDevice.Name, currentDevice.ID, args.DeviceOnLine);
                ErrorLog.Notice("Online Status Change: {0} IPID: {1} Status: {2}", currentDevice.Name, currentDevice.ID, args.DeviceOnLine);
                   
                if (err_counts.errorCounts["touchpanel"] > 0)
                {
                        CrestronConsole.PrintLine("Touchpanel is online");
                        err_counts.errorCounts["touchpanel"] = 0;
                        _email.SendEmail(roomSetup.MailSubject, $"{currentDevice.Name} is online");
                        CrestronConsole.PrintLine("Touchpanel Online Email Sent");
                }
                tp.ExtenderSystemReservedSigs.StandbyTimeout.UShortValue = roomSetup.Touchpanel.StandbyTimeout;

                if (roomSetup.Touchpanel.ScreenSaver.ToLower() == "on" && roomSetup.Touchpanel.StandbyTimeout != 0 && Scheduling.SS_Active)
                        
                {
                    tp.ExtenderScreenSaverReservedSigs.ScreenSaverImageUrl.StringValue = roomSetup.Touchpanel.ImageUrl;

                    tp.ExtenderScreenSaverReservedSigs.ScreensaverOff.BoolValue = false;
                    tp.ExtenderScreenSaverReservedSigs.ScreensaverOn.BoolValue = true;
                    
                }
                else if ((roomSetup.Touchpanel.ScreenSaver.ToLower() == "off" && !Scheduling.SS_Active) || roomSetup.Touchpanel.StandbyTimeout == 0)
                {
                    tp.ExtenderScreenSaverReservedSigs.ScreensaverOn.BoolValue = false;
                    tp.ExtenderScreenSaverReservedSigs.ScreensaverOff.BoolValue = true;
                }
                
            }
            else
            {
                CrestronConsole.PrintLine("Online Status Change: {0} IPID: {1} Status: {2}", currentDevice.Name, currentDevice.ID, args.DeviceOnLine);
                ErrorLog.Notice("Online Status Change: {0} IPID: {1} Status: {2}", currentDevice.Name, currentDevice.ID, args.DeviceOnLine);
                    //ErrorEmail.SendEmail("Device Offline", $"{currentDevice.Name} is offline");
                    //schedule.Alert_Timer("TP_Offline", roomSetup.Timeouts.ErrorCheckDelay);
                    throw new Exception("Touchpanel Offline");

            }
            }
            catch (Exception e)
            {   CrestronConsole.PrintLine("caught excemption tp_OnlineStatusChange: {0}", e.Message);
                Scheduling schedule = new Scheduling(roomSetup, tp, am3200, disp1);
                schedule.Alert_Timer("touchpanel", roomSetup.Timeouts.ErrorCheckDelay, e );
            }
            
           
        }
        private void hdmd_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            if (args.DeviceOnLine)
            {
                if (roomSetup.HuddleRoomSettings.Autoroute.ToLower() == "on")
                {
                   hdmd.AutoRouteOn();
                }
                else
                {
                    hdmd.AutoRouteOff();
                }

                if (roomSetup.HuddleRoomSettings.FrontpanelLock.ToLower() == "on")
                {
                    hdmd.EnableFrontPanelLock();
                }
                else
                {
                    hdmd.DisableFrontPanelLock();
                }

                if (roomSetup.HuddleRoomSettings.FrontpanelLed.ToLower() == "on")
                {
                    hdmd.EnableFrontPanelLed();
                }
                else
                {
                    hdmd.DisableFrontPanelLed();

                }
            }
            else 
            {
                CrestronConsole.PrintLine("Online Status Change: {0} IPID: {1} Status: {2}", currentDevice.Name, currentDevice.ID, args.DeviceOnLine);
                ErrorLog.Notice("Online Status Change: {0} IPID: {1} Status: {2}", currentDevice.Name, currentDevice.ID, args.DeviceOnLine);
            }
        }
        private void hdmd_DMSystemChange(GenericBase currentDevice, DMSystemEventArgs args)
        {
            if (hdmd.HdmiOutputs[1].VideoOutFeedback != null && disp1.Power.PowerOnFeedback.BoolValue)
            {
                try
                {   switch (hdmd.HdmiOutputs[1].VideoOutFeedback.Number)
                    {
                        case 1:
                            {
                                tp.BooleanInput[((uint)Join.btnPCOnVis)].BoolValue = false;
                                tp.BooleanInput[((uint)Join.btnAirMediaOnVis)].BoolValue = false;
                                tp.BooleanInput[((uint)Join.btnAuxOnVis)].BoolValue = true;
                                break;
                            }
                        case 2:
                            {
                                tp.BooleanInput[((uint)Join.btnPCOnVis)].BoolValue = true;
                                tp.BooleanInput[((uint)Join.btnAirMediaOnVis)].BoolValue = false;
                                tp.BooleanInput[((uint)Join.btnAuxOnVis)].BoolValue = false;
                                break;
                            }
                        case 3:
                            {
                                tp.BooleanInput[((uint)Join.btnPCOnVis)].BoolValue = false;
                                tp.BooleanInput[((uint)Join.btnAirMediaOnVis)].BoolValue = true;
                                tp.BooleanInput[((uint)Join.btnAuxOnVis)].BoolValue = false;
                                break;
                            }
                        case 0:
                            {
                                tp.BooleanInput[((uint)Join.btnPCOnVis)].BoolValue = false;
                                tp.BooleanInput[((uint)Join.btnAirMediaOnVis)].BoolValue = false;
                                tp.BooleanInput[((uint)Join.btnAuxOnVis)].BoolValue = false;
                                break;
                            }
                            
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    ErrorLog.Exception("HuddleHandler Error",e);
                    CrestronConsole.PrintLine($"HuddleHandler Error:  {e.Message}");
                }
            }
            else
                return;
        }

        private void disp1_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            CrestronConsole.PrintLine("Online Status Change: {0} IPID: {1} Status: {2}", currentDevice.Name, currentDevice.ID, args.DeviceOnLine);
            ErrorLog.Notice("Online Status Change: {0} IPID: {1} Status: {2}", currentDevice.Name, currentDevice.ID, args.DeviceOnLine);
        }

        private void am3200_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            CrestronConsole.PrintLine("Online Status Change: {0} IPID: {1} Status: {2}", currentDevice.Name, currentDevice.ID, args.DeviceOnLine);
            ErrorLog.Notice("Online Status Change: {0} IPID: {1} Status: {2}", currentDevice.Name, currentDevice.ID, args.DeviceOnLine);
        }

        public void Initialize()
        {   
            try
            {
                tp.ExtenderSystemReservedSigs.Use();
                tp.ExtenderSystemReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;
                tp.ExtenderScreenSaverReservedSigs.Use();
                tp.ExtenderScreenSaverReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;
                tp.ExtenderSystem3ReservedSigs.Use();
                tp.ExtenderSystem3ReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;


                if (tp.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(tp.RegistrationFailureReason.ToString());

                if (hdmd.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(hdmd.RegistrationFailureReason.ToString());


                if (disp1.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(disp1.RegistrationFailureReason.ToString());

                if (am3200.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(am3200.RegistrationFailureReason.ToString());

                tp.StringInput[((uint)Join.lblRoomName)].StringValue = roomSetup.Touchpanel.RoomText;

                
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error initializing HuddleHandler: {0}", e.Message);
                ErrorLog.Error("Error initializing HuddleHandler: {0}", e.Message);
                _email.SendEmail("Error initializing HuddleHandler", e.Message);
            }
        }

        private void tp_EXTSSSigChange(DeviceExtender currentDeviceExtender, SigEventArgs args)
        {
            CrestronConsole.PrintLine("Sig Change: {0} Value: {1}  Type: {2}", args.Sig.Number, args.Sig.StringValue, args.Sig.Type);
            CrestronConsole.PrintLine($"Prox_Active: {Scheduling.Prox_Active}");
            
            try
            {
                if (args.Sig.Type == eSigType.Bool)
                {
                    if (args.Sig.Number == 18358 && Scheduling.Prox_Active == true)
                    {
                        tp.SleepWakeManager.Wake();
                    }
                    return;
                }
                else if (args.Sig.Type == eSigType.UShort)
                {
                    return;
                }
                else if (args.Sig.Type == eSigType.String)
                {
                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error in tp_EXTSSSigChange: {0}", e.Message);
                ErrorLog.Error("Error in tp_EXTSSSigChange: {0}", e.Message);
                _email.SendEmail("Error in tp_EXTSSSigChange", e.Message);
            }
        }
    }
            
            
}
