using POC.BLL.Models;

namespace POC.BLL.Interfaces
{
  public interface IConfigurationService
  {
    EmailServiceConfig GetEmailConfig();
    void SetEmailConfig(EmailServiceConfig cfgModel);
  }
}