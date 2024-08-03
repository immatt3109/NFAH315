using Crestron.SimplSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NFAHRooms
{
    public static class Mics
    {
        public static Dictionary<string, string> tcpCommands = new Dictionary<string, string>();

        public static void MicList()
        {
            List<Microphone>  Microphones = RoomSetup.Microphones;

            ShureMutes();           
        }

        private static void ShureMutes() 
        {
            tcpCommands.Add("MXA_mute_on", "< SET DEVICE_AUDIO_MUTE ON >");
            tcpCommands.Add("MXA_mute_off", "< SET DEVICE_AUDIO_MUTE OFF >");
            tcpCommands.Add("ULXD_1_mute_on", "< SET 1 AUDIO_MUTE ON >");
            tcpCommands.Add("ULXD_1_mute_off", "< SET 1 AUDIO_MUTE OFF >");
            tcpCommands.Add("ULXD_2_mute_on", "< SET 2 AUDIO_MUTE ON >");
            tcpCommands.Add("ULXD_2_mute_off", "< SET 2 AUDIO_MUTE OFF >");
            tcpCommands.Add("ULXD_3_mute_on", "< SET 3 AUDIO_MUTE ON >");
            tcpCommands.Add("ULXD_3_mute_off", "< SET 3 AUDIO_MUTE OFF >");
            tcpCommands.Add("ULXD_4_mute_on", "< SET 4 AUDIO_MUTE ON >");
            tcpCommands.Add("ULXD_4_mute_off", "< SET 4 AUDIO_MUTE OFF >");
        }

        public static async void Mute(string value)
        {
            if (value.ToLower() == "on")
            {
                MuteOn();
                await TCC2Mute("on");
            }
            else if (value.ToLower() == "off")
            {
                MuteOff();
                await TCC2Mute("off");
            }
            else
            {
                CrestronConsole.PrintLine("Invalid mute command");
            }
        }
        private static void MuteOn()
        {
            Mics.SendCommand(RoomSetup.Microphones, "MXA", Mics.tcpCommands["MXA_mute_on"]);
            Mics.SendCommand(RoomSetup.Microphones, "ULXD", Mics.tcpCommands["ULXD_1_mute_on"]);
            Mics.SendCommand(RoomSetup.Microphones, "ULXD", Mics.tcpCommands["ULXD_2_mute_on"]);
            Mics.SendCommand(RoomSetup.Microphones, "ULXD", Mics.tcpCommands["ULXD_3_mute_on"]);
            Mics.SendCommand(RoomSetup.Microphones, "ULXD", Mics.tcpCommands["ULXD_4_mute_on"]);

            
        }

        private static void MuteOff()
        {
            Mics.SendCommand(RoomSetup.Microphones, "MXA", Mics.tcpCommands["MXA_mute_off"]);
            Mics.SendCommand(RoomSetup.Microphones, "ULXD", Mics.tcpCommands["ULXD_1_mute_off"]);
            Mics.SendCommand(RoomSetup.Microphones, "ULXD", Mics.tcpCommands["ULXD_2_mute_off"]);
            Mics.SendCommand(RoomSetup.Microphones, "ULXD", Mics.tcpCommands["ULXD_3_mute_off"]);
            Mics.SendCommand(RoomSetup.Microphones, "ULXD", Mics.tcpCommands["ULXD_4_mute_off"]);
        }

        private static void SendCommand(List<Microphone> microphones, string targetType, string command)
        { 
            foreach (Microphone mic in microphones)
            { 
                if (mic.Type.ToLower() == targetType.ToLower())
                {
                    try
                    {
                        using (TcpClient client = new TcpClient(mic.IpAddress, Constants.ShurePort))
                        {
                            
                            using (NetworkStream stream = client.GetStream())
                            {
                                byte[] data = Encoding.ASCII.GetBytes(command);
                                stream.Write(data, 0, data.Length);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        CrestronConsole.PrintLine("Error sending command to {0}: {1}", mic.IpAddress, e.Message);
                    }
                }
            }
        }

        private static async Task TCC2Mute(string Mute)
        {
            string messageWithSeparator = null;
            try
            {
                List<Microphone> Microphones = RoomSetup.Microphones;

                foreach (Microphone mic in Microphones)
                {
                    if (mic.Type.ToLower() == "tcc")
                    {

                        
                        if (Mute.ToLower() == "on")
                        {
                            messageWithSeparator = Constants.TCCMuteOn + Constants.JSONSeparator;
                        }
                        else if (Mute.ToLower() == "off")
                        {
                            messageWithSeparator = Constants.TCCMuteOff + Constants.JSONSeparator;
                        }
                        else
                        {
                            CrestronConsole.PrintLine("Invalid mute command");
                            return;
                        }
            
                        using (TcpClient client = new TcpClient())
                        {
                            await client.ConnectAsync(mic.IpAddress, Constants.TCCPort);
                            NetworkStream stream = client.GetStream();

                            byte[] data = Encoding.UTF8.GetBytes(messageWithSeparator);

                            await stream.WriteAsync(data, 0, data.Length);
                        }
                    }
                }
            }
            catch (Crestron.SimplSharp.SocketException e)
            {
                Console.WriteLine($"Socket error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

        }         
    }
}
