using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using POC.BLL.Models;
using System;
using System.Threading.Tasks;

namespace POC.BLL.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfigurationService<EmailServiceConfig> _cfgService;
        private readonly ILogger<EmailSenderService> _logger;

        public EmailSenderService(
          IConfigurationService<EmailServiceConfig> cfgService,
          ILogger<EmailSenderService> logger)
        {
            _cfgService = cfgService;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task SendEmailAsync(EmailMessage message)
        {
            var mailMessage = await CreateEmailMessage(message);
            await SendAsync(mailMessage);
        }

        /// <summary>
        /// Create message obj with MimeMessage type
        /// </summary>
        /// <param name="message">message</param>
        /// <returns></returns>
        private async Task<MimeMessage> CreateEmailMessage(EmailMessage message)
        {
            var _emailConfig = await _cfgService.GetSettingsAsync();

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        /// <summary>
        /// Send message to emails
        /// </summary>
        /// <param name="message">message</param>
        /// <returns></returns>
        private async Task SendAsync(MimeMessage message)
        {
            var _emailConfig = await _cfgService.GetSettingsAsync();
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(0, ex, "Email send error");
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
