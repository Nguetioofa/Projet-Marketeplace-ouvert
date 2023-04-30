using System.Net.Mail;
using System.Net;

namespace ApiWeb.Services
{
    public class EmailSender : IEmailSender
    {
            private readonly SmtpClient _client;
            public EmailSender(IConfiguration configuration)
            {
                // Création du SmtpClient à partir des paramètres du fichier appsettings.json
                _client = new SmtpClient
                {
                    Host = configuration["Email:Host"],
                    //Port = int.Parse(configuration["Email:Port"]),
                    EnableSsl = bool.Parse(configuration["Email:EnableSsl"]),
                    Credentials = new NetworkCredential(configuration["Email:Username"], configuration["Email:Password"])
                };
            }

            public async Task SendEmailAsync(string email, string subject, string message)
            {
                // Création du MailMessage
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("ngueukeu@outlook.com"),
                    To = { email },
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };
             //SmtpClient SMTPServer = new SmtpClient("52.97.14.98");
            _client.Host = "52.97.14.98";
            // Envoi du mail avec le SmtpClient
            await _client.SendMailAsync(mailMessage);
            }
    }

}
