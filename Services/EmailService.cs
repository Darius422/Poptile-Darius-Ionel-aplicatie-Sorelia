using System.Net;
using System.Net.Mail;

namespace BeautyHealthStore.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(
            string smtpServer,
            int smtpPort,
            string senderEmail,
            string senderPassword,
            string recipientEmail,
            string subject,
            string body)
        {
            using var client = new SmtpClient(smtpServer, smtpPort);

            client.Credentials = new NetworkCredential(
                senderEmail,
                senderPassword);

            client.EnableSsl = true;

            var message = new MailMessage(
                senderEmail,
                recipientEmail,
                subject,
                body);

            await client.SendMailAsync(message);
        }
    }
}