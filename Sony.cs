using Crestron.SimplSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Crestron.SimplSharp.CrestronIO;

namespace NFAHRooms
{
    public static class Sony
    {
        public static Dictionary<string, string> urls = new Dictionary<string, string>();
        
        public static void MakeDictionary()
        {
            string movespeed = RoomSetup.SonyCameras.CommonProperties.Pan;
            string zoomspeed = RoomSetup.SonyCameras.CommonProperties.Zoom;

            urls.Add("Teach_MoveUP", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.ptzf + "up," + movespeed);
            urls.Add("Teach_MoveDOWN", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.ptzf + "down," + movespeed);
            urls.Add("Teach_MoveLEFT", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.ptzf + "left," + movespeed);
            urls.Add("Teach_MoveRIGHT", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.ptzf + "right," + movespeed);
            urls.Add("Teach_MoveUPLEFT", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.ptzf + "up-left," + movespeed);
            urls.Add("Teach_MoveUPRIGHT", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.ptzf + "up-right," + movespeed);
            urls.Add("Teach_MoveDOWNLEFT", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.ptzf + "down-left," + movespeed);
            urls.Add("Teach_MoveDOWNRIGHT", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.ptzf + "down-right," + movespeed);
            urls.Add("Teach_ZoomIN", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.ptzf + "tele," + zoomspeed);
            urls.Add("Teach_ZoomOUT", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.ptzf + "wide," + zoomspeed);
            urls.Add("Teach_StopPTZ", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.ptzf + "stop,pantilt");
            urls.Add("Teach_StopZoom", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.ptzf + "stop,zoom");
            urls.Add("Teach_Preset1", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.preset + "1");
            urls.Add("Teach_Preset2", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.preset + "2");
            urls.Add("Teach_Preset3", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.preset + "3");
            urls.Add("AI_ON", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.AICommand + "PtzAutoFraming=on");
            urls.Add("AI_OFF", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.AICommand + "PtzAutoFraming=off");
            urls.Add("Lay_Ctr", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.AICommand + "PtzAutoFramingAdjustObjectHorizontal=center");
            urls.Add("Lay_Left", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.AICommand + "PtzAutoFramingAdjustObjectHorizontal=left1");
            urls.Add("Lay_Right", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.AICommand + "PtzAutoFramingAdjustObjectHorizontal=right1");
            urls.Add("Lay_Full", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.AICommand + "PtzAutoFramingShotMode=fullbody");
            urls.Add("Lay_Top", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.AICommand + "PtzAutoFramingShotMode=waist");
            urls.Add("Lay_Head", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.AICommand + "PtzAutoFramingShotMode=closeup");
            
            urls.Add("Student_MoveUP", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.ptzf + "up," + movespeed);
            urls.Add("Student_MoveDOWN", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.ptzf + "down," + movespeed);
            urls.Add("Student_MoveLEFT", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.ptzf + "left," + movespeed);
            urls.Add("Student_MoveRIGHT", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.ptzf + "right," + movespeed);
            urls.Add("Student_MoveUPLEFT", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.ptzf + "up-left," + movespeed);
            urls.Add("Student_MoveUPRIGHT", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.ptzf + "up-right," + movespeed);
            urls.Add("Student_MoveDOWNLEFT", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.ptzf + "down-left," + movespeed);
            urls.Add("Student_MoveDOWNRIGHT", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.ptzf + "down-right," + movespeed);
            urls.Add("Student_ZoomIN", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.ptzf + "tele," + zoomspeed);
            urls.Add("Student_ZoomOUT", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.ptzf + "wide," + zoomspeed);
            urls.Add("Student_StopPTZ", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.ptzf + "stop,pantilt");
            urls.Add("Student_StopZoom", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.ptzf + "stop,zoom");
            urls.Add("Student_Preset1", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.preset + "1");
            urls.Add("Student_Preset2", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.preset + "2");
            urls.Add("Student_Preset3", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.preset + "3");

            urls.Add("TPS1_Set", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.presetset + "1,TPS1,On");
            urls.Add("TPS2_Set", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.presetset + "2,TPS2,On");
            urls.Add("TPS3_Set", Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.command + Constants.presetset + "3,TPS3,On");

            urls.Add("SPS1_Set", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.presetset + "1,SPS1,On");
            urls.Add("SPS2_Set", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.presetset + "2,SPS2,On");
            urls.Add("SPS3_Set", Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.command + Constants.presetset + "3,SPS3,On");
        }
        
