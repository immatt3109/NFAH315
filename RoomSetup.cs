using Newtonsoft.Json;
using System.Collections.Generic;
using Crestron.SimplSharp.CrestronIO;
using Crestron.SimplSharp;
using System;
using static Crestron.SimplSharpPro.DM.Audio;
using System.Linq;

namespace NFAHRooms
{       
    public class RoomSetup
    {
        private static string json;

        [JsonProperty("room_type")]
        public static string RoomType { get; set; }
        [JsonProperty("display_1")]
        public static string Display1 { get; set; }
        [JsonProperty("display_2")]
        public static string Display2 { get; set; }
        [JsonProperty("display_3")]
        public static string Display3 { get; set; }
        [JsonProperty("mail_subject")]
        public static string MailSubject { get; set; }
        [JsonProperty("Crestron")]
        public static CrestronConfiguration Crestron { get; set; }
        [JsonProperty("Touchpanel")]
        public static TouchpanelConfiguration Touchpanel { get; set; }
        [JsonProperty("evertz")]
        public static EvertzConfiguration Evertz { get; set; }

        [JsonProperty("sony_cameras")]
        public static SonyCameraConfig SonyCameras { get; set; }

        [JsonProperty("microphones")]
        public static List<Microphone> Microphones { get; set; }

        [JsonProperty("huddle_room_settings")]
        public static HuddleRoomSettings HuddleRoomSettings { get; set; }

        [JsonProperty("DailyEvents")]
        public static List<DailyEvent> DailyEvents { get; set; }

        [JsonProperty("timeouts")]
        public static TimeoutConfig Timeouts { get; set; }

        [JsonProperty("nvx_settings")]
        public static NvxSettings NvxSettings { get; set; }

        public static RoomSetup LoadRoomSetup(string filepath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filepath))
                    return null;

                using (StreamReader sr = new StreamReader(filepath, System.Text.Encoding.Default))

                    json = sr.ReadToEnd();

                RoomSetup roomSetup = JsonConvert.DeserializeObject<RoomSetup>(json);

                NvxSettings?.PopulateDictionaries();

                return roomSetup;

