using System;
using System.Linq;
using System.Linq.Expressions;
using POC.DAL.Models;

namespace POC.DAL.Interfaces
{
  public interface IBaseRepository<T> where T : IBaseEntity
  {
    IQueryable<T> FindAll();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    PagesList<T> GetByQueryParam(QueryParameters parameters);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
  }
}