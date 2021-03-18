
namespace POC.BLL.Models
{
    public class FileData
    {
        public string Id { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] File { get; set; }
    }
}