                //return JsonConvert.DeserializeObject<RoomSetup>(json);
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error reading room_setup.json: {0}", e.Message);
                CrestronConsole.PrintLine("Error reading room_setup.json: {0}", e.Message);
                return null;
            }
        }
    }

    public class CrestronConfiguration
    {
        [JsonProperty("SNTP_server")]
        public string SntpServer { get; set; }
        [JsonProperty("Timezone_ID")]
        public string TimezoneId { get; set; }
        [JsonProperty("Username")]
        public string Username { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
    }
    public class TouchpanelConfiguration
    {
        [JsonProperty("Room_Text")]
        public string RoomText { get; set; }
        [JsonProperty("Proximity")]
        public string Proximity { get; set; }
        [JsonProperty("ScreenSaver")]
        public string ScreenSaver{ get; set; }
        [JsonProperty ("Standby_Timeout")]
        public ushort StandbyTimeout { get; set; }
        [JsonProperty("Image_URL")]
        public string ImageUrl { get; set; }
        [JsonProperty("Room_Type")]
        public string TP_RoomType { get; set; }
    }
    public class EvertzConfiguration
    {
        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("hdmi_output_default_source")]
        public DefEvertzOuts DefEvertzOut { get; set; }

        [JsonProperty("UDP_server")]
        public UDPServer UDP_Server { get; set; }
    }
    public class DefEvertzOuts
    {
        [JsonProperty("out_1")]
        public int Out1 { get; set; }
        [JsonProperty("out_2")]
        public int Out2 { get; set; }
        [JsonProperty("out_3")]
        public int Out3 { get; set; }
        [JsonProperty("out_4")]
        public int Out4 { get; set; }
    }

    public class UDPServer
    {
        [JsonProperty("udp_port")]
        public int UdpPort { get; set; }

        [JsonProperty("parameters_to_report")]
        public ParametersToReport ParametersToReport { get; set; }

        [JsonProperty("allowed_ip_address")]
        public string AllowedIpAddress { get; set; }

        [JsonProperty("read_buffer_size")]
        public int ReadBufferSize { get; set; }

        [JsonProperty("server_name")]
        public string ServerName { get; set; }
    }
    
    public class ParametersToReport
    {
        [JsonProperty("param1")]
        public string param1 { get; set; }
        [JsonProperty("param2")]
        public string param2 { get; set; }
    }

    public class SonyCameraConfig
    {
        [JsonProperty("common_properties")]
        public SonyCameraCommonProperties CommonProperties { get; set; }
    }

    public class SonyCameraCommonProperties
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
                
        [JsonProperty("pan")]
        public string Pan { get; set; }

        [JsonProperty("zoom")]
        public string Zoom { get; set; }
        [JsonProperty("teacher_IP")]
        public string TeacherIP { get; set; }
        [JsonProperty("student_IP")]
        public string StudentIP { get; set; }
    }
    public class Microphone
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }
    }

    public class HuddleRoomSettings
    {
        [JsonProperty("frontpanel_lock")]
        public string FrontpanelLock { get; set; }

        [JsonProperty("frontpanel_led")]
        public string FrontpanelLed { get; set; }

        [JsonProperty("autoroute")]
        public string Autoroute { get; set; }
    }

    public class DailyEvent
    {
        [JsonProperty("EventName")]
        public string EventName { get; set; }

        [JsonProperty("EventTime")]
        public DateTime EventTime { get; set; }

        [JsonProperty("Daily")]
        public bool Daily { get; set; }

        [JsonProperty("WeekDaysOnly")]
        public bool WeekDaysOnly { get; set; }

        [JsonProperty("WeekendsOnly")]
        public bool WeekendsOnly { get; set; }

        [JsonProperty("Monday")]
        public bool Monday { get; set; }

        [JsonProperty("Tuesday")]
        public bool Tuesday { get; set; }

        [JsonProperty("Wednesday")]
        public bool Wednesday { get; set; }

        [JsonProperty("Thursday")]
        public bool Thursday { get; set; }

        [JsonProperty("Friday")]
        public bool Friday { get; set; }

        [JsonProperty("Saturday")]
        public bool Saturday { get; set; }

        [JsonProperty("Sunday")]
        public bool Sunday { get; set; }
    }

    public class TimeoutConfig
    {
        [JsonProperty("error_check_delay")]
        public int ErrorCheckDelay { get; set; }

        [JsonProperty("error_threshold")]
        public int ErrorThreshold { get; set; }
    }
    
    public class NvxSettings
    {
        [JsonProperty("dm_server_processor_ip")]
        public string DmServerProcessorIp { get; set; }

        [JsonProperty("assigned_ipid")]
        public string AssignedIpid { get; set; }
        [JsonProperty("inputs")]
        public List<NvxInput> Inputs { get; set; }
        [JsonProperty("outputs")]
        public List<NvxOutput> Outputs { get; set; }

        [JsonIgnore]
        public Dictionary<string, NvxInput> InputDictionary { get; private set; }

        [JsonIgnore]
        public Dictionary<string, NvxOutput> OutputDictionary { get; private set; }
        public void PopulateDictionaries()
        {
            try
            {
                InputDictionary = new Dictionary<string, NvxInput>();
                OutputDictionary = new Dictionary<string, NvxOutput>();

                if (Inputs != null)
                {
                    foreach (var input in Inputs)
                    {
                        InputDictionary[input.InputProg] = input;
                        CrestronConsole.PrintLine($"Input Prog: {input.InputProg} Input NVX: {input.InputNVX}");
                    }
                }

                if (Outputs != null)
                {
                    foreach (var output in Outputs)
                    {
                        OutputDictionary[output.OutputProg] = output;
                        CrestronConsole.PrintLine($"Output Prog: {output.OutputProg} Output NVX: {output.OutputNVX}");

                    }
                }

                
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error in PopulateDictionaries: {0}", e.Message);
            }
        }
    }
    public class NvxInput
    {
        public string InputProg { get; set; }
        public string InputNVX { get; set; }
    }
    public class NvxOutput
    {
        public string OutputProg { get; set; }
        public string OutputNVX { get; set; }
    }
    


}
