using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Domain;


namespace Web.Infrastructure
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return SendEmail(message);
        }


        protected Task SendEmail(IdentityMessage message)
        {
            using (var mm = new MailMessage(PrivateSettings.SenderEmail, message.Destination))
            {
                mm.Subject = message.Subject;
                mm.Body = message.Body;
                mm.IsBodyHtml = true;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    EnableSsl = true
                };
                var networkCred = new NetworkCredential(PrivateSettings.SenderEmail, PrivateSettings.EmailPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCred;                
                smtp.Port = 587;                
                smtp.Send(mm);       
            }
            return Task.FromResult(0);

        }
    }
}