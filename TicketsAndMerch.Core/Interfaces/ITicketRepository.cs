using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface ITicketRepository : IBaseRepository<Ticket>
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<Ticket?> GetTicketByIdAsync(int id);
        Task<IEnumerable<Ticket>> GetTicketsByConcertAsync(int concertId);
    }
}
