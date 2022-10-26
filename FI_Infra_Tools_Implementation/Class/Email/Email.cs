using FI_Infra_Tools_Aggregate;
using FI_Infra_Tools_Core;
using System;
using System.Net.Mail;

namespace FI_Infra_Tools_Implementation
{
    public class Email:IEmail
    {

        public SmtpClient AuthenticateUser(string username, string password)
        {

            var fromAddress = new MailAddress(username, password);
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(fromAddress.Address, password),
                Timeout = 100000
            };
            return smtp;
        }


        public bool SendMail(SmtpClient SmtpServer, ParametersEmail email)
        {
            try 
            {             MailMessage mail = new MailMessage();
                mail.From = new MailAddress(email.UserName);
                if (!string.IsNullOrEmpty(email.Recipients))
                {
                    string[] strReci = email.Recipients.Split(';');
                    for (int i = 0; i < strReci.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(strReci[i].ToString()))
                        mail.To.Add(strReci[i].ToString());
                    }
                }
                

                mail.Subject = email.Subject;
                mail.Body = email.Body;
                mail.IsBodyHtml = true;
                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception ex )
            {
                throw new Exception(ex.Message);
                
            }

        }


    }
}