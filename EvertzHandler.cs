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
                        tp_ClearButtonStatus(((uint)EvertzOutputs.out_Proj1).ToString());
                        
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
                            case "0":
                                if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                    ControlSystem.proj1.PowerOff();
                            break;
                        }
                    break;
                case "2":
                    tp_ClearButtonStatus(((uint)EvertzOutputs.out_Proj2).ToString());

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
                    tp_ClearButtonStatus(((uint)EvertzOutputs.out_Proj3).ToString());

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
            
        }
        private static async void tp_SigChange(BasicTriList currentDevice, SigEventArgs args)
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
                                        case ((uint)Join.btn1_PCOff):
                                            {
                                                SetOutput(((uint)Join.btn1_PCOff));

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((int)EvertzOutputs.out_Proj1).ToString(), ((uint)EvertzInputs.in_PCMain).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_ExtDeskOff):
                                            {
                                                SetOutput((uint)Join.btn1_ExtDeskOff);

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj1).ToString(), ((uint)EvertzInputs.in_PCExtDesk).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_DocCamOff):
                                            {
                                                SetOutput((uint)Join.btn1_DocCamOff);

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj1).ToString(), ((uint)EvertzInputs.in_DocCam).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_AirMediaOff):
                                            {
                                                SetOutput((uint)Join.btn1_AirMediaOff);

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj1).ToString(), ((uint)EvertzInputs.in_AirMedia).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_AuxOff):
                                            {
                                                SetOutput((uint)Join.btn1_AuxOff);

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj1).ToString(), ((uint)EvertzInputs.in_Aux).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_DSPwrOff):
                                            {
                                                SetOutput((uint)Join.btn1_AuxOff);

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj1).ToString(), ((uint)EvertzInputs.in_DS).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_PwrOn):  //Power On
                                            {
                                                if (ControlSystem.proj1.PowerOffFeedback.BoolValue)
                                                    SetOutput((uint)Join.btn1_PwrOn);
                                                break;
                                            }
                                        case ((uint)Join.btn1_PwrOff):  //Power Off
                                            {                                                
                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj1).ToString(), ((uint)EvertzInputs.in_Blank).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn2_PCOff):
                                            {
                                                SetOutput(((uint)Join.btn2_PCOff));

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((int)EvertzOutputs.out_Proj2).ToString(), ((uint)EvertzInputs.in_PCMain).ToString());
                                                break;

                                            }
                                        case ((uint)Join.btn2_ExtDeskOff):
                                            {
                                                SetOutput((uint)Join.btn2_ExtDeskOff);

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj2).ToString(), ((uint)EvertzInputs.in_PCExtDesk).ToString());
                                                break;

                                            }
                                        case ((uint)Join.btn2_DocCamOff):
                                            {
                                                SetOutput((uint)Join.btn2_DocCamOff);

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj2).ToString(), ((uint)EvertzInputs.in_DocCam).ToString());
                                                break;

                                            }
                                        case ((uint)Join.btn2_AirMediaOff):
                                            {
                                                SetOutput((uint)Join.btn2_AirMediaOff);

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj2).ToString(), ((uint)EvertzInputs.in_AirMedia).ToString());
                                                break;

                                            }
                                        case ((uint)Join.btn2_AuxOff):
                                            {

                                                SetOutput((uint)Join.btn2_AuxOff);

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj2).ToString(), ((uint)EvertzInputs.in_Aux).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn2_DSPwrOff):
                                            {
                                                SetOutput((uint)Join.btn2_AuxOff);

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj2).ToString(), ((uint)EvertzInputs.in_DS).ToString());
                                                break;

                                            }
                                        case ((uint)Join.btn2_PwrOn):  //Power On
                                            {
                                                if (ControlSystem.proj2.PowerOffFeedback.BoolValue)
                                                    SetOutput((uint)Join.btn2_PwrOn);
                                                break;
                                            }
                                        case ((uint)Join.btn2_PwrOff):  //Power Off
                                            {
                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj2).ToString(), ((uint)EvertzInputs.in_Blank).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn3_PCOff):
                                            {
                                                SetOutput(((uint)Join.btn3_PCOff));

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((int)EvertzOutputs.out_Proj3).ToString(), ((uint)EvertzInputs.in_PCMain).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn3_ExtDeskOff):
                                            {
                                                SetOutput((uint)Join.btn3_ExtDeskOff);

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj3).ToString(), ((uint)EvertzInputs.in_PCExtDesk).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn3_DocCamOff):
                                            {
                                                SetOutput((uint)Join.btn3_DocCamOff);

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj3).ToString(), ((uint)EvertzInputs.in_DocCam).ToString());
                                                break;
                                                
                                            }
                                        case ((uint)Join.btn3_AirMediaOff):
                                            {
                                                SetOutput((uint)Join.btn3_AirMediaOff);

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj3).ToString(), ((uint)EvertzInputs.in_AirMedia).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn3_AuxOff):
                                            {

                                                SetOutput((uint)Join.btn3_AuxOff);

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj3).ToString(), ((uint)EvertzInputs.in_Aux).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn3_DSPwrOff):
                                            {
                                                SetOutput((uint)Join.btn3_AuxOff);

                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj3).ToString(), ((uint)EvertzInputs.in_DS).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn3_PwrOn):  //Power On
                                            {
                                                if (ControlSystem.proj3.PowerOffFeedback.BoolValue)
                                                    SetOutput((uint)Join.btn3_PwrOn);
                                                break;
                                            }
                                        case ((uint)Join.btn3_PwrOff):  //Power Off
                                            {
                                                await Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj3).ToString(), ((uint)EvertzInputs.in_Blank).ToString());
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
        private static void SetOutput(uint OutputNum)
        {
            if (OutputNum > 100 && OutputNum < 200)
            {
                if (ControlSystem.proj1.PowerOffFeedback.BoolValue)
                    ControlSystem.proj1.PowerOn();

                if (!ControlSystem.proj1.SourceSelectFeedbackSigs[((uint)SonyProjInputs.ProjHDMI)].BoolValue)
                    ControlSystem.proj1.SourceSelectSigs[((uint)SonyProjInputs.ProjHDMI)].Pulse();
            }
            if (OutputNum > 200 && OutputNum < 300)
            {
                if (ControlSystem.proj2.PowerOffFeedback.BoolValue)
                    ControlSystem.proj2.PowerOn();

                if (!ControlSystem.proj2.SourceSelectFeedbackSigs[((uint)SonyProjInputs.ProjHDMI)].BoolValue)
                    ControlSystem.proj2.SourceSelectSigs[((uint)SonyProjInputs.ProjHDMI)].Pulse();
            }
            if (OutputNum > 300 && OutputNum < 400)
            {
                if (ControlSystem.proj3.PowerOffFeedback.BoolValue)
                    ControlSystem.proj3.PowerOn();

                if (!ControlSystem.proj3.SourceSelectFeedbackSigs[((uint)SonyProjInputs.ProjHDMI)].BoolValue)
                    ControlSystem.proj3.SourceSelectSigs[((uint)SonyProjInputs.ProjHDMI)].Pulse();
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

                    
                    ControlSystem.tp.ExtenderButtonToolbarReservedSigs.HideButtonToolbar();
                    ControlSystem.tp.ExtenderSystemReservedSigs.LcdBrightnessAutoOff();
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
        private static void proj1_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            return;
            //try
            //{
            //    if (args.DeviceOnLine)
            //    {
            //        if (Scheduling.errorCounts.TryGetValue("proj1", out int x) && x > 0)
            //        {
            //            Scheduling.errorCounts["proj1"] = 0;
            //            Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
            //        }
            //    }
            //    else if (!args.DeviceOnLine)
            //    {
            //        Scheduling.Alert_Timer("proj1", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
            //    }
            //}
            //catch (Exception e)
            //{
            //    CrestronConsole.PrintLine("Proj1_OnlineStatusChange: {0}", e.Message);
            //    ErrorLog.Notice("Proj1_OnlineStatusChange: {0}", e.Message);
            //    Email.SendEmail(RoomSetup.MailSubject + " Proj1_OnlineStatusChange", e.Message);
            //}
        }
        private static void proj1_BaseEvent(GenericBase currentDevice, BaseEventArgs args)
        {
            return;
            //try
            //{
            //    if (ControlSystem.proj1.PowerOnFeedback.BoolValue)  //Power On
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = false;
            //        ControlSystem.proj1.SourceSelectSigs[5].Pulse();
            //    }

            //    if (ControlSystem.proj1.PowerOffFeedback.BoolValue) //Power Off
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = true;
            //    }

            //    if (!ControlSystem.proj1.SourceSelectFeedbackSigs[5].BoolValue)
            //    {
            //        ControlSystem.proj1.SourceSelectSigs[5].Pulse();
            //    }
            //}
            //catch (Exception e)
            //{
            //    ErrorLog.Notice($"EvertzHandler Error:  {e.Message}");
            //    CrestronConsole.PrintLine($"EvertzHandler Error:  {e.Message}");
            //    Email.SendEmail(RoomSetup.MailSubject + " Proj1_BaseEvent", e.Message);
            //}
        }
        private static void proj2_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            return;
            //try
            //{
            //    if (args.DeviceOnLine)
            //    {
            //        if (Scheduling.errorCounts.TryGetValue("proj2", out int x) && x > 0)
            //        {
            //            Scheduling.errorCounts["proj2"] = 0;
            //            Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
            //        }
            //    }
            //    else if (!args.DeviceOnLine)
            //    {
            //        Scheduling.Alert_Timer("proj2", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
            //    }
            //}
            //catch (Exception e)
            //{
            //    CrestronConsole.PrintLine("Proj2_OnlineStatusChange: {0}", e.Message);
            //    ErrorLog.Notice("Proj2_OnlineStatusChange: {0}", e.Message);
            //    Email.SendEmail(RoomSetup.MailSubject + " Proj2_OnlineStatusChange", e.Message);
            //}
        }
        private static void proj2_BaseEvent(GenericBase currentDevice, BaseEventArgs args)
        {
            return;
            //try
            //{
            //    if (ControlSystem.proj2.PowerOnFeedback.BoolValue)  //Power On
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = false;
            //        ControlSystem.proj2.SourceSelectSigs[5].Pulse();
            //    }

            //    if (ControlSystem.proj2.PowerOffFeedback.BoolValue) //Power Off
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = true;
            //    }

            //    if (!ControlSystem.proj2.SourceSelectFeedbackSigs[5].BoolValue)
            //    {
            //        ControlSystem.proj2.SourceSelectSigs[5].Pulse();
            //    }
            //}
            //catch (Exception e)
            //{
            //    ErrorLog.Notice($"EvertzHandler Error:  {e.Message}");
            //    CrestronConsole.PrintLine($"EvertzHandler Error:  {e.Message}");
            //    Email.SendEmail(RoomSetup.MailSubject + " Proj2_BaseEvent", e.Message);
            //}
        }
        private static void proj3_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            return;
            //try
            //{
            //    if (args.DeviceOnLine)
            //    {
            //        if (Scheduling.errorCounts.TryGetValue("proj3", out int x) && x > 0)
            //        {
            //            Scheduling.errorCounts["proj3"] = 0;
            //            Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
            //        }
            //    }
            //    else if (!args.DeviceOnLine)
            //    {
            //        Scheduling.Alert_Timer("proj3", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
            //    }
            //}
            //catch (Exception e)
            //{
            //    CrestronConsole.PrintLine("Proj3_OnlineStatusChange: {0}", e.Message);
            //    ErrorLog.Notice("Proj3_OnlineStatusChange: {0}", e.Message);
            //    Email.SendEmail(RoomSetup.MailSubject + " Proj3_OnlineStatusChange", e.Message);
            //}
        }
        private static void proj3_BaseEvent(GenericBase currentDevice, BaseEventArgs args)
        {
            return;
            //try
            //{
            //    if (ControlSystem.proj3.PowerOnFeedback.BoolValue)  //Power On
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn3_PwrOnVis)].BoolValue = false;
            //        ControlSystem.proj3.SourceSelectSigs[5].Pulse();
            //    }

            //    if (ControlSystem.proj3.PowerOffFeedback.BoolValue) //Power Off
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn3_PwrOnVis)].BoolValue = true;
            //    }

            //    if (!ControlSystem.proj3.SourceSelectFeedbackSigs[5].BoolValue)
            //    {
            //        ControlSystem.proj3.SourceSelectSigs[5].Pulse();
            //    }
            //}
            //catch (Exception e)
            //{
            //    ErrorLog.Notice($"EvertzHandler Error:  {e.Message}");
            //    CrestronConsole.PrintLine($"EvertzHandler Error:  {e.Message}");
            //    Email.SendEmail(RoomSetup.MailSubject + " Proj3_BaseEvent", e.Message);
            //}
        }
        private static void disp2_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            return;
            //try
            //{
            //    if (args.DeviceOnLine)
            //    {
            //        if (Scheduling.errorCounts.TryGetValue("tv", out int x) && x > 0)
            //        {
            //            Scheduling.errorCounts["tv"] = 0;
            //            Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
            //        }
            //    }
            //    else if (!args.DeviceOnLine)
            //    {
            //        Scheduling.Alert_Timer("tv", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
            //    }
            //}
            //catch (Exception e)
            //{
            //    CrestronConsole.PrintLine("Display_OnlineStatusChange: {0}", e.Message);
            //    ErrorLog.Notice("Display_OnlineStatusChange: {0}", e.Message);
            //    Email.SendEmail(RoomSetup.MailSubject + " Display_OnlineStatusChange", e.Message);
            //}
        }
        private static void disp2_BaseEvent(GenericBase currentDevice, BaseEventArgs args)
        {
            return;
            //try
            //{
            //    if (ControlSystem.disp2.PowerOnFeedback.BoolValue)  //Power On
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = false;
            //        ControlSystem.disp2.SourceSelectSigs[5].Pulse();
            //    }

            //    if (ControlSystem.disp2.PowerOffFeedback.BoolValue) //Power Off
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = true;
            //    }

            //    if (!ControlSystem.disp2.SourceSelectFeedbackSigs[5].BoolValue)
            //    {
            //        ControlSystem.disp2.SourceSelectSigs[5].Pulse();
            //    }
            //}
            //catch (Exception e)
            //{
            //    ErrorLog.Notice($"EvertzHandler Error:  {e.Message}");
            //    CrestronConsole.PrintLine($"EvertzHandler Error:  {e.Message}");
            //    Email.SendEmail(RoomSetup.MailSubject + " Disp2_BaseEvent", e.Message);
            //}
        }
        private static void disp3_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            return;
            //try
            //{
            //    if (args.DeviceOnLine)
            //    {
            //        if (Scheduling.errorCounts.TryGetValue("tv", out int x) && x > 0)
            //        {
            //            Scheduling.errorCounts["tv"] = 0;
            //            Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
            //        }
            //    }
            //    else if (!args.DeviceOnLine)
            //    {
            //        Scheduling.Alert_Timer("tv", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
            //    }
            //}
            //catch (Exception e)
            //{
            //    CrestronConsole.PrintLine("Display_OnlineStatusChange: {0}", e.Message);
            //    ErrorLog.Notice("Display_OnlineStatusChange: {0}", e.Message);
            //    Email.SendEmail(RoomSetup.MailSubject + " Display_OnlineStatusChange", e.Message);
            //}
        }
        private static void disp3_BaseEvent(GenericBase currentDevice, BaseEventArgs args)
        {
            return;
            //try
            //{
            //    if (ControlSystem.disp3.PowerOnFeedback.BoolValue)  //Power On
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn3_PwrOnVis)].BoolValue = false;
            //        ControlSystem.disp3.SourceSelectSigs[5].Pulse();
            //    }

            //    if (ControlSystem.disp3.PowerOffFeedback.BoolValue) //Power Off
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn3_PwrOnVis)].BoolValue = true;
            //    }

            //    if (!ControlSystem.disp3.SourceSelectFeedbackSigs[5].BoolValue)
            //    {
            //        ControlSystem.disp3.SourceSelectSigs[5].Pulse();
            //    }
            //}
            //catch (Exception e)
            //{
            //    ErrorLog.Notice($"EvertzHandler Error:  {e.Message}");
            //    CrestronConsole.PrintLine($"EvertzHandler Error:  {e.Message}");
            //    Email.SendEmail(RoomSetup.MailSubject + " Disp3_BaseEvent", e.Message);
            //}
        }

        public static void Initialize()
        {
            try
            {
                ControlSystem.tp.ExtenderButtonToolbarReservedSigs.Use();
                ControlSystem.tp.ExtenderButtonToolbarReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;
                ControlSystem.tp.ExtenderSystemReservedSigs.Use();
                ControlSystem.tp.ExtenderSystemReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;
                ControlSystem.tp.ExtenderScreenSaverReservedSigs.Use();
                ControlSystem.tp.ExtenderScreenSaverReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;
                ControlSystem.tp.ExtenderSystem3ReservedSigs.Use();
                ControlSystem.tp.ExtenderSystem3ReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;
                
                

                if (ControlSystem.tp.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(ControlSystem.tp.RegistrationFailureReason.ToString());

                    ControlSystem.tp.SigChange += new SigEventHandler(tp_SigChange);
                    ControlSystem.tp.OnlineStatusChange += new OnlineStatusChangeEventHandler(tp_OnlineStatusChange);

                if (ControlSystem.am3200.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(ControlSystem.am3200.RegistrationFailureReason.ToString());

                    ControlSystem.am3200.OnlineStatusChange += new OnlineStatusChangeEventHandler(am3200_OnlineStatusChange);

                if (RoomSetup.Display1 == "proj")
                { 
                    if (ControlSystem.proj1.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        throw new Exception(ControlSystem.proj1.RegistrationFailureReason.ToString());

                    ControlSystem.proj1.OnlineStatusChange += new OnlineStatusChangeEventHandler(proj1_OnlineStatusChange);
                    ControlSystem.proj1.BaseEvent += new BaseEventHandler(proj1_BaseEvent);
                }
                if (RoomSetup.Display1 == "tv")
                {
                    if (ControlSystem.disp1.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        throw new Exception(ControlSystem.disp1.RegistrationFailureReason.ToString());

                    ControlSystem.disp1.OnlineStatusChange += new OnlineStatusChangeEventHandler(disp1_OnlineStatusChange);
                    ControlSystem.disp1.BaseEvent += new BaseEventHandler(disp1_BaseEvent);
                }

                if (RoomSetup.Display2 == "proj")
                {
                    if (ControlSystem.proj2.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        throw new Exception(ControlSystem.proj2.RegistrationFailureReason.ToString());

                    ControlSystem.proj2.OnlineStatusChange += new OnlineStatusChangeEventHandler(proj2_OnlineStatusChange);
                    ControlSystem.proj2.BaseEvent += new BaseEventHandler(proj2_BaseEvent);
                }
                if (RoomSetup.Display2 == "tv")
                {
                    if (ControlSystem.disp2.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        throw new Exception(ControlSystem.disp2.RegistrationFailureReason.ToString());

                    ControlSystem.disp2.OnlineStatusChange += new OnlineStatusChangeEventHandler(disp2_OnlineStatusChange);
                    ControlSystem.disp2.BaseEvent += new BaseEventHandler(disp2_BaseEvent);
                }
            

                if (RoomSetup.Display3 == "proj")
                {
                    if (ControlSystem.proj3.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        throw new Exception(ControlSystem.proj3.RegistrationFailureReason.ToString());

                    ControlSystem.proj3.OnlineStatusChange += new OnlineStatusChangeEventHandler(proj3_OnlineStatusChange);
                    ControlSystem.proj3.BaseEvent += new BaseEventHandler(proj3_BaseEvent);
                }
                if (RoomSetup.Display3 == "tv")
                {
                    if (ControlSystem.disp3.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        throw new Exception(ControlSystem.disp3.RegistrationFailureReason.ToString());

                    ControlSystem.disp3.OnlineStatusChange += new OnlineStatusChangeEventHandler(disp3_OnlineStatusChange);
                    ControlSystem.disp3.BaseEvent += new BaseEventHandler(disp3_BaseEvent);
                }
                
                switch (RoomSetup.Touchpanel.TP_RoomType.ToLower())
                {
                    case "evertz_1":
                        CrestronConsole.PrintLine("Evertz Room Setup 1");
                        ControlSystem.tp.BooleanInput[((uint)Join.pg1Proj)].BoolValue = true;
                        ControlSystem.tp.BooleanInput[((uint)Join.pg2Proj)].BoolValue = false;
                        ControlSystem.tp.BooleanInput[((uint)Join.pg3Display)].BoolValue = false;
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


                Evertz.Initialize();
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error initializing EvertzHandler: {0}", e.Message);
                CrestronConsole.PrintLine("Error initializing EvertzHandler: {0}", e.StackTrace);
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
