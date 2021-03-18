using POC.DAL.Entities;
using POC.DAL.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace POC.DAL.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T> FindByIdAsync(string Id);
        Task<PagesList<T>> GetByQueryParamAsync(QueryParameters parameters);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}