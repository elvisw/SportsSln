using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SportsStore.Models
{

    public class EFOrderRepository : IOrderRepository
    {
        private StoreDbContext _context;

        public EFOrderRepository(StoreDbContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Order> Orders => _context.Orders
                            .Include(o => o.Lines)
                            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                _context.Orders.Add(order);
            }
            _context.SaveChanges();
        }
    }
}
