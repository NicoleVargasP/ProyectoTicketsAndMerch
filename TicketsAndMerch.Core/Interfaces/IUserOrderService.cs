using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IUserOrderService
    {
        // No se usa UserId, se filtra por Login
        Task<IEnumerable<UserOrder>> GetUserOrdersByLoginAsync(string login, UserOrderQueryFilter filters);
    }
}
