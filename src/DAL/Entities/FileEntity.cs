using System.ComponentModel.DataAnnotations.Schema;

namespace POC.DAL.Entities
{
    [Table("galery")]
    public class FileEntity : BaseEntity
    {
        public string FileName { get; set; }

        public string Folder { get; set; }

        public string Category { get; set; }

        public string ContentType { get; set; }
    }
}