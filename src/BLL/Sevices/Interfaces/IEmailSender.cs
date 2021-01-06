using System.Threading.Tasks;
using POC.DAL.Models;

namespace POC.BLL.Services
{
  public interface IEmailSenderService
  {
    Task SendEmailAsync(EmailMessage message);
  }
}