        public static async void TimerCallback(object state)
        {  
            if (state.ToString() == "TPS1")
            {
                        
                _ = SendRequest(urls["TPS1_Set"]);
                await DownloadThumbnail(state.ToString());
            }
            else if (state.ToString() == "TPS2")
            {                
                _ = SendRequest(urls["TPS2_Set"]);
                await DownloadThumbnail(state.ToString());
            }
            else if (state.ToString() == "TPS3")
            {
                _ = SendRequest(urls["TPS3_Set"]);
                await DownloadThumbnail(state.ToString());
            }
            else if (state.ToString() == "SPS1")
            {
                _ = SendRequest(urls["SPS1_Set"]);
                await DownloadThumbnail(state.ToString());
            }
            else if (state.ToString() == "SPS2")
            {
                _ = SendRequest(urls["SPS2_Set"]);
                await DownloadThumbnail(state.ToString());
            }
            else if (state.ToString() == "SPS3")
            {
                _ = SendRequest(urls["SPS3_Set"]);
                await DownloadThumbnail(state.ToString());
            }
            else
            {
                CrestronConsole.PrintLine("Error in Camera Preset Timer Callback");
                ErrorLog.Error("Error in Camera Preset Timer Callback");
            }
        }

        public static async Task DownloadThumbnail(string Preset)
        {
            string fileName = null;
            string localFilePath = null;
            string TeachfileUrl = Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.presetfolder;
            string StudfileUrl = Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.presetfolder;
            byte[] fileData = null;

            try
            {
                await Task.Delay(5000);
                if (Preset != null)
                    switch (Preset)
                    {
                        case "TPS1":
                            fileName = "presetimg1.jpg";
                            break;
                        case "TPS2":
                            fileName = "presetimg2.jpg";
                            break;
                        case "TPS3":
                            fileName = "presetimg3.jpg";
                            break;
                        case "SPS1":
                            fileName = "presetimg4.jpg";
                            break;
                        case "SPS2":
                            fileName = "presetimg5.jpg";
                            break;
                        case "SPS3":
                            fileName = "presetimg6.jpg";
                            break;
                        default:
                            break;
                    }
//need to fix the file names here for getting them off camera and placing them into the correct folder with the correct filename.
                if (!Directory.Exists(Constants.localFolderPath))
                    Directory.CreateDirectory(Constants.localFolderPath);
                
                if (Preset.Contains("TPS"))
                    TeachfileUrl += fileName;
                else if (Preset.Contains("SPS"))
                    StudfileUrl += fileName;
                
                HttpClientHandler handler = new HttpClientHandler
                {
                    Credentials = new NetworkCredential(RoomSetup.SonyCameras.CommonProperties.Username, RoomSetup.SonyCameras.CommonProperties.Password),
                };

                using (HttpClient client = new HttpClient(handler))
                {
                    if (Preset.Contains("TPS"))
                    {
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, TeachfileUrl);
                        request.Headers.Referrer = new Uri(Constants.http + RoomSetup.SonyCameras.CommonProperties.TeacherIP + "/");
                        fileData = await client.GetByteArrayAsync(TeachfileUrl);
                    }
                    else if (Preset.Contains("SPS"))
                    {
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, StudfileUrl);
                        request.Headers.Referrer = new Uri(Constants.http + RoomSetup.SonyCameras.CommonProperties.StudentIP + "/");
                        fileData = await client.GetByteArrayAsync(StudfileUrl);
                    }
                    
                    localFilePath = Path.Combine(Constants.localFolderPath, fileName);

                    System.IO.File.WriteAllBytes(localFilePath, fileData);
                    
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine($"Error Downlaod Thumbnail: {e.Message} Stacktrace: {e.StackTrace}");
                ErrorLog.Error("Error: {0}", e.Message);
            }
        }

        public static async Task SendRequest(string url)
        {

            HttpClientHandler handler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential(RoomSetup.SonyCameras.CommonProperties.Username, RoomSetup.SonyCameras.CommonProperties.Password)
            };
            
            using (HttpClient client = new HttpClient(handler))
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                {
                    

                    string referrer = "http://" + RoomSetup.SonyCameras.CommonProperties.TeacherIP + "/";
                    request.Headers.Referrer = new Uri(referrer);

                    
                }
                else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                {
                    string referrer = "http://" + RoomSetup.SonyCameras.CommonProperties.StudentIP + "/";
                    request.Headers.Referrer = new Uri(referrer);
                }
                                
                HttpResponseMessage response = await client.SendAsync(request);
               
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    CrestronConsole.PrintLine("Unauthorized HTTP Request");
                    ErrorLog.Error("Unauthorized HTTP Request");
                }
                else if (response.IsSuccessStatusCode)
                {   
                    _ = await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
