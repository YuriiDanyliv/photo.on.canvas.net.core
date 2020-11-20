using System.Threading.Tasks;
using POC.DAL.Models;

namespace POC.BLL.Services
{
  public interface IEmailSenderService
  {
    void SendEmail(EmailMessage message);
    Task SendEmailAsync(EmailMessage message);
  }
}
