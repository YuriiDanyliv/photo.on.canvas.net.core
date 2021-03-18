using Microsoft.EntityFrameworkCore;
using POC.DAL.Entities;
using POC.DAL.Models;
using System.Threading.Tasks;

namespace POC.DAL.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(EFContext context) : base(context)
        {
        }

        public override async Task<PagesList<Order>> GetByQueryParamAsync(QueryParameters parameters)
        {
            var orders = _context.Set<Order>().AsNoTracking().Include(order => order.Canvas);
            return await PagesList<Order>.GetPagesListAsync(orders, parameters);


        }
    }
}