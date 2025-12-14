using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.QueryFilters;

namespace TicketsAndMerch.Core.Services
{
    public class UserOrderService : IUserOrderService
    {
        private readonly IUserOrderRepository _userOrderRepository;

        public UserOrderService(IUserOrderRepository userOrderRepository)
        {
            _userOrderRepository = userOrderRepository;
        }

        public async Task<IEnumerable<UserOrder>> GetUserOrdersAsync(int userId, UserOrderQueryFilter filters)
        {
            return await _userOrderRepository.GetUserOrdersAsync(userId, filters);
        }
    }
}
