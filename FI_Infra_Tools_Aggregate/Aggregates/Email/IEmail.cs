using FI_Infra_Tools_Core;
using System.Net.Mail;


namespace FI_Infra_Tools_Aggregate
{
    public interface IEmail
    {
        SmtpClient AuthenticateUser(string username, string password);
        bool SendMail(SmtpClient SmtpServer, ParametersEmail email);
    }
}
