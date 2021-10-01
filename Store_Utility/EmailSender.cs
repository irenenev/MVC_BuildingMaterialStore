using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Store_Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private string password = "";
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        public async Task Execute(string email, string subject, string body)
        {
            password = _configuration.GetSection("Password").Get<string>();
            // from, to, subject, messagebody
            MailMessage message = new MailMessage(WebConstants.EmailAdmin, email, subject, body);
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient(WebConstants.Host, WebConstants.Port);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(WebConstants.EmailAdmin, password);
            client.DeliveryFormat = SmtpDeliveryFormat.International;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                await client.SendMailAsync(message);
            }
            catch (SmtpException e)
            {
                e.ToString();
            }
        }
    }
}
