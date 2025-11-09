using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IBuyTicketService
    {
        Task<IEnumerable<BuyTicket>> GetAllBuyTicketsDapperAsync();
        Task<BuyTicket> GetBuyTicketByIdAsync(int id);
        Task<BuyTicket> BuyTicketAsync(BuyTicket buyTicket);
        Task DeleteBuyTicketAsync(int id);
    }
}