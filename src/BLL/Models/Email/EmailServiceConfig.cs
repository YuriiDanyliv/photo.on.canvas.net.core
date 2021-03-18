namespace POC.BLL.Models
{
    public class EmailServiceConfig : IConfigurationModel
    {
        public bool EmailConfirmIsOn { get; set; }

        public string From { get; set; }

        public string SmtpServer { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}