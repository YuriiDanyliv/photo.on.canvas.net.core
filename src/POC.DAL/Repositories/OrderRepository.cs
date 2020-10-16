using System.Linq;
using Microsoft.EntityFrameworkCore;
using POC.DAL.Context;
using POC.DAL.Entities;
using POC.DAL.Interfaces;
using POC.DAL.Models;

namespace POC.DAL.Repositories
{
  public class OrderRepository : Repository<Order>, IOrderRepository
  {
    public OrderRepository(EFContext context) : base(context)
    {
    }
    
    public override PagesList<Order> GetByQueryParam(QueryParameters parameters)
    {
      return PagesList<Order>.GetPagesList(_context.Set<Order>().AsNoTracking().Include(order => order.Canvas),
       parameters.PageNumber, parameters.PageSize);
    }
  }
}