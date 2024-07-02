using Newtonsoft.Json;
using Crestron.SimplSharp.CrestronIO;
using Crestron.SimplSharp;
using System;
using System.Net.Mail;

namespace NFAHRooms
{
    public static class Email
    {
        private static string json;
        
        public static void Initialize()
        {
            LoadEmailSetup();
        }

        [JsonProperty("serverADDR")]
        public static string serverADDR { get; private set; }

        [JsonProperty("serverPORT")]
        public static ushort serverPORT { get; private set; }

        [JsonProperty("serverSecure")]
        public static bool serverSecure { get; private set; }

        [JsonProperty("userName")]
        public static string userName { get; private set; }

        [JsonProperty("userPWD")]
        public static string userPWD { get; private set; }

        [JsonProperty("mailFrom")]
        public static string mailFrom { get; private set; }

        [JsonProperty("mailTo")]
        public static string mailTo { get; private set; }

        [JsonProperty("mailBody")]
        public static string mailBody { get; private set; }

        [JsonProperty("numAttach")]
        public static ushort numAttach { get; private set; }

        [JsonProperty("attachName")]
        public static string attachName { get; private set; }

        [JsonProperty("mailCC")]
        public static string mailCC { get; private set; }

        private static void LoadEmailSetup()
        {
            string EmailFilePath = "/user/email.json";
            
            try
            {
                using (StreamReader sr = new StreamReader(EmailFilePath, System.Text.Encoding.Default))
                {
                    json = sr.ReadToEnd();
                }
                var tempEmail = JsonConvert.DeserializeObject<EmailSetup>(json);

                serverADDR = tempEmail.serverADDR;
                serverPORT = tempEmail.serverPORT;
                serverSecure = tempEmail.serverSecure;
                userName = tempEmail.userName;
                userPWD = tempEmail.userPWD;
                mailFrom = tempEmail.mailFrom;
                mailTo = tempEmail.mailTo;
                mailBody = tempEmail.mailBody;
                numAttach = tempEmail.numAttach;
                attachName = tempEmail.attachName;
                mailCC = tempEmail.mailCC;
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error reading email.json: {0}", e.Message);
                CrestronConsole.PrintLine("Error reading email.json: {0}", e.Message);
                SendEmail(RoomSetup.MailSubject + " LoadEmailSetup", e.Message);
            }
        }

        public static void SendEmail(string MailSub, string MailMessage)
        {                       
            try 
            { 
                CrestronMailFunctions.SendMailErrorCodes myErrorCode = new  CrestronMailFunctions.SendMailErrorCodes(); 
                myErrorCode = CrestronMailFunctions.SendMail(serverADDR, serverPORT, serverSecure, userName, userPWD, mailFrom, mailTo, mailCC, MailSub, (mailBody + MailMessage), numAttach, attachName);
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error sending thisail: {0}", e.Message);
                CrestronConsole.PrintLine("Error sending thisail: {0}", e.Message);
                SendEmail(RoomSetup.MailSubject + " SendEmail", e.Message);
            }
        }

        private class EmailSetup
        {
            [JsonProperty("serverADDR")]
            public string serverADDR { get; set; }

            [JsonProperty("serverPORT")]
            public ushort serverPORT { get; set; }

            [JsonProperty("serverSecure")]
            public bool serverSecure { get; set; }

            [JsonProperty("userName")]
            public string userName { get; set; }

            [JsonProperty("userPWD")]
            public string userPWD { get; set; }

            [JsonProperty("mailFrom")]
            public string mailFrom { get; set; }

            [JsonProperty("mailTo")]
            public string mailTo { get; set; }

            [JsonProperty("mailBody")]
            public string mailBody { get; set; }

            [JsonProperty("numAttach")]
            public ushort numAttach { get; set; }

            [JsonProperty("attachName")]
            public string attachName { get; set; }

            [JsonProperty("mailCC")]
            public string mailCC { get; set; }
        }
    }
}
