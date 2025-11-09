using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;
using TicketsAndMerch.Core.Enum;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class ConcertRepository : BaseRepository<Concert>, IConcertRepository
    {
        public ConcertRepository(TicketsAndMerchContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Concert>> GetAllConcertsAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<Concert> GetConcertByIdAsync(int id)
        {
            return await _entities.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
