using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(TicketsAndMerchContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _entities
                .Include(o => o.User)
                .Include(o => o.Merch)
                .Include(o => o.Ticket)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _entities
                .Include(o => o.User)
                .Include(o => o.Merch)
                .Include(o => o.Ticket)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId)
        {
            return await _entities
                .Include(o => o.Merch)
                .Include(o => o.Ticket)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
    }
}
