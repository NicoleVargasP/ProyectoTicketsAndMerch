using Microsoft.EntityFrameworkCore;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Enum;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Infrastructure.Data;
using TicketsAndMerch.Infrastructure.Queries;

namespace TicketsAndMerch.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly IDapperContext _dapper;
        public OrderRepository(TicketsAndMerchContext context, IDapperContext dapper) : base(context)
        {
            _dapper = dapper;  
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => OrderQueries.OrderQuerySqlServer,

                    DatabaseProvider.MySql => @"",
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<Order>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order> GetOrderByIdAsync(int ide)
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => OrderQueries.OrderByIdQuerySQLServer,

                    DatabaseProvider.MySql => @"",
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryFirstOrDefaultAsync<Order>(sql, new { id = ide });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId)
        {
            return await _entities
                .Include(o => o.Merch)
                .Include(o => o.Ticket)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
    }
}
