using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.QueryFilters;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface ITicketService
    {
        Task<ResponseData> GetAllTicketsAsync(TicketQueryFilter filters);
        Task<IEnumerable<Ticket>> GetAllTicketsDapperAsync();
        Task<Ticket> GetTicketByIdAsync(int id);
        Task AddTicketAsync(Ticket ticket);
        Task UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(int id);
    }
}
