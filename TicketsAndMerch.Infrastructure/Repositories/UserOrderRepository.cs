using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.QueryFilters;
using TicketsAndMerch.Infrastructure.Data;
using TicketsAndMerch.Infrastructure.Queries;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class UserOrderRepository : BaseRepository<Order>, IUserOrderRepository
    {
        private readonly IDapperContext _dapper;

        public UserOrderRepository(TicketsAndMerchContext context, IDapperContext dapper)
            : base(context)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<UserOrder>> GetUserOrdersAsync(int userId, UserOrderQueryFilter filters)
        {
            return await _dapper.QueryAsync<UserOrder>(UserOrderQueries.GetUserOrdersSqlServer, new
            {
                UserId = userId,
                OrderType = filters.OrderType,
                Status = filters.Status
            });
        }
    }
}
