using Application.Abstractions.Email;
using Application.Abstractions.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Email
{
    internal sealed class EmailSender : IEmailSender
    {
        private readonly EmailSettings _settings;
        public EmailSender(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }
        public async Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken)
        {
            var message = new MimeMessage();

            //Sender
            message.From.Add(new MailboxAddress(_settings.SenderUsername, _settings.SenderEmail));

            //Reciever
            message.To.Add(new MailboxAddress("", to));

            //Message content
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            //Send email
            using var client = new SmtpClient();

            try
            {
                await client.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, SecureSocketOptions.StartTls, cancellationToken);
                await client.AuthenticateAsync(_settings.SmtpUsername, _settings.SmtpPassword, cancellationToken);
                await client.SendAsync(message, cancellationToken);
            }

            finally
            {
                await client.DisconnectAsync(true, cancellationToken);
            }
        }
    }
}
