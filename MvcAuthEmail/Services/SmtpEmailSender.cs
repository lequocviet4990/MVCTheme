using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MvcAuthEmail.Services
{
    public sealed class SmtpOptions
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
    }

    public sealed class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpOptions _options;

        public SmtpEmailSender(IOptions<SmtpOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_options.FromName, _options.FromEmail));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            message.Body = new BodyBuilder { HtmlBody = htmlMessage }.ToMessageBody();

            using var client = new SmtpClient();
            var secure = _options.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;
            await client.ConnectAsync(_options.Host, _options.Port, secure);

            if (!string.IsNullOrWhiteSpace(_options.User))
            {
                await client.AuthenticateAsync(_options.User, _options.Password);
            }

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}

