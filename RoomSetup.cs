using Newtonsoft.Json;
using System.Collections.Generic;
using Crestron.SimplSharp.CrestronIO;
using Crestron.SimplSharp;
using System;

namespace NFAHRooms
{       
    public class RoomSetup
    {
        private static string json;

        [JsonProperty("room_type")]
        public string RoomType { get; set; }

        [JsonProperty("mail_subject")]
        public string MailSubject { get; set; }
        [JsonProperty("Crestron")]
        public CrestronConfiguration Crestron { get; set; }
        [JsonProperty("Touchpanel")]
        public TouchpanelConfiguration Touchpanel { get; set; }
        [JsonProperty("evertz")]
        public EvertzConfiguration Evertz { get; set; }

        [JsonProperty("sony_cameras")]
        public SonyCameraConfig SonyCameras { get; set; }

        [JsonProperty("microphones")]
        public List<Microphone> Microphones { get; set; }

        [JsonProperty("huddle_room_settings")]
        public HuddleRoomSettings HuddleRoomSettings { get; set; }

        [JsonProperty("DailyEvents")]
        public List<DailyEvent> DailyEvents { get; set; }

        [JsonProperty("timeouts")]
        public TimeoutConfig Timeouts { get; set; }

        [JsonProperty("nvx_settings")]
        public NvxSettings NvxSettings { get; set; }

        public static RoomSetup LoadRoomSetup(string filepath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filepath))
                    return null;

                using (StreamReader sr = new StreamReader(filepath, System.Text.Encoding.Default))

                    json = sr.ReadToEnd();
                
                return JsonConvert.DeserializeObject<RoomSetup>(json);
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
        [JsonProperty("Processor_IP")]
        public string ProcessorIp { get; set; }
        [JsonProperty("Host_Name")]
        public string HostName { get; set; }
        [JsonProperty("Subnet")]
        public string Subnet { get; set; }
        [JsonProperty("DNS1")]
        public string Dns1 { get; set; }
        [JsonProperty("DNS2")]
        public string Dns2 { get; set; }
        [JsonProperty("Gateway")]
        public string Gateway { get; set; }
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
    }
    public class EvertzConfiguration
    {
        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("hdmi_output_default_source")]
        public Dictionary<string, string> HdmiOutputDefaultSource { get; set; }

        [JsonProperty("UDP_server")]
        public UDPServer UDP_Server { get; set; }
    }

    public class UDPServer
    {
        [JsonProperty("udp_port")]
        public int UdpPort { get; set; }

        [JsonProperty("parameters_to_report")]
        public List<string> ParametersToReport { get; set; }

        [JsonProperty("allowed_ip_address")]
        public string AllowedIpAddress { get; set; }

        [JsonProperty("read_buffer_size")]
        public int ReadBufferSize { get; set; }

        [JsonProperty("server_name")]
        public string ServerName { get; set; }
    }

    public class SonyCameraConfig
    {
        [JsonProperty("common_properties")]
        public SonyCameraCommonProperties CommonProperties { get; set; }

        [JsonProperty("cameras")]
        public List<SonyCamera> Cameras { get; set; }
    }

    public class SonyCameraCommonProperties
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("cam_url")]
        public string  CamURL { get; set; }

        [JsonProperty("image_name")]
        public string IMGName { get; set; }
        
        [JsonProperty("storage_location")]
        public string StorageLocation { get; set; }

        [JsonProperty("pan")]
        public string Pan { get; set; }

        [JsonProperty("zoom")]
        public string Zoom { get; set; }
    }

    public class SonyCamera
    {
        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }
    }
       
    public class Microphone
    {
        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }
    }

    public class HuddleRoomSettings
    {
        [JsonProperty("default_video_output")]
        public int DefaultVideoOutput { get; set; }

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
    }
}
