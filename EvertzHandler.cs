using System;
using Crestron.SimplSharp;                          	// For Basic SIMPL# Classes
using Crestron.SimplSharpPro;                       	// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro.UI;
using Crestron.SimplSharpPro.DM.AirMedia;
using Crestron.SimplSharpPro.DM;
using Crestron.SimplSharpPro.CrestronConnected;
using Independentsoft.Email.Mime;

namespace NFAHRooms
{


    public static class EvertzHandler
    {
        //private static Ts1070 tp;
        //private static Am300 am3200;
        //private static RoomViewConnectedDisplay disp1;
        //private static RoomSetup roomSetup;
        private static Scheduling schedule;
        ////private Evertz Evertz;
        //public EvertzHandler(Ts1070 tp, Am300 am3200, RoomViewConnectedDisplay disp1, RoomSetup roomSetup)
        //{
            

        //    this.tp = tp;
        //    this.am3200 = am3200;
        //    this.disp1 = disp1;

        //    this.tp.SigChange += new SigEventHandler(tp_SigChange);
        //    this.tp.OnlineStatusChange += new OnlineStatusChangeEventHandler(tp_OnlineStatusChange);
        //    this.disp1.OnlineStatusChange += new OnlineStatusChangeEventHandler(disp1_OnlineStatusChange);
        //    this.am3200.OnlineStatusChange += new OnlineStatusChangeEventHandler(am3200_OnlineStatusChange);
        //    this.disp1.BaseEvent += new BaseEventHandler(disp1_BaseEvent);

        //    this.roomSetup = roomSetup;

        //    //schedule = new Scheduling(roomSetup, tp, am3200, disp1, hdmd);
        //}

        private static void disp1_BaseEvent(GenericBase currentDevice, BaseEventArgs args)
        {
            ///
            ///btnPwrOff = 23,  //If power is on and you want to turn it off, it's this button
            ///btnPwrOn = 33,  //If power is off and you want to turn it on, it's this button
            ///btnPwrOnVis = 33 //If power is off this button should be visible
            ///
            try
            {
                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)  //Power On
                {
                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = false;
                    ControlSystem.proj1.SourceSelectSigs[5].Pulse();
                }

                if (ControlSystem.proj1.PowerOffFeedback.BoolValue) //Power Off
                {
                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = true;
                }

                if (!ControlSystem.proj1.SourceSelectFeedbackSigs[5].BoolValue)
                {
                    ControlSystem.proj1.SourceSelectSigs[5].Pulse();
                }
            }
            catch (Exception e)
            {
                ErrorLog.Notice($"EvertzHandler Error:  {e.Message}");
                CrestronConsole.PrintLine($"EvertzHandler Error:  {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject + " Disp1_BaseEvent", e.Message);
            }

        }

