using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Hosting.Server;
using Grpc.Core;

namespace UserWebApi.Services
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string userEmail, string subject)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/EmailTemplate.html"))

            {
                body = reader.ReadToEnd();
            }


            var sender = "projecttestmail314@gmail.com";
            var pwd = "qrxi juot yepu ccyh";

            using (MailMessage mailMessage = new MailMessage())

            {
                mailMessage.From = new MailAddress(sender);

                mailMessage.Subject = subject;

                mailMessage.Body = body;

                mailMessage.IsBodyHtml = true;

                mailMessage.To.Add(new MailAddress(userEmail));

                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(sender, pwd)
                };

                client.Send(mailMessage);
            }
        }
    }
}
