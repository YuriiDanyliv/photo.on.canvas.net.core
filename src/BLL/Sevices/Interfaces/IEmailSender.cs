using System.Threading.Tasks;
using POC.BLL.Models;

namespace POC.BLL.Services
{
    public interface IEmailSenderService
    {
        /// <summary>
        /// Send message to email 
        /// </summary>
        /// <param name="message">message</param>
        /// <returns></returns>
        Task SendEmailAsync(EmailMessage message);
    }
}
