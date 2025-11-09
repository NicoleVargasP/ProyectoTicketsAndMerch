using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.QueryFilters;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseData> GetAllOrdersAsync(OrderQueryFilter filters);
        Task<IEnumerable<Order>> GetAllOrdersDapperAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
    }
}
