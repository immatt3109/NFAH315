using Newtonsoft.Json;
using Crestron.SimplSharp.CrestronIO;
using Crestron.SimplSharp;
using System;
using System.Net.Mail;

namespace NFAHRooms
{
    public class Email
    {
        private static string json;
        //private Email em;
              

       public void EmailSetup()
        {
            LoadEmailSetup();
            
            
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

                var tempEmail = JsonConvert.DeserializeObject<Email>(json);
                CrestronConsole.PrintLine(tempEmail.serverADDR);
                CrestronConsole.PrintLine(tempEmail.serverPORT.ToString());
                CrestronConsole.PrintLine(tempEmail.serverSecure.ToString());
                CrestronConsole.PrintLine(tempEmail.userName);
                CrestronConsole.PrintLine(tempEmail.userPWD);
                CrestronConsole.PrintLine(tempEmail.mailFrom);
                CrestronConsole.PrintLine(tempEmail.mailTo);
                CrestronConsole.PrintLine(tempEmail.mailCC);
                CrestronConsole.PrintLine(tempEmail.mailBody);
                CrestronConsole.PrintLine(tempEmail.numAttach.ToString());
                CrestronConsole.PrintLine(tempEmail.attachName);

                this.serverADDR = tempEmail.serverADDR;
                this.serverPORT = tempEmail.serverPORT;
                this.serverSecure = tempEmail.serverSecure;
                this.userName = tempEmail.userName;
                this.userPWD = tempEmail.userPWD;
                this.mailFrom = tempEmail.mailFrom;
                this.mailTo = tempEmail.mailTo;
                this.mailBody = tempEmail.mailBody;
                this.numAttach = tempEmail.numAttach;
                this.attachName = tempEmail.attachName;
                this.mailCC = tempEmail.mailCC;
                

                CrestronConsole.PrintLine($"{this.serverADDR}, {this.serverPORT.ToString()}, {this.serverSecure.ToString()}, {this.userName}, {this.userPWD}, {this.mailFrom}, {this.mailTo}, {this.mailCC}, {this.mailBody}, {this.numAttach.ToString()}, {this.attachName}");

            }
            catch (Exception e)
            {
                ErrorLog.Error("Error reading email.json: {0}", e.Message);
                CrestronConsole.PrintLine("Error reading email.json: {0}", e.Message);
            }
        }

        public void SendEmail(string MailSub, string MailMessage)
        {
            try
            {
                CrestronConsole.PrintLine("SendEmail");
                CrestronConsole.PrintLine("Server");
                CrestronConsole.PrintLine($"Server: {this.serverADDR}");
                CrestronConsole.PrintLine("Port");
                CrestronConsole.PrintLine($"Port: {this.serverPORT}");
                CrestronConsole.PrintLine($"Secure: {this.serverSecure}");
                CrestronConsole.PrintLine($"UserName: {this.userName}");
                CrestronConsole.PrintLine($"Password: {this.userPWD}");
                CrestronConsole.PrintLine($"MailFrom: {this.mailFrom}");
                CrestronConsole.PrintLine($"MailTo: {this.mailTo}");
                CrestronConsole.PrintLine($"MailCC: {this.mailCC}");

                CrestronConsole.PrintLine($"MailBody: {this.mailBody}");
                CrestronConsole.PrintLine($"NumAttch: {this.numAttach}");
                CrestronConsole.PrintLine($"AttchName: {this.attachName}");
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error printing: {0}", e.Message);
                CrestronConsole.PrintLine("Error printing: {0} {1} {2} ", e.Message, e.StackTrace, e.Source);
            }

           
            try 
            { 
                
            CrestronMailFunctions.SendMailErrorCodes myErrorCode = new  CrestronMailFunctions.SendMailErrorCodes(); 
            myErrorCode = CrestronMailFunctions.SendMail(this.serverADDR, this.serverPORT, this.serverSecure, this.userName, this.userPWD, this.mailFrom, this.mailTo, this.mailCC, MailSub, (this.mailBody + MailMessage), this.numAttach, this.attachName);
            CrestronConsole.PrintLine("Email Error Code = {0}", myErrorCode);        
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error sending thisail: {0}", e.Message);
                CrestronConsole.PrintLine("Error sending thisail: {0}", e.Message);
            }
        }
    }
}
