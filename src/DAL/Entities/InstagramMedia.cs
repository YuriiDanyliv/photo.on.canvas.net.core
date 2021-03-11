using System.ComponentModel.DataAnnotations.Schema;

namespace POC.DAL.Entities
{
    [Table("instagramMedia")]
    public class InstagramMedia : BaseEntity
    {
        public string Category { get; set; }

        public string ImageUri { get; set; }

        public string VideoUri { get; set; }
    }
}