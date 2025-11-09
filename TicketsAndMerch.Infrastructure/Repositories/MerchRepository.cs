using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class MerchRepository : BaseRepository<Merch>, IMerchRepository
    {
        public MerchRepository(TicketsAndMerchContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Merch>> GetAllMerchAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<Merch> GetMerchByIdAsync(int id)
        {
            return await _entities.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Merch>> GetMerchInStockAsync()
        {
            return await _entities
                .Where(m => m.Stock > 0)
                .OrderByDescending(m => m.Stock)
                .ToListAsync();
        }
    }
}
