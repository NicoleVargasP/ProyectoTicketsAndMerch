using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(TicketsAndMerchContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _entities
                .Include(p => p.Order)
                .ToListAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _entities
                .Include(p => p.Order)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByOrderAsync(int orderId)
        {
            return await _entities
                .Include(p => p.Order)
                .Where(p => p.OrderId == orderId)
                .ToListAsync();
        }
    }
}
