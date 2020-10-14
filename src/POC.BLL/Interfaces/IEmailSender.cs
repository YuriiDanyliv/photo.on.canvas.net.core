using System.Threading.Tasks;
using POC.DAL.Models;

namespace BLL.Interfaces
{
  public interface IEmailSenderService
  {
    void SendEmail(EmailMessage message);
    Task SendEmailAsync(EmailMessage message);
  }
}
