using FI_Infra_Tools_Aggregate;
using Nest;
using System;
using System.Linq;
using System.Reflection;

namespace FI_Infra_Tools_Implementation
{
    public class LogService:ILog
    {
        private readonly IEmail _email;
        private readonly IDevOps _devOps;
        public LogService(IEmail email,IDevOps devOps)
        {
            _email = email;   
            _devOps = devOps;
        }
        private void CreateTXT(FI_Infra_Tools_Core.Log log)
        {
            string filename = System.DateTime.Now.ToString("ddMMyyyy") + ".txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter(filename, true);
            file.WriteLine(SeriazableLog(log));
            file.Close();
        }

        private void InsertLogKibana(FI_Infra_Tools_Core.Log log)
        {
            var settings = new ConnectionSettings(new Uri("https://192.168.30.177:9200"))
          .DefaultIndex("ingetec");
            var client = new ElasticClient(settings);
            var asyncIndexResponse = client.IndexDocument(log);

        }

        public void InsertTraceLog(FI_Infra_Tools_Core.Log log)
        {
            try
            {
                CreateTXT(log);
                InsertLogKibana(log);
                SendMailLog(log);
                NewBug(log);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        private static string SeriazableLog(FI_Infra_Tools_Core.Log log)
        {
            FieldInfo[] fields = typeof(FI_Infra_Tools_Core.Log).GetFields();
            PropertyInfo[] properties = typeof(FI_Infra_Tools_Core.Log).GetProperties().Where(x => x.GetIndexParameters().Length == 0).ToArray();
            return string.Join(";",properties.Select(p => p.GetValue(log, null)).ToArray());

        }

        private  void SendMailLog(FI_Infra_Tools_Core.Log log)
        {
            System.Net.Mail.SmtpClient smtpClient = _email.AuthenticateUser("gestiona@ingetec.com.co", "*Caracax05");
            FI_Infra_Tools_Core.ParametersEmail parametersEmail  = new FI_Infra_Tools_Core.ParametersEmail
            (
                      "Error - API " + log.NameAPI + " " + System.DateTime.Now.ToLongDateString(),
                      SeriazableLog(log),
                      "devops@ingetec.com.co",
                      "gestiona@ingetec.com.co",
                      "*Caracax05",
                      log.NameMethod
                      
                  );            
            _email.SendMail(smtpClient, parametersEmail);

        }

        private void NewBug(FI_Infra_Tools_Core.Log log)
        {
            _devOps.CreateBugUsingClientLib("Error API" + log.NameAPI , String.Empty, log.Content, log.NameMethod, log.IPSource);
            
        }


    }
}
