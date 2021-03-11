using POC.DAL.Entities;

namespace POC.DAL.Repositories
{
    public class FileRepository : Repository<FileEntity>, IFileRepository
    {
        public FileRepository(EFContext context) : base(context)
        {
        }
    }
}