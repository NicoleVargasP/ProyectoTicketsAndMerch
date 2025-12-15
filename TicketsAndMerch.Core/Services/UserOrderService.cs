using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketsAndMerch.Core.Services
{
    public class UserOrderService : IUserOrderService
    {
        private readonly IUserOrderRepository _userOrderRepository;

        public UserOrderService(IUserOrderRepository userOrderRepository)
        {
            _userOrderRepository = userOrderRepository;
        }

        public async Task<IEnumerable<UserOrder>> GetUserOrdersByLoginAsync(string login, UserOrderQueryFilter filters)
        {
            return await _userOrderRepository.GetUserOrdersByLoginAsync(login, filters);
        }
    }
}
