using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.QueryFilters;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IUserOrderService
    {
        Task<IEnumerable<UserOrder>> GetUserOrdersAsync(int userId, UserOrderQueryFilter filters);
    }
}
