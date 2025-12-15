using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IUserOrderRepository
    {
        // Obtener órdenes por Login
        Task<IEnumerable<UserOrder>> GetUserOrdersByLoginAsync(string login, UserOrderQueryFilter filters);
    }
}
