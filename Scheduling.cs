using Crestron.SimplSharp.Scheduler;
using Crestron.SimplSharp;
using System;
using System.Collections.Generic;
using Crestron.SimplSharpPro.UI;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.DM.AirMedia;
using Crestron.SimplSharpPro.CrestronConnected;
using Crestron.SimplSharpPro.DM;
using System.Net.Http;



namespace NFAHRooms
{
    public static class Scheduling
    {
        public static ScheduledEventGroup SystemEventGroup;
        public static List<ScheduledEvent> SysEvent = new List<ScheduledEvent>();
        
        //private RoomSetup _rsetup;
        //private Email _email;
        //private Ts1070 _tp;
        //private Am300 _am3200;
        //private HdMd4x14kzE _hdmd;
        //private CrestronConnectedDisplayV2 _tv;
        public static Dictionary<string, int> errorCounts = new Dictionary<string, int>();

        public static bool SS_Active;
        public static bool Prox_Active;
        

        //public Scheduling(RoomSetup rsetup, Ts1070 tp, Am300 airmedia, CrestronConnectedDisplayV2 disp1, HdMd4x14kzE hdmd)
        //{
        //    _rsetup = rsetup;
        //    _tp = tp;
        //    _am3200 = airmedia;
        //    _tv = disp1;
        //    _hdmd = hdmd;

            //_email = new Email();
            //_email.EmailSetup();

        //}

        public static void AddDailyTimerEvent()
        {
            foreach (var dailyevent in RoomSetup.DailyEvents)
            {
                string EventName = dailyevent.EventName;

                DateTime SysEventDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, dailyevent.EventTime.Hour, dailyevent.EventTime.Minute, 00);

                try
                {
                    if (dailyevent.Daily == true)
                    {
                        DailyEvent(EventName + SysEvent.Count, SysEventDate, "daily");
                    }
                    else if (dailyevent.WeekDaysOnly == true && dailyevent.WeekendsOnly == true ||
                        (dailyevent.Monday == true && dailyevent.Tuesday == true && dailyevent.Wednesday == true && dailyevent.Thursday == true &&
                        dailyevent.Friday == true && dailyevent.Saturday == true && dailyevent.Sunday == true))
                    {
                        DailyEvent(EventName + SysEvent.Count, SysEventDate, "daily");
                    }

                    else if ((dailyevent.WeekDaysOnly == true || dailyevent.WeekendsOnly == true) && (dailyevent.WeekDaysOnly == false || dailyevent.WeekendsOnly == false))
                    {
                        if (dailyevent.WeekDaysOnly == true)
                        {
                            DailyEvent(EventName + SysEvent.Count, SysEventDate, "weekdays");

                            if (dailyevent.Saturday == true)
                                DailyEvent(EventName + SysEvent.Count, SysEventDate, "saturday");
                            if (dailyevent.Sunday == true)
                                DailyEvent(EventName + SysEvent.Count, SysEventDate, "sunday");
                        }

                        else if (dailyevent.WeekendsOnly == true)
                        {
                            DailyEvent(EventName + SysEvent.Count, SysEventDate, "weekends");

                            if (dailyevent.Monday == true)
                                DailyEvent(EventName + SysEvent.Count, SysEventDate, "monday");
                            if (dailyevent.Tuesday == true)
                                DailyEvent(EventName + SysEvent.Count, SysEventDate, "tuesday");
                            if (dailyevent.Wednesday == true)
                                DailyEvent(EventName + SysEvent.Count, SysEventDate, "wednesday");
                            if (dailyevent.Thursday == true)
                                DailyEvent(EventName + SysEvent.Count, SysEventDate, "thursday");
                            if (dailyevent.Friday == true)
                                DailyEvent(EventName + SysEvent.Count, SysEventDate, "friday");
                        }

                    }

                    else
                    {
                        if (dailyevent.Monday == true)
                            DailyEvent(EventName + SysEvent.Count, SysEventDate, "monday");
                        if (dailyevent.Tuesday == true)
                            DailyEvent(EventName + SysEvent.Count, SysEventDate, "tuesday");
                        if (dailyevent.Wednesday == true)
                            DailyEvent(EventName + SysEvent.Count, SysEventDate, "wednesday");
                        if (dailyevent.Thursday == true)
                            DailyEvent(EventName + SysEvent.Count, SysEventDate, "thursday");
                        if (dailyevent.Friday == true)
                            DailyEvent(EventName + SysEvent.Count, SysEventDate, "friday");
                        if (dailyevent.Saturday == true)
                            DailyEvent(EventName + SysEvent.Count, SysEventDate, "saturday");
                        if (dailyevent.Sunday == true)
                            DailyEvent(EventName + SysEvent.Count, SysEventDate, "sunday");
                    }

                }
                catch (Exception e)
                { 
                    ErrorLog.Error("SystemEventTimer Add: {0}", e.Message);
                    CrestronConsole.PrintLine($"ERROR - SystemEventTimer Add:{e.Message}");
                    Email.SendEmail(RoomSetup.MailSubject, $"ERROR - SystemEventTimer Add: {e.Message} {e.Source} {e.StackTrace}");
                }
            }
        }

