using API.Contracts;
using System.Net.Mail;

namespace API.Utilities.Handlers
{
    public class EmailHandler : IEmailHandler
    {
        // Declares all private property for EmailHandler class.
        private string _server;
        private int _port;
        private string _fromEmailAddress;

        // Declares a public constructor that takes server, port, and fromEmailAddress parameter.
        public EmailHandler(string server, int port, string fromEmailAddress)
        {
            _server = server;
            _port = port;
            _fromEmailAddress = fromEmailAddress;
        }

        // Declares a public method Send that takes subject, body and toEmail parameter.
        public void Send(string subject, string body, string toEmail)
        {
            // Build message
            var message = new MailMessage()
            {
                From = new MailAddress(_fromEmailAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(toEmail));
            // Send message
            using var smtpClient = new SmtpClient(_server, _port);
            smtpClient.Send(message);
        }
    }
}
