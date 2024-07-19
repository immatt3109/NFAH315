using System;
using Crestron.SimplSharp;                          	// For Basic SIMPL# Classes
using Crestron.SimplSharpPro;                       	// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro.UI;
using Crestron.SimplSharpPro.DM.AirMedia;
using Crestron.SimplSharpPro.DM;
using Crestron.SimplSharpPro.CrestronConnected;


namespace NFAHRooms
{
    

    public class HuddleHandler
    {
        private Ts1070 tp;
        private HdMd4x14kzE hdmd;
        private Am300 am3200;
        private CrestronConnectedDisplayV2 disp1;
        //private HDMD hdmdClass;
        //private RoomSetup roomSetup;
        //private Scheduling schedule;
        public HuddleHandler(Ts1070 tp, HdMd4x14kzE hdmd, Am300 am3200, CrestronConnectedDisplayV2 disp1)
        {
            //hdmdClass = new HDMD(hdmd);
            
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

            //this.roomSetup = roomSetup;

            //schedule = new Scheduling();
        }
                
        private void disp1_BaseEvent(GenericBase currentDevice, BaseEventArgs args)
        {
            ///
            ///btnPwrOff = 23,  //If power is on and you want to turn it off, it's this button
            ///btnPwrOn = 33,  //If power is off and you want to turn it on, it's this button
            ///btnPwrOnVis = 33 //If power is off this button should be visible
            ///

            try
            {
                if (disp1.Power.PowerOnFeedback.BoolValue && !disp1.Power.PowerOffFeedback.BoolValue)  //Power On
                {
                    tp.BooleanInput[((uint)Join.btnPwrOnVis)].BoolValue = false;
                    //hdmdClass.RouteVideo(((uint)roomSetup.HuddleRoomSettings.DefaultVideoOutput));

                    if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                        disp1.Video.Source.SourceSelect.UShortValue = 1;
                    if (hdmd.FrontPanelLockEnabledFeedback.BoolValue && RoomSetup.HuddleRoomSettings.FrontpanelLock == "off")
                        hdmd.DisableFrontPanelLock();
                    
                }

                if (disp1.Power.PowerOffFeedback.BoolValue && !disp1.Power.PowerOnFeedback.BoolValue) //Power Off
                {
                    tp.BooleanInput[((uint)Join.btnPwrOnVis)].BoolValue = true;
                    //hdmdClass.RouteVideo(0);
                    HDMD.RouteVideo(0); 

                    if (hdmd.FrontPanelLockDisabledFeedback.BoolValue)
                        hdmd.EnableFrontPanelLock();
                }
            }
            catch (Exception e)
            {
                ErrorLog.Notice($"HuddleHandler Error:  {e.Message}");
                CrestronConsole.PrintLine($"HuddleHandler Error:  {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject + " Disp1_BaseEvent", e.Message);
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
                                                HDMD.RouteVideo(2);
                                                //hdmdClass.RouteVideo(2);
                                                if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                        disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                
                                                if (disp1.Power.PowerOffFeedback.BoolValue)
                                                    disp1.Power.PowerOn();
                                                
                                                break;
                                            }
                                        case 21:
                                            {
                                                HDMD.RouteVideo(3);
                                                //hdmdClass.RouteVideo(3);
                                                if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    disp1.Video.Source.SourceSelect.UShortValue = 1;

                                                if (disp1.Power.PowerOffFeedback.BoolValue)
                                                    disp1.Power.PowerOn();

                                                break;
                                            }
                                        case 22:
                                            {
                                                HDMD.RouteVideo(1);
                                                //hdmdClass.RouteVideo(1);
                                                if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    disp1.Video.Source.SourceSelect.UShortValue = 1;

                                                if (disp1.Power.PowerOffFeedback.BoolValue)
                                                    disp1.Power.PowerOn();

                                                break;
                                            }
                                        case 33:  //Power On
                                            {   
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
                                                disp1.Power.PowerOff();
                                                //hdmdClass.RouteVideo(0);
                                                HDMD.RouteVideo(0);
                                                
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
                Email.SendEmail(RoomSetup.MailSubject + " TP_SigChange", e.Message);
            }
        }
        private void tp_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            try
            {
                if (args.DeviceOnLine)
                {
                    if (Scheduling.errorCounts.TryGetValue("touchpanel", out int x) && x > 0)
                    {
                        Scheduling.errorCounts["touchpanel"] = 0;
                        Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
                    }

                    tp.ExtenderSystemReservedSigs.StandbyTimeout.UShortValue = RoomSetup.Touchpanel.StandbyTimeout;

                    if (RoomSetup.Touchpanel.ScreenSaver.ToLower() == "on" && RoomSetup.Touchpanel.StandbyTimeout != 0 && Scheduling.SS_Active)
                    {
                        tp.ExtenderScreenSaverReservedSigs.ScreenSaverImageUrl.StringValue = RoomSetup.Touchpanel.ImageUrl;

                        tp.ExtenderScreenSaverReservedSigs.ScreensaverOff.BoolValue = false;
                        tp.ExtenderScreenSaverReservedSigs.ScreensaverOn.BoolValue = true;
                    }

                    else if ((RoomSetup.Touchpanel.ScreenSaver.ToLower() == "off" && !Scheduling.SS_Active) || RoomSetup.Touchpanel.StandbyTimeout == 0)
                    {
                        tp.ExtenderScreenSaverReservedSigs.ScreensaverOn.BoolValue = false;
                        tp.ExtenderScreenSaverReservedSigs.ScreensaverOff.BoolValue = true;
                    }
                }
                else if (!args.DeviceOnLine)
                {
                    Scheduling.Alert_Timer("touchpanel", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("TP_OnlineStatusChange: {0}", e.Message);
                ErrorLog.Notice("TP_OnlineStatusChange: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " TP_OnlineStatusChange", e.Message);
            }  
        }
        private void hdmd_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            try
            {
                if (args.DeviceOnLine)
                {
                    if (RoomSetup.HuddleRoomSettings.Autoroute.ToLower() == "on")
                    {
                        hdmd.AutoRouteOn();
                    }
                    else
                    {
                        hdmd.AutoRouteOff();
                    }

                    if (RoomSetup.HuddleRoomSettings.FrontpanelLock.ToLower() == "on")
                    {
                        hdmd.EnableFrontPanelLock();
                    }
                    else
                    {
                        hdmd.DisableFrontPanelLock();
                    }

                    if (RoomSetup.HuddleRoomSettings.FrontpanelLed.ToLower() == "on")
                    {
                        hdmd.EnableFrontPanelLed();
                    }
                    else
                    {
                        hdmd.DisableFrontPanelLed();
                    }

                    if (Scheduling.errorCounts.TryGetValue("hdmd", out int x) && x > 0)
                    {
                        Scheduling.errorCounts["hdmd"] = 0;
                        Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
                    }
                }

                if (args.DeviceOnLine == false)
                {

                    Scheduling.Alert_Timer("hdmd", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("HDMD_OnlineStatusChange: {0}", e.Message);
                ErrorLog.Notice("HDMD_OnlineStatusChange: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " HDMD_OnlineStatusChange", e.Message);
            }
        }
        private void hdmd_DMSystemChange(GenericBase currentDevice, DMSystemEventArgs args)
        { 
            if (hdmd.HdmiOutputs[1].VideoOutFeedback != null)
            {
                CrestronConsole.PrintLine("HdmiOutputs[1].VideoOutFeedback.Number: {0}", hdmd.HdmiOutputs[1].VideoOutFeedback.Number);
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
                    Email.SendEmail(RoomSetup.MailSubject, e.Message);
                }
            }
            if (hdmd.HdmiOutputs[1].VideoOutFeedback == null)
            {
                tp.BooleanInput[((uint)Join.btnPCOnVis)].BoolValue = false;
                tp.BooleanInput[((uint)Join.btnAirMediaOnVis)].BoolValue = false;
                tp.BooleanInput[((uint)Join.btnAuxOnVis)].BoolValue = false;
            }
        }

        private void disp1_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            try
            {
                if (args.DeviceOnLine)
                {
                    if (Scheduling.errorCounts.TryGetValue("tv", out int x) && x > 0)
                    {
                        Scheduling.errorCounts["tv"] = 0;
                        Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
                    }
                }
                else if (!args.DeviceOnLine)
                {
                    Scheduling.Alert_Timer("tv", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Display_OnlineStatusChange: {0}", e.Message);
                ErrorLog.Notice("Display_OnlineStatusChange: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " Display_OnlineStatusChange", e.Message);
            }
        }

        private void am3200_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            try
            {
                if (args.DeviceOnLine)
                {
                    if (Scheduling.errorCounts.TryGetValue("airmedia", out int x) && x > 0)
                    {
                        Scheduling.errorCounts["airmedia"] = 0;
                        Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
                    }
                }
                else if (!args.DeviceOnLine)
                {
                    Scheduling.Alert_Timer("airmedia", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("AM3200_OnlineStatusChange: {0}", e.Message);
                ErrorLog.Notice("AM3200_OnlineStatusChange: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " AM3200_OnlineStatusChange", e.Message);
            }
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

                //tp.StringInput[((uint)Join.lblRoomName)].StringValue = RoomSetup.Touchpanel.RoomText;
                hdmd.HdmiOutputs[1].HdmiOutputPort.DisableAutomaticPowerSettings();
                //am3200.HdmiOut.Resolution = CommonStreamingSupport.eScreenResolutions.Resolution1080p60Hz;
                //tp.ExtenderButtonToolbarReservedSigs.HideButtonToolbar();
                //tp.ExtenderSystemReservedSigs.LcdBrightnessAutoOff();
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error initializing HuddleHandler: {0}", e.Message);
                ErrorLog.Error("Error initializing HuddleHandler: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " Error initializing HuddleHandler", e.Message);
            }
        }

        private void tp_EXTSSSigChange(DeviceExtender currentDeviceExtender, SigEventArgs args)
        {
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
                Email.SendEmail(RoomSetup.MailSubject + " Error in tp_EXTSSSigChange", e.Message);
            }
        }
    }       
}
