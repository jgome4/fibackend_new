using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using MediatR;
using System.Collections.Generic;
using FI_Infra_Tools_Core;
using FI_Infra_Tools_Aggregate;
using FI_Domain;

namespace FI_Aplication_Events_Domain;

    public class OnTaskCommitted
    {
        public class Handler : INotificationHandler<DomainEventNotification<TaskCommitted>>
        {
            private readonly ILogger<Handler> _log;
            private readonly IEmail _email;
            

            EmailTask _EmaillTask;
            public Handler(ILogger<Handler> log, IEmail email)
            {
                _log = log;
                _email = email;
            }

            public System.Threading.Tasks.Task Handle(DomainEventNotification<TaskCommitted> notification, CancellationToken cancellationToken)
            {
                var domainEvent = notification.DomainEvent;
                try
                {
                System.Net.Mail.SmtpClient smtpClient = _email.AuthenticateUser("gestiona@ingetec.com.co", "*Caracax05");

                SendNotificationMailTask(smtpClient,domainEvent);
                    return System.Threading.Tasks.Task.CompletedTask;
                }
                catch (Exception exc)
                {
                    _log.LogError(exc, "Error handling domain event {domainEvent}", domainEvent.GetType());
                    throw;
                }
            }

       





        private void SendNotificationMailTask(System.Net.Mail.SmtpClient smtpClient,TaskCommitted taskCommitted)
        {

            string subject = taskCommitted.ActionsDomainEvents == ActionsDomainEvents.ADD ? "Se creó la tarea " + taskCommitted.TaskName :
                            taskCommitted.ActionsDomainEvents == ActionsDomainEvents.UPDATE ? "Se actualizó la tarea " + taskCommitted.TaskName :
                            taskCommitted.ActionsDomainEvents == ActionsDomainEvents.DELETE ? "Se eliminó la tarea " + taskCommitted.TaskName : 
                            string.Empty;

            string html = System.IO.File.ReadAllText(".\\TempleateMail\\MailTask.html");
            html = html.Replace("XXX1", "Jaime");
            html = html.Replace("XXX2", $"<span><b>{subject}</b></span>");
            ParametersEmail parametersEmail = new ParametersEmail
               (
                   "FI - Tareas Asignadas -" + subject,
                    html,
                    "jorgegomez@ingetec.com.co",
                   "gestiona@ingetec.com.co",
                   "*Caracax05",
                   string.Empty
               );
            _email.SendMail(smtpClient, parametersEmail);

        }
    }

    }

