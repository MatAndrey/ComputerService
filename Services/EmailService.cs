
using System.Net;
using System.Net.Mail;

namespace ComputerService.Services
{
    public class EmailService(IConfiguration config) : IEmailService
    {
        public async Task SendEmailToAdminAsync(string subject, string body)
        {
            await SendEmailAsync(config["Smtp:AdminAdress"], subject, body);
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpClient = new SmtpClient(config["Smtp:Host"])
            {
                Port = int.Parse(config["Smtp:Port"]),
                Credentials = new NetworkCredential(config["Smtp:Username"], config["Smtp:Password"]),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(config["Smtp:From"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
