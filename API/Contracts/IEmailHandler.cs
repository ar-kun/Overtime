namespace API.Contracts
{
    public interface IEmailHandler
    {
        // Defines method for sending an email with the specified subject and body to the specified email address
        void Send(string subject, string body, string toEmail);
    }
}
