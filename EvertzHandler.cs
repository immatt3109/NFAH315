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


    public class EvertzHandler
    {
        private Ts1070 tp;
        private Am300 am3200;
        private RoomViewConnectedDisplay disp1;
        private RoomSetup roomSetup;
        private Scheduling schedule;
        private Evertz Evertz;
        public EvertzHandler(Ts1070 tp, Am300 am3200, RoomViewConnectedDisplay disp1, RoomSetup roomSetup)
        {
            

            this.tp = tp;
            this.am3200 = am3200;
            this.disp1 = disp1;

            this.tp.SigChange += new SigEventHandler(tp_SigChange);
            this.tp.OnlineStatusChange += new OnlineStatusChangeEventHandler(tp_OnlineStatusChange);
            this.disp1.OnlineStatusChange += new OnlineStatusChangeEventHandler(disp1_OnlineStatusChange);
            this.am3200.OnlineStatusChange += new OnlineStatusChangeEventHandler(am3200_OnlineStatusChange);
            this.disp1.BaseEvent += new BaseEventHandler(disp1_BaseEvent);

            this.roomSetup = roomSetup;

            //schedule = new Scheduling(roomSetup, tp, am3200, disp1, hdmd);
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
                if (disp1.PowerOnFeedback.BoolValue)  //Power On
                {
                    tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = false;
                    disp1.SourceSelectSigs[5].Pulse();
                }

                if (disp1.PowerOffFeedback.BoolValue) //Power Off
                {
                    tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = true;
                }

                if (!disp1.SourceSelectFeedbackSigs[5].BoolValue)
                {
                    disp1.SourceSelectSigs[5].Pulse();
                }
            }
            catch (Exception e)
            {
                ErrorLog.Notice($"EvertzHandler Error:  {e.Message}");
                CrestronConsole.PrintLine($"EvertzHandler Error:  {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject + " Disp1_BaseEvent", e.Message);
            }

        }

        public void tp_ButtonStatus(String SourceNum, String OutputNum)
        {
            CrestronConsole.PrintLine($"tp_ButtonStatus Called.  SourceNum: {SourceNum}, OutputNum: {OutputNum}");
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
                                        case 120:
                                            {
                                                
                                                if (disp1.PowerOnFeedback.BoolValue)
                                                {
                                                    //if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //    disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    //hdmdClass.RouteVideo(2);
                                                }
                                                break;
                                            }
                                        case 121:
                                            {
                                                
                                                if (disp1.PowerOnFeedback.BoolValue)
                                                {
                                                    //if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //    disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    //hdmdClass.RouteVideo(3);
                                                }
                                                break;
                                            }
                                        case 122:
                                            {
                                                
                                                if (disp1.PowerOnFeedback.BoolValue)
                                                {
                                                    //if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //    disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    //hdmdClass.RouteVideo(1);
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn1_PwrOn):  //Power On
                                            {
                                                if (disp1.PowerOffFeedback.BoolValue)
                                                {
                                                    disp1.PowerOn();
                                                //    if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                //        disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                }
                                                break;
                                            }
                                        case 130:
                                            {
                                                break;
                                            }
                                        case 131:
                                            {
                                                break;
                                            }
                                        case 132:
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn1_PwrOff):  //Power Off
                                            {
                                                if (disp1.PowerOnFeedback.BoolValue)
                                                {
                                                    disp1.PowerOff();
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
                ErrorLog.Notice($"EvertzHandler Error:  {e.Message}");
                CrestronConsole.PrintLine($"EvertzHandler Error:  {e.Message}");
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
                    schedule.Alert_Timer("touchpanel", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("TP_OnlineStatusChange: {0}", e.Message);
                ErrorLog.Notice("TP_OnlineStatusChange: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " TP_OnlineStatusChange", e.Message);
            }
        }
        

        private void disp1_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            try
            {CrestronConsole.PrintLine("Display_OnlineStatusChange");
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
                    schedule.Alert_Timer("tv", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
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
                    schedule.Alert_Timer("airmedia", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
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
            {CrestronConsole.PrintLine("Initializing EvertzHandler");
                tp.ExtenderSystemReservedSigs.Use();
                tp.ExtenderSystemReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;
                tp.ExtenderScreenSaverReservedSigs.Use();
                tp.ExtenderScreenSaverReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;
                tp.ExtenderSystem3ReservedSigs.Use();
                tp.ExtenderSystem3ReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;


                if (tp.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(tp.RegistrationFailureReason.ToString());

                if (disp1.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(disp1.RegistrationFailureReason.ToString());

                if (am3200.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(am3200.RegistrationFailureReason.ToString());

                tp.StringInput[((uint)Join.lblRoomName)].StringValue = RoomSetup.Touchpanel.RoomText;
                am3200.HdmiOut.Resolution = CommonStreamingSupport.eScreenResolutions.Resolution1080p60Hz;

                switch (RoomSetup.Touchpanel.TP_RoomType.ToLower())
                {
                    case "evertz_1":
                        CrestronConsole.PrintLine("Evertz Room Setup 1");
                        tp.BooleanInput[((uint)Join.pg1Proj)].BoolValue = true;
                        tp.BooleanInput[((uint)Join.pg2Proj)].BoolValue = false;
                        tp.BooleanInput[((uint)Join.pg3Display)].BoolValue = false;
                        break;
                    case "evertz_2":
                        CrestronConsole.PrintLine("Evertz Room Setup 2");
                        tp.BooleanInput[((uint)Join.pg1Proj)].BoolValue = false;
                        tp.BooleanInput[((uint)Join.pg2Proj)].BoolValue = true;
                        tp.BooleanInput[((uint)Join.pg3Display)].BoolValue = false;
                        break;
                    case "evertz_3":
                        CrestronConsole.PrintLine("Evertz Room Setup 3");
                        tp.BooleanInput[((uint)Join.pg1Proj)].BoolValue = false;
                        tp.BooleanInput[((uint)Join.pg2Proj)].BoolValue = false;
                        tp.BooleanInput[((uint)Join.pg3Display)].BoolValue = true;
                        break;
                }

                Evertz = new Evertz();
                Evertz.Initialize();
                CrestronConsole.PrintLine("Evertz Initialize Called?");

                
                


            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error initializing EvertzHandler: {0}", e.Message);
                ErrorLog.Error("Error initializing EvertzHandler: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " Error initializing EvertzHandler", e.Message);
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
