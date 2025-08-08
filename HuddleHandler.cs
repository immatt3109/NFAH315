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
    

    public static class HuddleHandler
    {
                        
        private static void disp1_BaseEvent(GenericBase currentDevice, BaseEventArgs args)
        {
            ///
            ///btnPwrOff = 23,  //If power is on and you want to turn it off, it's this button
            ///btnPwrOn = 33,  //If power is off and you want to turn it on, it's this button
            ///btnPwrOnVis = 33 //If power is off this button should be visible
            ///

            try
            {
                if (ControlSystem.disp1.Power.PowerOnFeedback.BoolValue && !ControlSystem.disp1.Power.PowerOffFeedback.BoolValue)  //Power On
                {
                    ControlSystem.tp.BooleanInput[((uint)Join.btnPwrOnVis)].BoolValue = false;
                    
                    if (ControlSystem.disp1.Video.Source.SourceSelect.UShortValue != 1)
                        ControlSystem.disp1.Video.Source.SourceSelect.UShortValue = 1;
                    if (ControlSystem.hdmd.FrontPanelLockEnabledFeedback.BoolValue && RoomSetup.HuddleRoomSettings.FrontpanelLock == "off")
                        ControlSystem.hdmd.DisableFrontPanelLock();
                    
                }

                if (ControlSystem.disp1.Power.PowerOffFeedback.BoolValue && !ControlSystem.disp1.Power.PowerOnFeedback.BoolValue) //Power Off
                {
                    ControlSystem.tp.BooleanInput[((uint)Join.btnPwrOnVis)].BoolValue = true;
                    HDMD.RouteVideo(0); 

                    if (ControlSystem.hdmd.FrontPanelLockDisabledFeedback.BoolValue)
                        ControlSystem.hdmd.EnableFrontPanelLock();
                }
            }
            catch (Exception e)
            {
                ErrorLog.Notice($"HuddleHandler Error:  {e.Message}");
                CrestronConsole.PrintLine($"HuddleHandler Error:  {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject + " Disp1_BaseEvent", e.Message);
            }
        }
        private static void tp_SigChange(BasicTriList currentDevice, SigEventArgs args)
        {
            try
            {
                if (currentDevice == ControlSystem.tp)
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
                                                if (ControlSystem.disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                        ControlSystem.disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                
                                                if (ControlSystem.disp1.Power.PowerOffFeedback.BoolValue)
                                                    ControlSystem.disp1.Power.PowerOn();

                                                ControlSystem.hdmd.HdmiOutputs[1].HdmiOutputPort.DisableAutomaticPowerSettings();

                                                break;
                                            }
                                        case 21:
                                            {
                                                HDMD.RouteVideo(3);
                                                if (ControlSystem.disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    ControlSystem.disp1.Video.Source.SourceSelect.UShortValue = 1;

                                                if (ControlSystem.disp1.Power.PowerOffFeedback.BoolValue)
                                                    ControlSystem.disp1.Power.PowerOn();

                                                ControlSystem.hdmd.HdmiOutputs[1].HdmiOutputPort.DisableAutomaticPowerSettings();
                                                break;
                                            }
                                        case 22:
                                            {
                                                HDMD.RouteVideo(1);
                                                if (ControlSystem.disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    ControlSystem.disp1.Video.Source.SourceSelect.UShortValue = 1;

                                                if (ControlSystem.disp1.Power.PowerOffFeedback.BoolValue)
                                                    ControlSystem.disp1.Power.PowerOn();

                                                ControlSystem.hdmd.HdmiOutputs[1].HdmiOutputPort.DisableAutomaticPowerSettings();

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
                                                ControlSystem.disp1.Power.PowerOff();
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
        private static void tp_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
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

                    ControlSystem.tp.ExtenderSystemReservedSigs.StandbyTimeout.UShortValue = RoomSetup.Touchpanel.StandbyTimeout;

                    if (RoomSetup.Touchpanel.ScreenSaver.ToLower() == "on" && RoomSetup.Touchpanel.StandbyTimeout != 0 && Scheduling.SS_Active)
                    {
                        ControlSystem.tp.ExtenderScreenSaverReservedSigs.ScreenSaverImageUrl.StringValue = RoomSetup.Touchpanel.ImageUrl;

                        ControlSystem.tp.ExtenderScreenSaverReservedSigs.ScreensaverOff.BoolValue = false;
                        ControlSystem.tp.ExtenderScreenSaverReservedSigs.ScreensaverOn.BoolValue = true;
                    }

                    else if ((RoomSetup.Touchpanel.ScreenSaver.ToLower() == "off" && !Scheduling.SS_Active) || RoomSetup.Touchpanel.StandbyTimeout == 0)
                    {
                        ControlSystem.tp.ExtenderScreenSaverReservedSigs.ScreensaverOn.BoolValue = false;
                        ControlSystem.tp.ExtenderScreenSaverReservedSigs.ScreensaverOff.BoolValue = true;
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
        private static void hdmd_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            try
            {
                if (args.DeviceOnLine)
                {
                    if (RoomSetup.HuddleRoomSettings.Autoroute.ToLower() == "on")
                    {
                        ControlSystem.hdmd.AutoRouteOn();
                    }
                    else
                    {
                        ControlSystem.hdmd.AutoRouteOff();
                    }

                    if (RoomSetup.HuddleRoomSettings.FrontpanelLock.ToLower() == "on")
                    {
                        ControlSystem.hdmd.EnableFrontPanelLock();
                    }
                    else
                    {
                        ControlSystem.hdmd.DisableFrontPanelLock();
                    }

                    if (RoomSetup.HuddleRoomSettings.FrontpanelLed.ToLower() == "on")
                    {
                        ControlSystem.hdmd.EnableFrontPanelLed();
                    }
                    else
                    {
                        ControlSystem.hdmd.DisableFrontPanelLed();
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
        private static void hdmd_DMSystemChange(GenericBase currentDevice, DMSystemEventArgs args)
        { 
            if (ControlSystem.hdmd.HdmiOutputs[1].VideoOutFeedback != null)
            {
                
                try
                {   switch (ControlSystem.hdmd.HdmiOutputs[1].VideoOutFeedback.Number)
                    {
                        case 1:
                            {
                                ControlSystem.tp.BooleanInput[((uint)Join.btnPCOnVis)].BoolValue = false;
                                ControlSystem.tp.BooleanInput[((uint)Join.btnAirMediaOnVis)].BoolValue = false;
                                ControlSystem.tp.BooleanInput[((uint)Join.btnAuxOnVis)].BoolValue = true;
                                break;
                            }
                        case 2:
                            {
                                ControlSystem.tp.BooleanInput[((uint)Join.btnPCOnVis)].BoolValue = true;
                                ControlSystem.tp.BooleanInput[((uint)Join.btnAirMediaOnVis)].BoolValue = false;
                                ControlSystem.tp.BooleanInput[((uint)Join.btnAuxOnVis)].BoolValue = false;
                                break;
                            }
                        case 3:
                            {
                                ControlSystem.tp.BooleanInput[((uint)Join.btnPCOnVis)].BoolValue = false;
                                ControlSystem.tp.BooleanInput[((uint)Join.btnAirMediaOnVis)].BoolValue = true;
                                ControlSystem.tp.BooleanInput[((uint)Join.btnAuxOnVis)].BoolValue = false;
                                break;
                            }
                        case 0:
                            {
                                ControlSystem.tp.BooleanInput[((uint)Join.btnPCOnVis)].BoolValue = false;
                                ControlSystem.tp.BooleanInput[((uint)Join.btnAirMediaOnVis)].BoolValue = false;
                                ControlSystem.tp.BooleanInput[((uint)Join.btnAuxOnVis)].BoolValue = false;
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
            if (ControlSystem.hdmd.HdmiOutputs[1].VideoOutFeedback == null)
            {
                ControlSystem.tp.BooleanInput[((uint)Join.btnPCOnVis)].BoolValue = false;
                ControlSystem.tp.BooleanInput[((uint)Join.btnAirMediaOnVis)].BoolValue = false;
                ControlSystem.tp.BooleanInput[((uint)Join.btnAuxOnVis)].BoolValue = false;
            }
        }

        private static void disp1_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
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

        private static void am3200_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
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
        public static void Initialize()
        {   
            try
            {
                ControlSystem.tp.ExtenderSystemReservedSigs.Use();
                ControlSystem.tp.ExtenderSystemReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;
                ControlSystem.tp.ExtenderScreenSaverReservedSigs.Use();
                ControlSystem.tp.ExtenderScreenSaverReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;
                ControlSystem.tp.ExtenderSystem3ReservedSigs.Use();
                ControlSystem.tp.ExtenderSystem3ReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;


                if (ControlSystem.tp.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(ControlSystem.tp.RegistrationFailureReason.ToString());

                if (ControlSystem.hdmd.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(ControlSystem.hdmd.RegistrationFailureReason.ToString());


                if (ControlSystem.disp1.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(ControlSystem.disp1.RegistrationFailureReason.ToString());

                if (ControlSystem.am3200.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(ControlSystem.am3200.RegistrationFailureReason.ToString());

                ControlSystem.tp.SigChange += new SigEventHandler(tp_SigChange);
                ControlSystem.tp.OnlineStatusChange += new OnlineStatusChangeEventHandler(tp_OnlineStatusChange);
                ControlSystem.hdmd.OnlineStatusChange += new OnlineStatusChangeEventHandler(hdmd_OnlineStatusChange);
                ControlSystem.hdmd.DMSystemChange += new DMSystemEventHandler(hdmd_DMSystemChange);
                ControlSystem.disp1.OnlineStatusChange += new OnlineStatusChangeEventHandler(disp1_OnlineStatusChange);
                ControlSystem.am3200.OnlineStatusChange += new OnlineStatusChangeEventHandler(am3200_OnlineStatusChange);
                ControlSystem.disp1.BaseEvent += new BaseEventHandler(disp1_BaseEvent);

                ControlSystem.tp.StringInput[((uint)Join.lblRoomName)].StringValue = RoomSetup.Touchpanel.RoomText;
                ControlSystem.hdmd.HdmiOutputs[1].HdmiOutputPort.DisableAutomaticPowerSettings();
                ControlSystem.am3200.HdmiOut.Resolution = CommonStreamingSupport.eScreenResolutions.Resolution1080p60Hz;
                ControlSystem.tp.ExtenderSystemReservedSigs.LcdBrightnessAutoOff();
                

            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error initializing HuddleHandler: {0}", e.Message);
                ErrorLog.Error("Error initializing HuddleHandler: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " Error initializing HuddleHandler", e.Message);
            }
        }

        private static void tp_EXTSSSigChange(DeviceExtender currentDeviceExtender, SigEventArgs args)
        {
            try
            {
                if (args.Sig.Type == eSigType.Bool)
                {
                    if (args.Sig.Number == 18358 && Scheduling.Prox_Active == true)
                    {
                        ControlSystem.tp.SleepWakeManager.Wake();
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