        private static void DailyEvent(string name, DateTime eventTime, string daystoschedule)
        {
            

            try
            {
                if (eventTime.TimeOfDay < DateTime.Now.TimeOfDay )
                {
                    if (daystoschedule == "daily") 
                    {
                        eventTime = eventTime.AddDays(1);
                    }

                    if (daystoschedule == "weekdays")
                    {
                        if (eventTime.DayOfWeek == DayOfWeek.Friday)
                        {
                            eventTime = eventTime.AddDays(DaysUntil((int)DayOfWeek.Friday, (int)DayOfWeek.Monday));
                        }
                        else if (eventTime.DayOfWeek == DayOfWeek.Saturday)
                        {
                            eventTime = eventTime.AddDays(DaysUntil((int)DayOfWeek.Saturday, (int)DayOfWeek.Monday));
                        }
                        else if (eventTime.DayOfWeek == DayOfWeek.Sunday)
                        {
                            eventTime = eventTime.AddDays(DaysUntil((int)DayOfWeek.Sunday, (int)DayOfWeek.Monday));
                        }
                        else
                        {
                            eventTime = eventTime.AddDays(1);
                        }
                    }

                    if (daystoschedule == "weekends")
                    {
                        if (eventTime.DayOfWeek == DayOfWeek.Saturday)
                        {
                            eventTime = eventTime.AddDays(1);
                        }
                        else if (eventTime.DayOfWeek == DayOfWeek.Sunday)
                        {
                            eventTime = eventTime.AddDays(DaysUntil((int)DayOfWeek.Sunday, (int)DayOfWeek.Saturday));
                        }
                        else
                        {
                            eventTime = eventTime.AddDays(DaysUntil((int)eventTime.DayOfWeek, (int)DayOfWeek.Saturday));
                        }
                    }
                                               
                    if (daystoschedule == "monday")
                        eventTime = eventTime.AddDays(DaysUntil((int)eventTime.DayOfWeek, (int)DayOfWeek.Monday));
                    if (daystoschedule == "tuesday")
                        eventTime = eventTime.AddDays(DaysUntil((int)eventTime.DayOfWeek, (int)DayOfWeek.Tuesday));
                    if (daystoschedule == "wednesday")
                        eventTime = eventTime.AddDays(DaysUntil((int)eventTime.DayOfWeek, (int)DayOfWeek.Wednesday));
                    if (daystoschedule == "thursday")
                        eventTime = eventTime.AddDays(DaysUntil((int)eventTime.DayOfWeek, (int)DayOfWeek.Thursday));
                    if (daystoschedule == "friday")
                        eventTime = eventTime.AddDays(DaysUntil((int)eventTime.DayOfWeek, (int)DayOfWeek.Friday));
                    if (daystoschedule == "saturday")
                        eventTime = eventTime.AddDays(DaysUntil((int)eventTime.DayOfWeek, (int)DayOfWeek.Saturday));
                    if (daystoschedule == "sunday")
                        eventTime = eventTime.AddDays(DaysUntil((int)eventTime.DayOfWeek, (int)DayOfWeek.Sunday));
                }

                if (eventTime.TimeOfDay > DateTime.Now.TimeOfDay)
                {
                    if (daystoschedule == "weekdays")
                    {
                        if (eventTime.DayOfWeek == DayOfWeek.Friday)
                        {
                            eventTime = eventTime.AddDays(DaysUntil((int)DayOfWeek.Friday, (int)DayOfWeek.Monday));
                        }
                        else if (eventTime.DayOfWeek == DayOfWeek.Saturday)
                        {
                            eventTime = eventTime.AddDays(DaysUntil((int)DayOfWeek.Saturday, (int)DayOfWeek.Monday));
                        }
                        else if (eventTime.DayOfWeek == DayOfWeek.Sunday)
                        {
                            eventTime = eventTime.AddDays(DaysUntil((int)DayOfWeek.Sunday, (int)DayOfWeek.Monday));
                        }
                    }
     
                    if (daystoschedule == "weekends")
                    {
                        if (DateTime.Now.DayOfWeek != DayOfWeek.Sunday && DateTime.Now.DayOfWeek != DayOfWeek.Saturday)
                        {
                            var days = DaysUntil((int)DateTime.Now.DayOfWeek, (int)DayOfWeek.Saturday);
                            if ( days == 7)
                                eventTime = eventTime.AddDays(0);
                            else
                                eventTime = eventTime.AddDays(days);    
                        }
                    }

                    if (daystoschedule == "monday")
                    {
                        var days = DaysUntil((int)DateTime.Now.DayOfWeek, (int)DayOfWeek.Monday);
                        if (days == 7)
                            eventTime = eventTime.AddDays(0);
                        else
                            eventTime = eventTime.AddDays(days);
                    }
                    if (daystoschedule == "tuesday")
                    { var days = DaysUntil((int)DateTime.Now.DayOfWeek, (int)DayOfWeek.Tuesday);
                        if (days == 7)
                            eventTime = eventTime.AddDays(0);
                        else
                            eventTime = eventTime.AddDays(days);
                    }

                    if (daystoschedule == "wednesday")
                    {
                        var days = DaysUntil((int)DateTime.Now.DayOfWeek, (int)DayOfWeek.Wednesday);
                        if (days == 7)
                            eventTime = eventTime.AddDays(0);
                        else
                            eventTime = eventTime.AddDays(days);
                    }
                    if (daystoschedule == "thursday")
                    { var days = DaysUntil((int)DateTime.Now.DayOfWeek, (int)DayOfWeek.Thursday);
                        if (days == 7)
                            eventTime = eventTime.AddDays(0);
                        else
                            eventTime = eventTime.AddDays(days);
                    }
                    if (daystoschedule == "friday")
                    {var days = DaysUntil((int)DateTime.Now.DayOfWeek, (int)DayOfWeek.Friday);
                        if (days == 7)
                            eventTime = eventTime.AddDays(0);
                        else
                            eventTime = eventTime.AddDays(days);
                    }
                    if (daystoschedule == "saturday")
                    {var days = DaysUntil((int)DateTime.Now.DayOfWeek, (int)DayOfWeek.Saturday);
                        if (days == 7)
                            eventTime = eventTime.AddDays(0);
                        else
                            eventTime = eventTime.AddDays(days);
                    }
                    if (daystoschedule == "sunday")
                    {var days = DaysUntil((int)DateTime.Now.DayOfWeek, (int)DayOfWeek.Sunday);
                        if (days == 7)
                            eventTime = eventTime.AddDays(0);
                        else
                            eventTime = eventTime.AddDays(days);
                    }
                }

                SysEvent.Add(new ScheduledEvent(name, SystemEventGroup));
                SysEvent[SysEvent.Count - 1].DateAndTime.SetAbsoluteEventTime(eventTime);
                SysEvent[SysEvent.Count - 1].Persistent = true;
                SysEvent[SysEvent.Count - 1].Acknowledgeable = false;
                
                if (daystoschedule == "daily")  
                    SysEvent[SysEvent.Count - 1].Recurrence.Weekly(ScheduledEventCommon.eWeekDays.All);
                if (daystoschedule == "weekdays")
                    SysEvent[SysEvent.Count - 1].Recurrence.Weekly(ScheduledEventCommon.eWeekDays.Workdays);
                if (daystoschedule == "weekends")
                    SysEvent[SysEvent.Count - 1].Recurrence.Weekly(ScheduledEventCommon.eWeekDays.Weekends);
                if (daystoschedule == "monday")
                    SysEvent[SysEvent.Count - 1].Recurrence.Weekly(ScheduledEventCommon.eWeekDays.Monday);
                if (daystoschedule == "tuesday")
                    SysEvent[SysEvent.Count - 1].Recurrence.Weekly(ScheduledEventCommon.eWeekDays.Tuesday);
                if (daystoschedule == "wednesday")
                    SysEvent[SysEvent.Count - 1].Recurrence.Weekly(ScheduledEventCommon.eWeekDays.Wednesday);
                if (daystoschedule == "thursday")
                    SysEvent[SysEvent.Count - 1].Recurrence.Weekly(ScheduledEventCommon.eWeekDays.Thursday);
                if (daystoschedule == "friday")
                    SysEvent[SysEvent.Count - 1].Recurrence.Weekly(ScheduledEventCommon.eWeekDays.Friday);
                if (daystoschedule == "saturday")
                    SysEvent[SysEvent.Count - 1].Recurrence.Weekly(ScheduledEventCommon.eWeekDays.Saturday);
                if (daystoschedule == "sunday")
                    SysEvent[SysEvent.Count - 1].Recurrence.Weekly(ScheduledEventCommon.eWeekDays.Sunday );
                
                SysEvent[SysEvent.Count - 1].UserCallBack += SysEvent_Trigger_Callback;
                SysEvent[SysEvent.Count - 1].Enable();
            }
            catch (Exception e)
            {//Email _email = new Email();
                ErrorLog.Error("Could not create Daily Event", e.Message);
                CrestronConsole.PrintLine($"ERROR - Could Not Create Daily Event - Message: {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject, $"ERROR - Daily Schedule Event: {e.Message} {e.Source} {e.StackTrace}");
            }
        }
        private static int DaysUntil(int startday, int targetday)
        {
            int daysuntil = ((targetday + 7) - startday) % 7;
           
            if (daysuntil == 0 )
                daysuntil = 7;
            
            return daysuntil;
        }
        /// <summary>
        /// Need to add what to do with the various timers for the events that we want to allow.
        /// </summary>
        /// <param name="schEvent"></param>
        /// <param name="type"></param>
        private static void SysEvent_Trigger_Callback(ScheduledEvent schEvent, ScheduledEventCommon.eCallbackReason type)
        {
            if (schEvent.Name.ToString().Length > 1 && schEvent.Name.ToString().Substring(0, schEvent.Name.ToString().Length - 1).Equals("autoshutdown", StringComparison.OrdinalIgnoreCase))
            {
                if (RoomSetup.RoomType.ToLower() == "huddle_room")
                {
                    ControlSystem.disp1.Power.PowerOff();
                }

                if (RoomSetup.RoomType.ToLower() == "evertz_room")
                {

                    string ParameterURL = "http://" + RoomSetup.Evertz.IpAddress + "/v.api/apis/EV/SET/parameter/217/1";

                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            HttpResponseMessage DelServerResponse = client.GetAsync(ParameterURL).Result;
                            DelServerResponse.EnsureSuccessStatusCode();
                        }
                        catch (Exception e)
                        {
                            ErrorLog.Error("Could not send Evertz Dante Reset Command", e.Message);
                            CrestronConsole.PrintLine($"ERROR - Could Not Send Evertz Dante Reseet Shutdown Command - Message: {e.Message}");
                            Email.SendEmail(RoomSetup.MailSubject, $"ERROR - Evertz Dante Reset Command: {e.Message} {e.Source} {e.StackTrace}");
                        }
                    }

                    if (RoomSetup.Touchpanel.TP_RoomType == "evertz_1")
                    {
                        if (RoomSetup.Display1 == "proj")
                        {
                            //if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                            ControlSystem.proj1.PowerOff();
                            EvertzHandler.tp_ClearButtonStatus(((uint)EvertzOutputs.out_Proj1).ToString());
                            ControlSystem.tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = true;
                            Mics.Mute("On");
                        }
                        else if (RoomSetup.Display1 == "tv")
                        {

                            //if (ControlSystem.disp1.Power.PowerOnFeedback.BoolValue)
                            ControlSystem.disp1.Power.PowerOff();
                            EvertzHandler.tp_ClearButtonStatus(((uint)EvertzOutputs.out_Proj1).ToString());
                            ControlSystem.tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = true;
                            Mics.Mute("On");
                        }
                        _ = Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj1).ToString(), ((uint)EvertzInputs.in_Blank).ToString());
                    }