        public static void tp_ButtonStatus(String Output, String Input)
        {
            switch (Output)
            {
                case "1":
                    tp_ClearButtonStatus(EvertzOutputs.out_Proj1.ToString());

                        switch (Input)
                        {
                            case "1":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn1_PCOn)].BoolValue = true;
                                break;
                            case "2":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn1_ExtDeskOn)].BoolValue = true;
                                break;
                            case "3":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn1_DocCamOn)].BoolValue = true;
                                break;
                            case "4":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn1_AirMediaOn)].BoolValue = true;
                                break;
                            case "5":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn1_AuxOn)].BoolValue = true;
                                break;
                            case "8":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn1_DSPwrOn)].BoolValue = true;
                                break;
                        }
                    break;
                case "2":
                    tp_ClearButtonStatus(EvertzOutputs.out_Proj2.ToString());

                        switch (Input)
                        {
                            case "1":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_PCOn)].BoolValue = true;
                                break;
                        case "2":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_ExtDeskOn)].BoolValue = true;
                                break;
                        case "3":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_DocCamOn)].BoolValue = true;
                                break;
                        case "4":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_AirMediaOn)].BoolValue = true;
                                break;
                        case "5":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_AuxOn)].BoolValue = true;
                                break;
                        case "8":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_DSPwrOn)].BoolValue = true;
                                break;
                        }
                    break;
                case "3":
                    tp_ClearButtonStatus(EvertzOutputs.out_Proj3.ToString());

                        switch (Input)
                        {
                            case "1":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn3_PCOn)].BoolValue = true;
                                break;
                            case "2":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn3_ExtDeskOn)].BoolValue = true;
                                break;
                            case "3":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn3_DocCamOn)].BoolValue = true;
                                break;
                            case "4":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn3_AirMediaOn)].BoolValue = true;
                                break;
                            case "5":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn3_AuxOn)].BoolValue = true;
                                break;
                            case "8":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn3_DSPwrOn)].BoolValue = true;
                                break;
                            }
                    break;
            }
        }
        private static void tp_ClearButtonStatus(String Output)
        {
            switch (Output)
            {
                case "1":
                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_PCOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_ExtDeskOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_DocCamOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_AirMediaOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_AuxOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_DSPwrOn)].BoolValue = false;
                    break;
                case "2":
                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_PCOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_ExtDeskOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_DocCamOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_AirMediaOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_AuxOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_DSPwrOn)].BoolValue = false;
                    break;
                case "3":
                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_PCOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_ExtDeskOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_DocCamOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_AirMediaOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_AuxOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_DSPwrOn)].BoolValue = false;
                    break;
            }
            ControlSystem.tp.BooleanInput[((uint)Join.btn1_PCOn)].BoolValue = false;
        }
        private static async void tp_SigChange(BasicTriList currentDevice, SigEventArgs args)
        {CrestronConsole.PrintLine("tp_SigChange");
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
                                        case ((uint)Join.btn1_PCOff):
                                            {
                                                Output1();

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((int)EvertzOutputs.out_Proj1).ToString(), ((uint)EvertzInputs.in_PCMain).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_ExtDeskOff):
                                            {
                                                Output1();

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj1).ToString(), ((uint)EvertzInputs.in_PCExtDesk).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_DocCamOff):
                                            {
                                                Output1();

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj1).ToString(), ((uint)EvertzInputs.in_DocCam).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_AirMediaOff):
                                            {
                                                Output1();

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj1).ToString(), ((uint)EvertzInputs.in_AirMedia).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_AuxOff):
                                            {
                                                Output1();

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj1).ToString(), ((uint)EvertzInputs.in_Aux).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_DSPwrOff):
                                            {
                                                Output1();

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj1).ToString(), ((uint)EvertzInputs.in_DS).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_PwrOn):  //Power On
                                            {
                                                if (ControlSystem.proj1.PowerOffFeedback.BoolValue)
                                                    Output1();                                                
                                                break;
                                            }
                                        case ((uint)Join.btn1_PCOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn1_ExtDeskOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn1_DocCamOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn1_AirMediaOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn1_AuxOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn1_DSPwrOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn1_PwrOff):  //Power Off
                                            {
                                                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                                    ControlSystem.proj1.PowerOff();
                                                break;
                                            }
                                        case ((uint)Join.btn2_PCOff):
                                            {

                                                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                                {
                                                    //if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //    disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    //hdmdClass.RouteVideo(2);
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn2_ExtDeskOff):
                                            {

                                                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                                {
                                                    //if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //    disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    //hdmdClass.RouteVideo(3);
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn2_DocCamOff):
                                            {

                                                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                                {
                                                    //if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //    disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    //hdmdClass.RouteVideo(1);
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn2_AirMediaOff):
                                            {

                                                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                                {
                                                    //if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //    disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    //hdmdClass.RouteVideo(4);
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn2_AuxOff):
                                            {

                                                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                                {
                                                    //if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //    disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    //hdmdClass.RouteVideo(5);
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn2_DSPwrOff):
                                            {

                                                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                                {
                                                    //if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //    disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    //hdmdClass.RouteVideo(6);
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn2_PwrOn):  //Power On
                                            {
                                                if (ControlSystem.proj1.PowerOffFeedback.BoolValue)
                                                {
                                                    ControlSystem.proj1.PowerOn();
                                                    //    if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //        disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn2_PCOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn2_ExtDeskOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn2_DocCamOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn2_AirMediaOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn2_AuxOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn2_DSPwrOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn2_PwrOff):  //Power Off
                                            {
                                                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                                {
                                                    ControlSystem.proj1.PowerOff();
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn3_PCOff):
                                            {

                                                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                                {
                                                    //if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //    disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    //hdmdClass.RouteVideo(2);
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn3_ExtDeskOff):
                                            {

                                                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                                {
                                                    //if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //    disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    //hdmdClass.RouteVideo(3);
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn3_DocCamOff):
                                            {

                                                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                                {
                                                    //if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //    disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    //hdmdClass.RouteVideo(1);
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn3_AirMediaOff):
                                            {

                                                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                                {
                                                    //if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //    disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    //hdmdClass.RouteVideo(4);
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn3_AuxOff):
                                            {

                                                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                                {
                                                    //if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //    disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    //hdmdClass.RouteVideo(5);
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn3_DSPwrOff):
                                            {

                                                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                                {
                                                    //if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //    disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                    //hdmdClass.RouteVideo(6);
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn3_PwrOn):  //Power On
                                            {
                                                if (ControlSystem.proj1.PowerOffFeedback.BoolValue)
                                                {
                                                    ControlSystem.proj1.PowerOn();
                                                    //    if (disp1.Video.Source.SourceSelect.UShortValue != 1)
                                                    //        disp1.Video.Source.SourceSelect.UShortValue = 1;
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn3_PCOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn3_ExtDeskOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn3_DocCamOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn3_AirMediaOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn3_AuxOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn3_DSPwrOn):
                                            {
                                                break;
                                            }
                                        case ((uint)Join.btn3_PwrOff):  //Power Off
                                            {
                                                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                                {
                                                    ControlSystem.proj1.PowerOff();
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
        private static void Output1()
        {
            if (ControlSystem.proj1.PowerOffFeedback.BoolValue)
                ControlSystem.proj1.PowerOn();

            if (!ControlSystem.proj1.SourceSelectFeedbackSigs[5].BoolValue)
                ControlSystem.proj1.SourceSelectSigs[5].Pulse();

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
        

        private static void disp1_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
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
        public static void Initialize()
        {
            try
            {CrestronConsole.PrintLine("Initializing EvertzHandler");
                ControlSystem.tp.ExtenderSystemReservedSigs.Use();
                ControlSystem.tp.ExtenderSystemReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;
                ControlSystem.tp.ExtenderScreenSaverReservedSigs.Use();
                ControlSystem.tp.ExtenderScreenSaverReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;
                ControlSystem.tp.ExtenderSystem3ReservedSigs.Use();
                ControlSystem.tp.ExtenderSystem3ReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;


                if (ControlSystem.tp.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(ControlSystem.tp.RegistrationFailureReason.ToString());

                if (ControlSystem.proj1.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(ControlSystem.proj1.RegistrationFailureReason.ToString());

                if (ControlSystem.am3200.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(ControlSystem.am3200.RegistrationFailureReason.ToString());
                                
                
                switch (RoomSetup.Touchpanel.TP_RoomType.ToLower())
                {
                    case "evertz_1":
                        CrestronConsole.PrintLine("Evertz Room Setup 1");
                        ControlSystem.tp.BooleanInput[((uint)Join.pg1Proj)].BoolValue = true;
                        ControlSystem.tp.BooleanInput[((uint)Join.pg2Proj)].BoolValue = false;
                        ControlSystem.tp.BooleanInput[((uint)Join.pg3Display)].BoolValue = false;

                        ControlSystem.tp.SigChange += new SigEventHandler(tp_SigChange);
                        ControlSystem.tp.OnlineStatusChange += new OnlineStatusChangeEventHandler(tp_OnlineStatusChange);
                        ControlSystem.proj1.OnlineStatusChange += new OnlineStatusChangeEventHandler(disp1_OnlineStatusChange);
                        ControlSystem.am3200.OnlineStatusChange += new OnlineStatusChangeEventHandler(am3200_OnlineStatusChange);
                        ControlSystem.proj1.BaseEvent += new BaseEventHandler(disp1_BaseEvent);
                        break;
                    case "evertz_2":
                        CrestronConsole.PrintLine("Evertz Room Setup 2");
                        ControlSystem.tp.BooleanInput[((uint)Join.pg1Proj)].BoolValue = false;
                        ControlSystem.tp.BooleanInput[((uint)Join.pg2Proj)].BoolValue = true;
                        ControlSystem.tp.BooleanInput[((uint)Join.pg3Display)].BoolValue = false;
                        break;
                    case "evertz_3":
                        CrestronConsole.PrintLine("Evertz Room Setup 3");
                        ControlSystem.tp.BooleanInput[((uint)Join.pg1Proj)].BoolValue = false;
                        ControlSystem.tp.BooleanInput[((uint)Join.pg2Proj)].BoolValue = false;
                        ControlSystem.tp.BooleanInput[((uint)Join.pg3Display)].BoolValue = true;
                        break;
                }

                ControlSystem.tp.StringInput[((uint)Join.lblRoomName)].StringValue = RoomSetup.Touchpanel.RoomText;
                ControlSystem.am3200.HdmiOut.Resolution = CommonStreamingSupport.eScreenResolutions.Resolution1080p60Hz;

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
