using POC.DAL.Models;

namespace POC.BLL.Model
{
  public class FileData
  {
    public string Id { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] File { get; set; }
  }
}