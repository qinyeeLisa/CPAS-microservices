using System.Net.Mail;
using System.Net;

namespace UserWebApi.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string userEmail, string subject, string message)
        {
            var sender = "projecttestmail314@gmail.com";
            var pwd = "qrxi juot yepu ccyh";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(sender, pwd)
            };

            return client.SendMailAsync(
                new MailMessage(from: sender,
                                to: userEmail,
                                subject,
                                message
                                ));
        }
    }
}
