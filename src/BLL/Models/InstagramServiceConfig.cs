using System.Collections.Generic;

namespace POC.BLL.Models
{
    public class InstagramServiceConfig : IConfigurationModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public List<string> MsgReceiverList { get; set; }
    }
}