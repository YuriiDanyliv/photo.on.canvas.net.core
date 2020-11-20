using System.Threading.Tasks;
using POC.DAL.Models;
using MailKit.Net.Smtp;
using MimeKit;
using POC.BLL.Models;

namespace POC.BLL.Services
{
  public class EmailSenderService : IEmailSenderService
  {
    private readonly IConfigurationService<EmailServiceConfig> _cfgService;
    private readonly EmailServiceConfig _emailConfig;

    public EmailSenderService(IConfigurationService<EmailServiceConfig> cfgService)
    {
      _cfgService = cfgService;
      _emailConfig = _cfgService.GetSettingsAsync().Result;
    }

    public void SendEmail(EmailMessage message)
    {
      var emailMessage = CreateEmailMessage(message);
      Send(emailMessage);
    }

    public async Task SendEmailAsync(EmailMessage message)
    {
      var mailMessage = CreateEmailMessage(message);
      await SendAsync(mailMessage);
    }

    private MimeMessage CreateEmailMessage(EmailMessage message)
    {
      var emailMessage = new MimeMessage();
      emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
      emailMessage.To.AddRange(message.To);
      emailMessage.Subject = message.Subject;
      emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

      return emailMessage;
    }

    private void Send(MimeMessage mailMessage)
    {
      using (var client = new SmtpClient())
      {
        try
        {
          client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
          client.AuthenticationMechanisms.Remove("XOAUTH2");
          client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

          client.Send(mailMessage);
        }
        catch
        {
          throw new System.Exception("Fail to send email");
        }
        finally
        {
          client.Disconnect(true);
          client.Dispose();
        }
      }
    }

    private async Task SendAsync(MimeMessage mailMessage)
    {
      using (var client = new SmtpClient())
      {
        try
        {
          await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
          client.AuthenticationMechanisms.Remove("XOAUTH2");
          await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

          await client.SendAsync(mailMessage);
        }
        catch
        {
          throw new System.Exception("Fail to send email");
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
