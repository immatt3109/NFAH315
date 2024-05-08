using Newtonsoft.Json;
using System.Collections.Generic;
using Crestron.SimplSharp.CrestronIO;
using Crestron.SimplSharp;
using System;

namespace NFAHRooms
{
    public class Email
    {
        private static string json;
        private Email em;
        public void EmailSetup()
        {
            LoadEmailSetup();
            CrestronConsole.PrintLine("Email Setup Loaded");

        }

        [JsonProperty("serverADDR")]
        private string serverADDR { get; set; }

        [JsonProperty("serverPORT")]
        private ushort serverPORT { get; set; }

        [JsonProperty("serverSecure")]
        private bool serverSecure { get; set; }

        [JsonProperty("userName")]
        private string userName { get; set; }

        [JsonProperty("userPWD")]
        private string userPWD { get; set; }

        [JsonProperty("mailFrom")]
        private string mailFrom { get; set; }

        [JsonProperty("mailTo")]
        private string mailTo { get; set; }

        [JsonProperty("mailBody")]
        private string mailBody { get; set; }

        [JsonProperty("numAttach")]
        private ushort numAttach { get; set; }

        [JsonProperty("attachName")]
        private string attachName { get; set; }

        [JsonProperty("mailCC")]
        private string mailCC { get; set; }

        private void LoadEmailSetup()
        {
            string EmailFilePath = "/user/email.json";
            
            try
            {
                using (StreamReader sr = new StreamReader(EmailFilePath, System.Text.Encoding.Default))

                json = sr.ReadToEnd();

                em = JsonConvert.DeserializeObject<Email>(json);
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error reading email.json: {0}", e.Message);
                CrestronConsole.PrintLine("Error reading email.json: {0}", e.Message);
            }
        }

        public void SendEmail(string MailSub, string MailMessage)
        {
            CrestronMailFunctions.SendMailErrorCodes myErrorCode = new  CrestronMailFunctions.SendMailErrorCodes(); 
            myErrorCode = CrestronMailFunctions.SendMail(em.serverADDR, em.serverPORT, em.serverSecure, em.userName, em.userPWD, em.mailFrom, em.mailTo, em.mailCC, MailSub, (em.mailBody + "\r\r" + MailMessage), em.numAttach, em.attachName);
            CrestronConsole.PrintLine("Email Error Code = {0}", myErrorCode);        
        }
    }
}
