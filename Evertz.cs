using System;
using Crestron.SimplSharp;                          	// For Basic SIMPL# Classes
using Crestron.SimplSharpPro;                       	// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.CrestronThread;        	// For Threading
using System.Net.Http;                              	// For HTTP Client
using System.Threading.Tasks;                        	// For Tasking
using Crestron.SimplSharp.CrestronSockets;
using Newtonsoft.Json;
using System.Collections.Generic;
using Crestron.SimplSharpPro.UI;

namespace NFAHRooms
{
    public static class Evertz
    {
        private static Crestron.SimplSharp.CrestronSockets.UDPServer ResponseServer;

        public class EvertzResponse
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Value { get; set; }
        }

        public class UDPResponse
        {
            [JsonProperty("jsonrpc")]
            public string Jsonrpc { get; set; }
            [JsonProperty("method")]
            public string Method { get; set; }
            [JsonProperty("params")]
            public List <Params> UDPParameters { get; set; }
        }

        public class Params
        {
            [JsonProperty("data")]
            public string Data { get; set; }
            [JsonProperty("varid")]
            public string Varid { get; set; }
        }

        private class EvertzServer
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("notify")]
            public Notify Notify { get; set; }
        }

        private class Notify
        {
            [JsonProperty("ip")]
            public string Ip { get; set; }

            [JsonProperty("parameters")]
            public List<string> Parameters { get; set; }

            [JsonProperty("port")]
            public int Port { get; set; }

            [JsonProperty("protocol")]
            public string Protocol { get; set; }
        }

        private class Data
        {
            [JsonProperty("server")]
            public EvertzServer Server { get; set; }
        }

        private class ServerResponse
        {
            [JsonProperty("data")]
            public Data Data { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("error")]
            public string Error { get; set; }
        }

        public static async Task<string> SetEvertzData(string Parameter, string Index, string Value)

        {
                       
            string url = "http://" + RoomSetup.Evertz.IpAddress+ "/v.api/apis/EV/SET/parameter/" + Parameter + "." + Index + "/" + Value;

            CrestronConsole.PrintLine("URL: {0}", url);

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                catch (HttpRequestException e)
                {
                    ErrorLog.Error("Error in GetEvertzData: {0}", e.Message);
                    return null;
                }
            }
            

        }

        public static async Task<string> GetEvertzData(string Parameter, string Index)
        {
            string url = "http://" + RoomSetup.Evertz.IpAddress + "/v.api/apis/EV/GET/parameter/" + Parameter + "." + Index;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    CrestronConsole.PrintLine("GetEvertzData ResponseBody: {0}", responseBody);
                    return responseBody;
                }
                catch (HttpRequestException e)
                {
                    ErrorLog.Error("Error in GetEvertzData: {0}", e.Message);
                    return null;
                }
            }
        }
        private static void ParseResponse(string response)
        {
            EvertzResponse jsonobj = Newtonsoft.Json.JsonConvert.DeserializeObject<EvertzResponse>(response);
            CrestronConsole.PrintLine($"ID: {jsonobj.ID} Value: {jsonobj.Value}");
        }

        private static void NewEvertzServer()
        {

            string DelServerURL = "http://" + RoomSetup.Evertz.IpAddress + "/v.api/apis/EV/SERVERDEL/server/1";
            CrestronConsole.PrintLine("DelServerURL: {0}", DelServerURL);

            string ProcessorIP = CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_CURRENT_IP_ADDRESS, 0);
            CrestronConsole.PrintLine("ProcessorIP: {0}", ProcessorIP);

            string ServerURL = "http://" + RoomSetup.Evertz.IpAddress + "/v.api/apis/EV/SERVERADD/server/" + ProcessorIP + "/" + RoomSetup.Evertz.UDP_Server.ServerName + "/" + RoomSetup.Evertz.UDP_Server.UdpPort + "/udp";
            CrestronConsole.PrintLine("ServerURL: {0}", ServerURL);

            string ParameterURL = "http://" + RoomSetup.Evertz.IpAddress + "/v.api/apis/EV/NOTIFYADD/parameter/1/" + RoomSetup.Evertz.UDP_Server.ParametersToReport.param1 ;
            CrestronConsole.PrintLine("ParameterURL: {0}", ParameterURL);

            string ParameterURL2 = "http://" + RoomSetup.Evertz.IpAddress + "/v.api/apis/EV/NOTIFYADD/parameter/1/" + RoomSetup.Evertz.UDP_Server.ParametersToReport.param2 ;
            CrestronConsole.PrintLine("ParameterURL2: {0}", ParameterURL2);


            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage DelServerResponse = client.GetAsync(DelServerURL).Result;
                    DelServerResponse.EnsureSuccessStatusCode();

                    HttpResponseMessage URLresponse = client.GetAsync(ServerURL).Result;
                    URLresponse.EnsureSuccessStatusCode();

                    if (URLresponse.IsSuccessStatusCode)
                    {
                        string responseBody = URLresponse.Content.ReadAsStringAsync().Result;
                        ServerResponse jsonobj = JsonConvert.DeserializeObject<ServerResponse>(responseBody);
                        CrestronConsole.PrintLine("ServerResponse: {0}", responseBody);

                        if (jsonobj.Status == "success")
                        {
                            HttpResponseMessage ParameterResponse = client.GetAsync(ParameterURL).Result;
                            ParameterResponse.EnsureSuccessStatusCode();

                            ParameterResponse = client.GetAsync(ParameterURL2).Result;
                            ParameterResponse.EnsureSuccessStatusCode();  ///????


                            if (ParameterResponse.IsSuccessStatusCode)
                            {
                                string ParameterBody = ParameterResponse.Content.ReadAsStringAsync().Result;
                                ServerResponse jsonobj2 = JsonConvert.DeserializeObject<ServerResponse>(ParameterBody);
                                CrestronConsole.PrintLine("ParameterResponse: {0}", ParameterBody);
                                CrestronConsole.PrintLine("StatusResponse: {0}", jsonobj2.Status);
                                CrestronConsole.PrintLine("ErrorResponse: {0}", jsonobj2.Error);
                                if (jsonobj2.Status == "success")
                                {
                                    ResponseServer = new Crestron.SimplSharp.CrestronSockets.UDPServer(RoomSetup.Evertz.IpAddress, RoomSetup.Evertz.UDP_Server.UdpPort, RoomSetup.Evertz.UDP_Server.ReadBufferSize);
                                    ResponseServer.EnableUDPServer();
                                    ResponseServer.ReceiveDataAsync(UDPServerReceiveCallback);
                                   

                                }
                                if (jsonobj2.Error != null)
                                {
                                    CrestronConsole.PrintLine("Cannot add Evertz Server Paramaters: {0}", jsonobj2.Error);
                                    ErrorLog.Error("Cannot add Evertz Server Paramaters: {0}", jsonobj2.Error);
                                }
                            }
                        }
                        else if (jsonobj.Error != null)
                        {
                            CrestronConsole.PrintLine("Cannot add Evertz Server: {0}", jsonobj.Error);
                            ErrorLog.Error("Cannot add Evertz Server: {0}", jsonobj.Error);
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    ErrorLog.Error("Error in GetEvertzData: {0}", e.Message);
                    CrestronConsole.PrintLine("Error in GetEvertzData: {0}", e.Message);
                }
            }
        }

        public static void Initialize()
        {
            try
            {
               NewEvertzServer();
               CrestronConsole.PrintLine("Evertz Server Initialized");
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in InitializeSystem: {0}", e.Message);
            }
        }

        private static void UDPServerReceiveCallback(Crestron.SimplSharp.CrestronSockets.UDPServer server, int numberOfBytesReceived)
        {CrestronConsole.PrintLine("UDP Data Received: {0}", numberOfBytesReceived);
            try
            {
                if (numberOfBytesReceived > 0)
                {
                    byte[] receivedBytes = new byte[numberOfBytesReceived];
                    Array.Copy(server.IncomingDataBuffer, receivedBytes, numberOfBytesReceived);
                    string data = System.Text.Encoding.ASCII.GetString(receivedBytes);
                    var x = ParseUDPResponse(data);

                    CrestronConsole.PrintLine("UDP Data Received: {0}", data);
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in UDPServerReceiveCallback: {0}", e.Message);
                CrestronConsole.PrintLine("Error in UDPServerReceiveCallback: {0}", e.Message);
            }
            ResponseServer.ReceiveDataAsync(UDPServerReceiveCallback);
        }

        private static UDPResponse ParseUDPResponse(string response)
        {
            try
            {
                UDPResponse jsonobj = Newtonsoft.Json.JsonConvert.DeserializeObject<UDPResponse>(response);
                CrestronConsole.PrintLine($"Varid: {jsonobj.UDPParameters[0].Varid} Data: {jsonobj.UDPParameters[0].Data}");
                string index = ParseIndex(jsonobj.UDPParameters[0].Varid.ToString());
                CrestronConsole.PrintLine($"Index: {index} Value: {jsonobj.UDPParameters[0].Data}");
                CrestronConsole.PrintLine($"jsonobj: {jsonobj}");
                EvertzHandler.tp_ButtonStatus(index, jsonobj.UDPParameters[0].Data);
                return jsonobj;
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in ParseUDPResponse: {0}", e.Message);
                CrestronConsole.PrintLine("Error in ParseUDPResponse: {0}", e.Message);
                return null;
            }
        }

        private static string ParseIndex(string input)
        {
            try
            {
                string[] parts = input.Split('.', '@');
                if (parts.Length >= 3)
                {
                    return parts[1]; // The part after the decimal and before the '@'
                }
                else
                {
                    return null; // The input string did not have the expected format
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in ParseIndex: {0}", e.Message);
                CrestronConsole.PrintLine("Error in ParseIndex: {0}", e.Message);
                return null;
            }

        }
    }
}
