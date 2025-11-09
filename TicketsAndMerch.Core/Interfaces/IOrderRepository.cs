using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId);
    }
}

