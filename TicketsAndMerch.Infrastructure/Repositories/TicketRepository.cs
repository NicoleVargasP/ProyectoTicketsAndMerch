using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(TicketsAndMerchContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _entities
                .Include(t => t.Concert)
                .ToListAsync();
        }

        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            return await _entities
                .Include(t => t.Concert)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByConcertAsync(int concertId)
        {
            return await _entities
                .Include(t => t.Concert)
                .Where(t => t.ConcertId == concertId)
                .ToListAsync();
        }
    }
}
