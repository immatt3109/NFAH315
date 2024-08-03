namespace NFAHRooms
{
    public static class Constants
    {
        public const string http = "http://";
        public const string command = "/command/";
        public const string ptzf = "ptzf.cgi?move=";
        public const string preset = "presetposition.cgi?PresetCall=";
        public const string presetset = "presetposition.cgi?PresetSet=";
        public const string presetfolder = "/preset/";
        public const string localFolderPath = "/html/Icons/";
        public const string rtsp = "rtsp://";
        public const string rtspStream = ":554/video1";
        public const string PresetHTMLFolder = "/Icons/presetimg";
        public const string PresetFileSuffix = ".jpg";
        public const string AICommand = "/analytics/ptzautoframing.cgi?";
        public const int ShurePort = 2202;
        public const int TCCPort = 45;
        public const string TCCMuteOn = "{ \"audio\": { \"mute\": true } }";
        public const string TCCMuteOff = "{ \"audio\": { \"mute\": false } }";
        public const string JSONSeparator = "\r\n";
    }
}
