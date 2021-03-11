using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POC.DAL.Entities;
using POC.DAL.Models;

namespace POC.DAL.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(EFContext context) : base(context)
        {
        }

        public override async Task<PagesList<Order>> GetByQueryParamAsync(QueryParameters parameters)
        {
            var orders = await _context.Set<Order>().AsNoTracking().Include(order => order.Canvas).ToListAsync();
            return PagesList<Order>.GetPagesList(orders, parameters);
        }
    }
}