                    if (RoomSetup.Touchpanel.TP_RoomType == "evertz_2")
                    {
                        if (RoomSetup.Display2 == "proj")
                        {
                            //if (ControlSystem.proj2.PowerOnFeedback.BoolValue)
                            ControlSystem.proj2.PowerOff();
                            EvertzHandler.tp_ClearButtonStatus(((uint)EvertzOutputs.out_Proj2).ToString());
                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = true;
                            Mics.Mute("On");
                        }
                        else if (RoomSetup.Display2 == "tv")
                        {

                            //if (ControlSystem.disp2.Power.PowerOnFeedback.BoolValue)
                            ControlSystem.disp2.Power.PowerOff();
                            EvertzHandler.tp_ClearButtonStatus(((uint)EvertzOutputs.out_Proj2).ToString());
                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = true;
                            Mics.Mute("On");
                        }
                        _ = Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj2).ToString(), ((uint)EvertzInputs.in_Blank).ToString());
                    }

                    if (RoomSetup.Touchpanel.TP_RoomType == "evertz_3")
                    {
                        if (RoomSetup.Display3 == "proj")
                        {
                            //if (ControlSystem.proj3.PowerOnFeedback.BoolValue)
                            ControlSystem.proj3.PowerOff();
                            EvertzHandler.tp_ClearButtonStatus(((uint)EvertzOutputs.out_Proj3).ToString());
                            ControlSystem.tp.BooleanInput[((uint)Join.btn3_PwrOnVis)].BoolValue = true;
                            Mics.Mute("On");
                        }
                        else if (RoomSetup.Display3 == "tv")
                        {

                            //if (ControlSystem.disp3.Power.PowerOnFeedback.BoolValue)
                            ControlSystem.disp3.Power.PowerOff();
                            EvertzHandler.tp_ClearButtonStatus(((uint)EvertzOutputs.out_Proj3).ToString());
                            ControlSystem.tp.BooleanInput[((uint)Join.btn3_PwrOnVis)].BoolValue = true;
                            Mics.Mute("On");
                        }
                        _ = Evertz.SetEvertzData(RoomSetup.Evertz.UDP_Server.ParametersToReport.param1, ((uint)EvertzOutputs.out_Proj3).ToString(), ((uint)EvertzInputs.in_Blank).ToString());
                    }
                }
            }

                if (schEvent.Name.ToString().Length > 1 && schEvent.Name.ToString().Substring(0, schEvent.Name.ToString().Length - 1).Equals("airmediareboot", StringComparison.OrdinalIgnoreCase))
            {
                ControlSystem.am3200.AirMedia.DeviceReboot();
            }
            
            if (schEvent.Name.ToString().Length > 1 && schEvent.Name.ToString().Substring(0, schEvent.Name.ToString().Length - 1).Equals("tp_screensaver_off", StringComparison.OrdinalIgnoreCase))
            {
                ControlSystem.tp.ExtenderScreenSaverReservedSigs.ScreensaverOn.BoolValue = false;
                ControlSystem.tp.ExtenderScreenSaverReservedSigs.ScreensaverOff.BoolValue = true;
                SS_Active = false;
            }

            if (schEvent.Name.ToString().Length > 1 && schEvent.Name.ToString().Substring(0, schEvent.Name.ToString().Length - 1).Equals("tp_screensaver_on", StringComparison.OrdinalIgnoreCase))
            {
                ControlSystem.tp.ExtenderScreenSaverReservedSigs.ScreensaverOn.BoolValue = true;
                ControlSystem.tp.ExtenderScreenSaverReservedSigs.ScreensaverOff.BoolValue = false;
                SS_Active = true;
            }

            if (schEvent.Name.ToString().Length > 1 && schEvent.Name.ToString().Substring(0, schEvent.Name.ToString().Length - 1).Equals("tp_prox_off", StringComparison.OrdinalIgnoreCase))
            {
                
                Prox_Active = false;
            }

            if (schEvent.Name.ToString().Length > 1 && schEvent.Name.ToString().Substring(0, schEvent.Name.ToString().Length - 1).Equals("tp_prox_on", StringComparison.OrdinalIgnoreCase))
            {
                
                Prox_Active = true;
            }

            
        }

        public static void Alert_Timer(string name, int delay, string ex)
        {
           try
            {
                if (!errorCounts.ContainsKey(name))
                {
                    errorCounts[name] = 0;
                }
                errorCounts[name] ++;

                CTimer alertTimer = new CTimer(Alert_Timer_Callback, new { Name = name, Exception = ex, Delay = delay }, Convert.ToInt64(delay) * 60000);
               
            }
            catch (Exception e)
            {
                ErrorLog.Error("Could not create Alert Timer", e.Message);
                CrestronConsole.PrintLine($"ERROR - Could Not Create Alert Timer - Message: {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject, $"ERROR - Alert Timer: {e.Message} {e.Source} {e.StackTrace}");
            }
        }
    
        /// <summary>
        /// Need to add what to do with the various timers for the alerts that we want to track.
        /// </summary>
        /// <param name="obj"></param>

        private static void Alert_Timer_Callback(object obj)
        {
            var data = (dynamic)obj;
            string name = data.Name;
            string ex = data.Exception;
            int delay = data.Delay;
                                    
            bool errorExists = CheckOnlineError(name);
            if (errorExists && errorCounts[name] <= RoomSetup.Timeouts.ErrorThreshold)
            {
                Email.SendEmail(RoomSetup.MailSubject, ex);
                Alert_Timer(name, delay, ex);
            }
            else if (!errorExists)
            {
                errorCounts[name] = 0;
            }
        }
        private static bool CheckOnlineError(string type)
        {
            if (type == "touchpanel")
            {
                if (ControlSystem.tp.IsOnline)
                {   
                    return false;
                }
                else if (!ControlSystem.tp.IsOnline)
                {   
                    return true;
                }
            }
            if (type == "hdmd")
            {
                if (ControlSystem.hdmd.IsOnline)
                {
                    return false;
                }
                else if (!ControlSystem.hdmd.IsOnline)
                {
                    return true;
                }
            }
            if (type == "airmedia")
            {
                if (ControlSystem.am3200.IsOnline)
                {
                    return false;
                }
                else if (!ControlSystem.am3200.IsOnline)
                {
                    return true;
                }
            }
            if (type == "tv")
            {
                if (ControlSystem.disp1.IsOnline)
                {
                    return false;
                }
                else if (!ControlSystem.disp1.IsOnline)
                {
                    return true;
                }
            }
            if (type == "evertz")
            {
                return true;
            }
            if (type == "sony")
            {
                return true;
            }
            if (type == "microphone")
            {
                return true;
            }
            if (type == "nvx")
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        
    }
}