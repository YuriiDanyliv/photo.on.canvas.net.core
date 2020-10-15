using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using POC.DAL.Context;
using POC.DAL.Interfaces;
using POC.DAL.Models;
using POC.DAL.Entities;

namespace POC.DAL.Repositories
{
  public abstract class Repository<T> : IBaseRepository<T> where T : BaseEntity
  {
    protected EFContext _context { get; set; }

    public Repository(EFContext context)
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

    public PagesList<T> GetByQueryParam(QueryParameters parameters)
    {
      return PagesList<T>.GetPagesList(FindAll(), parameters.PageNumber, parameters.PageSize);
    }

    public void Create(T entity)
    {
      var e = _context.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
      var e =_context.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
      _context.Set<T>().Update(entity);
    }
  }
}