using POC.DAL.Context;
using POC.DAL.Entities;
using POC.DAL.Interfaces;

namespace POC.DAL.Repositories
{
  public class FileRepository : Repository<FileEntity>, IFileRepository
  {
    public FileRepository(EFContext context) : base(context)
    {
    }
  }
}