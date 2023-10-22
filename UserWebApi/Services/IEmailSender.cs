namespace UserWebApi.Services
{
    public interface IEmailSender
    {
        void SendEmail(string email, string subject);
    }
}
