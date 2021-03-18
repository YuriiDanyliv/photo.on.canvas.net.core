using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using POC.DAL.Models;
using POC.DAL.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace POC.DAL.Repositories
{
    public abstract class Repository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected EFContext _context { get; set; }

        protected Repository(EFContext context)
        {
            _context = context;
        }

        public IQueryable<T> FindAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).AsNoTracking();
        }

        public async Task<T> FindByIdAsync(string Id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(i => i.Id == Id);
        }

        public virtual async Task<PagesList<T>> GetByQueryParamAsync(QueryParameters parameters)
        {
            var data = FindAll();
            return await PagesList<T>.GetPagesListAsync(data, parameters);
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}