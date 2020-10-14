using POC.DAL.Context;
using POC.DAL.Entities;
using POC.DAL.Interfaces;

namespace POC.DAL.Repositories
{
  public class OrderRepository : Repository<Order>, IOrderRepository
  {
    public OrderRepository(EFContext context) : base(context)
    {
    }
  }
}